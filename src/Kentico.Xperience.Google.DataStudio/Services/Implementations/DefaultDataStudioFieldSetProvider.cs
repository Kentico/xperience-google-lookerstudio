using CMS;
using CMS.Core;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using System.Collections.Generic;

[assembly: RegisterImplementation(typeof(IDataStudioFieldSetProvider), typeof(DefaultDataStudioFieldSetProvider), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="IDataStudioFieldSetProvider"/>.
    /// </summary>
    internal class DefaultDataStudioFieldSetProvider : IDataStudioFieldSetProvider
    {
        public IEnumerable<FieldSet> GetFieldSets()
        {
            return new FieldSet[]
            {
                DataStudioConstants.activityFieldSet,
                DataStudioConstants.automationStateFieldSet,
                DataStudioConstants.consentAgreementFieldSet,
                DataStudioConstants.consentFieldSet,
                DataStudioConstants.contactFieldSet,
                DataStudioConstants.contactGroupFieldSet,
                DataStudioConstants.contactGroupMemberFieldSet,
                DataStudioConstants.countryFieldSet,
                DataStudioConstants.customerFieldSet,
                DataStudioConstants.orderFieldSet,
                DataStudioConstants.ruleFieldSet,
                DataStudioConstants.scoreContactRuleFieldSet,
                DataStudioConstants.scoreFieldSet,
                DataStudioConstants.siteFieldSet,
                DataStudioConstants.stateFieldSet,
                DataStudioConstants.userFieldSet,
                DataStudioConstants.workflowFieldSet
            };
        }
    }
}
