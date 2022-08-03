using CMS;
using CMS.Base;
using CMS.Core;
using CMS.Helpers;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterImplementation(typeof(IDataStudioReportProvider), typeof(DefaultDataStudioReportProvider), Lifestyle = Lifestyle.Singleton, Priority = RegistrationPriority.SystemDefault)]
namespace Kentico.Xperience.Google.DataStudio.Services.Implementations
{
    /// <summary>
    /// The default implementation of <see cref="IDataStudioReportProvider"/>.
    /// </summary>
    internal class DefaultDataStudioReportProvider : IDataStudioReportProvider
    {
        /// <summary>
        /// The cache dependency key which all Google Data Studio report data is dependent on.
        /// </summary>
        public const string CACHE_DEPENDENCY = "gds|cachedependency";


        private readonly IProgressiveCache progressiveCache;


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataStudioReportProvider"/> class.
        /// </summary>
        public DefaultDataStudioReportProvider(IProgressiveCache progressiveCache)
        {
            this.progressiveCache = progressiveCache;
        }


        public async Task<IEnumerable<JObject>> GetReportData(DateTime start, DateTime end, string[] objectTypes)
        {
            return await progressiveCache.LoadAsync(async cs =>
            {
                var report = await LoadReport().ConfigureAwait(false);
                if (report == null)
                {
                    cs.Cached = false;
                    return null;
                }

                ApplyObjectTypeFilter(report, objectTypes);
                ApplyDateFilter(report, start, end);

                cs.CacheDependency = CacheHelper.GetCacheDependency(CACHE_DEPENDENCY);

                return report.Data;
            }, new CacheSettings(TimeSpan.FromMinutes(60).TotalMinutes, $"gds|getdata|{start}|{end}|{objectTypes.Join(",")}")).ConfigureAwait(false);
        }


        public async Task<IEnumerable<FieldSet>> GetReportFields()
        {
            return await progressiveCache.LoadAsync(async cs =>
            {
                var report = await LoadReport().ConfigureAwait(false);
                if (report == null)
                {
                    cs.Cached = false;
                    return null;
                }

                cs.CacheDependency = CacheHelper.GetCacheDependency(CACHE_DEPENDENCY);

                return report.FieldSets;
            }, new CacheSettings(TimeSpan.FromMinutes(60).TotalMinutes, $"gds|getreportfields")).ConfigureAwait(false);
        }


        /// <summary>
        /// Filters the <see cref="DataStudioReport.Data"/> property by removing objects in which the
        /// <see cref="FieldSet.DateFilterField"/> value is not within the <paramref name="start"/> and
        /// <paramref name="end"/> dates. Object types with no <see cref="FieldSet.DateFilterField"/> will
        /// have all data returned.
        /// </summary>
        /// <param name="report">The report to filter the data of.</param>
        /// <param name="start">The start of the time range to filter data on.</param>
        /// <param name="end">The end of the time range to filter data on.</param>
        private void ApplyDateFilter(DataStudioReport report, DateTime start, DateTime end)
        {
            var dateFilterFields = report.FieldSets
                .Where(set => !String.IsNullOrEmpty(set.DateFilterField))
                .Select(set => $"{set.ObjectType}.{set.DateFilterField}");
            report.Data = report.Data.Where(obj => {
                var propToFilter = obj.Properties().FirstOrDefault(prop => dateFilterFields.Contains(prop.Name));
                if (propToFilter == null)
                {
                    // Object has no filtering field, return all data
                    return true;
                }

                var objectCreated = propToFilter.Value.Value<DateTime>();
                return objectCreated >= start && objectCreated <= end;
            });
        }


        /// <summary>
        /// Filters the <see cref="DataStudioReport.Data"/> property by removing object types which are not listed
        /// in the <paramref name="objectTypes"/> parameter.
        /// </summary>
        /// <param name="report">The report to filter the data of.</param>
        /// <param name="objectTypes">The list of object types to include in the report data.</param>
        private void ApplyObjectTypeFilter(DataStudioReport report, string[] objectTypes)
        {
            report.Data = report.Data.Where(obj => {
                var prop = obj.Properties().FirstOrDefault();
                if (prop == null)
                {
                    return false;
                }

                var parts = prop.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return objectTypes.Contains($"{parts[0]}.{parts[1]}", StringComparer.OrdinalIgnoreCase);
            });
        }


        /// <summary>
        /// Returns the full Google Data Studio report from the filesystem, or null if not found.
        /// </summary>
        private async Task<DataStudioReport> LoadReport()
        {
            var fullPath = Path.Combine(SystemContext.WebApplicationPhysicalPath, DataStudioConstants.REPORT_DIRECTORY, DataStudioConstants.REPORT_NAME);
            if (!File.Exists(fullPath))
            {
                return null;
            }

            using (StreamReader r = new StreamReader(fullPath))
            {
                var reportText = await r.ReadToEndAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<DataStudioReport>(reportText);
            }
        }
    }
}