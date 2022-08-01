using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;

[assembly: RegisterImplementation(typeof(IDataStudioDataProtectionProvider), typeof(DefaultDataStudioDataProtectionProvider), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="IDataStudioDataProtectionProvider"/>.
    /// </summary>
    internal class DefaultDataStudioDataProtectionProvider : IDataStudioDataProtectionProvider
    {
        public void AnonymizeData(FieldSet fieldSet, IEnumerable<JObject> data)
        {
            var fieldsToAnonymize = fieldSet.Fields.Where(f => f.Anonymize);
            var hashSettings = new HashSettings(nameof(DefaultDataStudioDataProtectionProvider))
            {
                HashStringSaltOverride = Guid.NewGuid().ToString()

            };
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


        public bool IsObjectAllowed(BaseInfo infoObject)
        {
            // No object filtering applied by default
            return true;
        }
    }
}