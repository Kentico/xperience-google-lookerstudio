using CMS;
using CMS.Core;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Data;

[assembly: RegisterImplementation(typeof(IReportSchemaProvider), typeof(DefaultReportSchemaProvider), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    internal class DefaultReportSchemaProvider : IReportSchemaProvider
    {
        public IEnumerable<FieldSet> GetFieldSets()
        {
            return new FieldSet[]
            {
                DataStudioDefaultFieldSets.activityFieldSet,
                DataStudioDefaultFieldSets.automationStateFieldSet,
                DataStudioDefaultFieldSets.consentAgreementFieldSet,
                DataStudioDefaultFieldSets.consentFieldSet,
                DataStudioDefaultFieldSets.contactFieldSet,
                DataStudioDefaultFieldSets.contactGroupFieldSet,
                DataStudioDefaultFieldSets.contactGroupMemberFieldSet,
                DataStudioDefaultFieldSets.countryFieldSet,
                DataStudioDefaultFieldSets.customerFieldSet,
                DataStudioDefaultFieldSets.orderFieldSet,
                DataStudioDefaultFieldSets.ruleFieldSet,
                DataStudioDefaultFieldSets.scoreContactRuleFieldSet,
                DataStudioDefaultFieldSets.scoreFieldSet,
                DataStudioDefaultFieldSets.siteFieldSet,
                DataStudioDefaultFieldSets.stateFieldSet,
                DataStudioDefaultFieldSets.userFieldSet,
                DataStudioDefaultFieldSets.workflowFieldSet
            };
        }


        public JObject ProcessObject(string objectType, DataRow row)
        {
            var data = new JObject();
            foreach (DataColumn column in row.Table.Columns)
            {
                var columnValue = row[column.ColumnName];
                if (columnValue == DBNull.Value || columnValue == null || String.IsNullOrEmpty(columnValue.ToString()))
                {
                    continue;
                }

                data.Add($"{objectType}.{column.ColumnName}", JToken.FromObject(columnValue));
            }

            return data;
        }
    }
}
