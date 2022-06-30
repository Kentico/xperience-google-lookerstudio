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
                DataStudioConstants.activityFieldSet,
                DataStudioConstants.contactFieldSet,
                DataStudioConstants.contactGroupFieldSet,
                DataStudioConstants.contactGroupMemberFieldSet,
                DataStudioConstants.countryFieldSet,
                DataStudioConstants.siteFieldSet,
                DataStudioConstants.stateFieldSet,
                DataStudioConstants.userFieldSet
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
