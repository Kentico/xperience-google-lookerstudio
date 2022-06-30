using CMS;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;

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
        private readonly IEnumerable<FieldSet> mFieldSets;


        public DefaultDataStudioReportGenerator(IReportSchemaProvider reportSchemaProvider)
        {
            this.reportSchemaProvider = reportSchemaProvider;
            mFieldSets = reportSchemaProvider.GetFieldSets();
        }


        public IEnumerable<JObject> GetData(string objectType)
        {
            var fieldSet = mFieldSets.FirstOrDefault(f => f.ObjectType == objectType);
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
            // Delete existing report
            var reportPath = $"{SystemContext.WebApplicationPhysicalPath}\\{DataStudioConstants.REPORT_PATH}";
            if (File.Exists(reportPath))
            {
                File.Delete(reportPath);
            }

            var allData = new List<JObject>();
            var objectTypes = mFieldSets.Select(set => set.ObjectType);
            foreach (var objectType in objectTypes)
            {
                // Add data from object type
                allData.AddRange(GetData(objectType.ToLower()));
            }

            var report = new DataStudioReport
            {
                FieldSets = mFieldSets,
                Data = allData
            };

            // Write JSON file to filesystem
            using (StreamWriter file = File.CreateText(reportPath))
            {
                new JsonSerializer().Serialize(file, report);
            }

            return String.Empty;
        }
    }
}
