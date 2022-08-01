using CMS;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterImplementation(typeof(IDataStudioReportGenerator), typeof(DefaultDataStudioReportGenerator), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="IDataStudioReportGenerator"/>.
    /// </summary>
    internal class DefaultDataStudioReportGenerator : IDataStudioReportGenerator
    {
        private readonly IEnumerable<FieldSet> fieldSets;


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataStudioReportGenerator"/> class.
        /// </summary>
        public DefaultDataStudioReportGenerator(IDataStudioFieldSetProvider fieldSetProvider)
        {
            fieldSets = fieldSetProvider.GetFieldSets();
        }


        public async Task<IEnumerable<JObject>> GetData(string objectType)
        {
            var fieldSet = fieldSets.FirstOrDefault(f => f.ObjectType.Equals(objectType, StringComparison.OrdinalIgnoreCase));
            if (fieldSet == null)
            {
                return Enumerable.Empty<JObject>();
            }

            var columns = fieldSet.Fields.Select(f => f.Name);
            var result = await new ObjectQuery(objectType)
                .Columns(columns)
                .GetEnumerableTypedResultAsync()
                .ConfigureAwait(false);

            return result.Select(infoObject => ProcessObject(objectType, infoObject, columns));
        }


        public async void GenerateReport()
        {
            // Ensure folder exists
            var directory = Path.Combine(SystemContext.WebApplicationPhysicalPath, DataStudioConstants.reportDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Add anonymous objects to list
            var allData = new List<JObject>();
            foreach (var fieldSet in fieldSets)
            {
                var objectTypeData = await GetData(fieldSet.ObjectType.ToLowerInvariant()).ConfigureAwait(false);
                AnonymizeData(fieldSet, objectTypeData);

                allData.AddRange(objectTypeData);
            }

            var report = new DataStudioReport
            {
                FieldSets = fieldSets,
                Data = allData
            };

            // Write JSON file to filesystem
            var fullPath = Path.Combine(directory, DataStudioConstants.reportName);
            using (StreamWriter file = File.CreateText(fullPath))
            {
                new JsonSerializer().Serialize(file, report);
            }
        }


        /// <summary>
        /// Converts the value of any field where <see cref="FieldDefinition.Anonymize"/> is true into
        /// a hashed value.
        /// </summary>
        /// <param name="fieldSet">The current <see cref="FieldSet"/> whose data is being hashed.</param>
        /// <param name="data">The anonymous objects to apply hashing to.</param>
        private void AnonymizeData(FieldSet fieldSet, IEnumerable<JObject> data)
        {
            var fieldsToAnonymize = fieldSet.Fields.Where(f => f.Anonymize);
            var hashSettings = new HashSettings(nameof(DefaultDataStudioReportGenerator))
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


        private JObject ProcessObject(string objectType, BaseInfo infoObject, IEnumerable<string> columns)
        {
            var obj = new JObject();
            foreach (var column in columns)
            {
                var columnValue = infoObject.GetValue(column);
                if (columnValue == DBNull.Value || columnValue == null || String.IsNullOrEmpty(columnValue.ToString()))
                {
                    continue;
                }

                obj.Add($"{objectType}.{column}", JToken.FromObject(columnValue));
            }

            return obj;
        }
    }
}
