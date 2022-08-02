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
using System.Threading.Tasks;

[assembly: RegisterImplementation(typeof(IDataStudioReportGenerator), typeof(DefaultDataStudioReportGenerator), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// Default implementation of <see cref="IDataStudioReportGenerator"/>.
    /// </summary>
    internal class DefaultDataStudioReportGenerator : IDataStudioReportGenerator
    {
        private readonly IDataStudioDataProtectionProvider dataProtectionProvider;
        private readonly IEnumerable<FieldSet> fieldSets;


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataStudioReportGenerator"/> class.
        /// </summary>
        public DefaultDataStudioReportGenerator(IDataStudioFieldSetProvider fieldSetProvider, IDataStudioDataProtectionProvider dataProtectionProvider)
        {
            fieldSets = fieldSetProvider.GetFieldSets();
            this.dataProtectionProvider = dataProtectionProvider;
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

            return result
                .Where(infoObject => dataProtectionProvider.IsObjectAllowed(infoObject))
                .Select(infoObject => ProcessObject(objectType, infoObject, columns));
        }


        public async Task GenerateReport()
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
                var hashedData = dataProtectionProvider.AnonymizeData(fieldSet, objectTypeData.ToList());

                allData.AddRange(hashedData);
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
