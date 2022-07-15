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

[assembly: RegisterImplementation(typeof(IDataStudioReportGenerator), typeof(DefaultDataStudioReportGenerator), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    internal class DefaultDataStudioReportGenerator : IDataStudioReportGenerator
    {
        private readonly IReportSchemaProvider reportSchemaProvider;
        private readonly IEnumerable<FieldSet> fieldSets;


        public DefaultDataStudioReportGenerator(IReportSchemaProvider reportSchemaProvider)
        {
            this.reportSchemaProvider = reportSchemaProvider;
            fieldSets = reportSchemaProvider.GetFieldSets();
        }


        public IEnumerable<JObject> GetData(string objectType)
        {
            var fieldSet = fieldSets.FirstOrDefault(f => f.ObjectType == objectType);
            if (fieldSet == null)
            {
                return Enumerable.Empty<JObject>();
            }

            return new DataQuery(objectType, QueryName.GENERALSELECT)
                .Columns(fieldSet.Fields.Select(f => f.Name))
                .Result
                .Tables[0]
                .AsEnumerable()
                .Select(row => reportSchemaProvider.ProcessObject(objectType, row))
                .ToList();
        }


        public string GenerateReport()
        {
            // Ensure folder exists
            var directory = $"{SystemContext.WebApplicationPhysicalPath}\\App_Data\\CMSModules\\Kentico.Xperience.Google.DataStudio";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Add anonymous objects to list
            var allData = new List<JObject>();
            foreach (var fieldSet in fieldSets)
            {
                var objectTypeData = GetData(fieldSet.ObjectType.ToLower());
                AnonymizeData(fieldSet, objectTypeData);

                allData.AddRange(objectTypeData);
            }

            var report = new DataStudioReport
            {
                FieldSets = fieldSets,
                Data = allData
            };

            // Write JSON file to filesystem
            using (StreamWriter file = File.CreateText($"{directory}\\datastudio.json"))
            {
                new JsonSerializer().Serialize(file, report);
            }

            return String.Empty;
        }


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
                    var propToAnonymize = obj.Property($"{fieldSet.ObjectType.ToLower()}.{field.Name}");
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
    }
}
