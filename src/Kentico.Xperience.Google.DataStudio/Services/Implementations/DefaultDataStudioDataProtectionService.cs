using CMS;
using CMS.Activities;
using CMS.ContactManagement;
using CMS.Core;
using CMS.DataEngine;
using CMS.DataProtection;
using CMS.Helpers;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterImplementation(typeof(IDataStudioDataProtectionService), typeof(DefaultDataStudioDataProtectionService), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="IDataStudioDataProtectionService"/>.
    /// </summary>
    internal class DefaultDataStudioDataProtectionService : IDataStudioDataProtectionService
    {
        private readonly IConsentAgreementService consentAgreementService;
        private readonly IConsentInfoProvider consentInfoProvider;
        private readonly IContactInfoProvider contactInfoProvider;
        private readonly ISettingsService settingsService;


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataStudioDataProtectionService"/> class.
        /// </summary>
        public DefaultDataStudioDataProtectionService(ISettingsService settingsService,
            IConsentAgreementService consentAgreementService,
            IConsentInfoProvider consentInfoProvider,
            IContactInfoProvider contactInfoProvider)
        {
            this.settingsService = settingsService;
            this.consentAgreementService = consentAgreementService;
            this.consentInfoProvider = consentInfoProvider;
            this.contactInfoProvider = contactInfoProvider;
        }


        public List<JObject> AnonymizeData(IEnumerable<FieldSet> fieldSets, List<JObject> data)
        {
            var hashSettings = new HashSettings(nameof(DefaultDataStudioDataProtectionService))
            {
                HashStringSaltOverride = Guid.NewGuid().ToString()

            };
            foreach (var fieldSet in fieldSets)
            {
                var fieldsToAnonymize = fieldSet.Fields.Where(f => f.Anonymize);
                foreach (var field in fieldsToAnonymize)
                {
                    // Data type for anonymized field must be text
                    field.DataType = DataStudioFieldType.TEXT;
                    foreach (var obj in data)
                    {
                        var propToAnonymize = obj.Property($"{fieldSet.ObjectType.ToLowerInvariant()}.{field.Name}");
                        if (propToAnonymize == null)
                        {
                            continue;
                        }

                        var existingValue = propToAnonymize.Value.Value<string>();
                        var anonValue = ValidationHelper.GetHashString(existingValue, hashSettings);
                        propToAnonymize.Value = anonValue;
                    }
                }
            }

            return data;
        }


        public async Task<bool> IsObjectAllowed(BaseInfo infoObject)
        {
            var consentId = ValidationHelper.GetInteger(settingsService[DataStudioConstants.SETTINGKEY_CONSENTID], 0);
            if (consentId == 0)
            {
                return true;
            }

            // Only check consents for contacts and activities
            if (!(infoObject is ContactInfo) && !(infoObject is ActivityInfo))
            {
                return true;
            }

            var consent = await consentInfoProvider.GetAsync(consentId).ConfigureAwait(false);
            if (consent == null)
            {
                throw new InvalidOperationException("The selected consent could not be found.");
            }

            ContactInfo contact = null;
            if (infoObject is ContactInfo)
            {
                contact = infoObject as ContactInfo;
            }
            else if (infoObject is ActivityInfo)
            {
                var contactId = infoObject.GetIntegerValue(nameof(ActivityInfo.ActivityContactID), 0);
                contact = await contactInfoProvider.GetAsync(contactId).ConfigureAwait(false);
            }

            if (contact == null)
            {
                return false;
            }

            return consentAgreementService.IsAgreed(contact, consent);
        }
    }
}