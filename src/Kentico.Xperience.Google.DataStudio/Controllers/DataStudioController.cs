using CMS.Base;
using CMS.Core;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;

using Kentico.Xperience.Google.DataStudio.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kentico.Xperience.Google.DataStudio.Controllers
{
    /// <summary>
    /// A .NET Web API controller which receives requests for the report from the Google Data Studio connector.
    /// </summary>
    public class DataStudioController : ApiController
    {
        private string StartTime
        {
            get
            {
                return QueryHelper.GetString("start", String.Empty);
            }
        }


        private string EndTime
        {
            get
            {
                return QueryHelper.GetString("end", String.Empty);
            }
        }


        private string ObjectTypes
        {
            get
            {
                return QueryHelper.GetString("objectTypes", String.Empty);
            }
        }


        /// <summary>
        /// Reads the phyiscal report file and returns the requested data based on query string parameters
        /// for date filtering and object type(s).
        /// </summary>
        /// <returns>The JSON representation of <see cref="DataStudioReport.Data"/>.</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetReportData()
        {
            var user = BasicAuthenticate();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var progressiveCache = Service.Resolve<IProgressiveCache>();
            var data = await progressiveCache.LoadAsync(async cs =>
            {
                var report = await LoadReport().ConfigureAwait(false);
                if (report == null)
                {
                    cs.Cached = false;
                    return null;
                }

                ApplyObjectTypeFilter(report);
                ApplyDateFilter(report);

                cs.CacheDependency = CacheHelper.GetCacheDependency(DataStudioReportTask.CACHE_DEPENDENCY);
                
                return report.Data;
            }, new CacheSettings(TimeSpan.FromMinutes(60).TotalMinutes, $"gds|getdata|{StartTime}|{EndTime}|{ObjectTypes}")).ConfigureAwait(false);  
            if (data == null)
            {
                Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        /// <summary>
        /// Reads the phyiscal report file and returns the fields that should be allowed for selection in
        /// Google Data Studio.
        /// </summary>
        /// <returns>The JSON representation of <see cref="DataStudioReport.FieldSets"/>.</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetReportFields()
        {
            var user = BasicAuthenticate();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var progressiveCache = Service.Resolve<IProgressiveCache>();
            var fieldSets = await progressiveCache.LoadAsync(async cs =>
            {
                var report = await LoadReport().ConfigureAwait(false);
                if (report == null)
                {
                    cs.Cached = false;
                    return null;
                }

                cs.CacheDependency = CacheHelper.GetCacheDependency(DataStudioReportTask.CACHE_DEPENDENCY);

                return report.FieldSets;
            }, new CacheSettings(TimeSpan.FromMinutes(60).TotalMinutes, $"gds|getreportfields")).ConfigureAwait(false);
            if (fieldSets == null)
            {
                Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, fieldSets);
        }


        /// <summary>
        /// Validates the Basic authentication header to ensure the Google Data Studio connector's authorization
        /// configuration is valid.
        /// </summary>
        /// <returns>A 200 response if the credentials are valid, otherwise 401.</returns>
        [HttpGet]
        public HttpResponseMessage ValidateCredentials()
        {
            var user = BasicAuthenticate();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        /// <summary>
        /// Uses query string parameters "start" and "end" to determine which objects of <see cref="DataStudioReport.Data"/> should
        /// be returned, and removes all other objects from the report.
        /// </summary>
        /// <param name="report">The report to filter the data of.</param>
        private void ApplyDateFilter(DataStudioReport report)
        {
            if (String.IsNullOrEmpty(StartTime) || String.IsNullOrEmpty(EndTime))
            {
                return;
            }

            var startDate = DateTime.Parse(StartTime);
            var endDate = DateTime.Parse(EndTime);
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
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
                return objectCreated  >= startDate && objectCreated <= endDate;
            });
        }


        /// <summary>
        /// Uses the query string parameter "objectTypes" to determine which objects of <see cref="DataStudioReport.Data"/> should
        /// be returned, and removes all other objects which are not of the requested types.
        /// </summary>
        /// <param name="report">The report to filter the data of.</param>
        private void ApplyObjectTypeFilter(DataStudioReport report)
        {
            if (String.IsNullOrEmpty(ObjectTypes))
            {
                return;
            }

            var typeArray = ObjectTypes.Split(',');
            report.Data = report.Data.Where(obj => {
                var propName = obj.Properties().FirstOrDefault().Name;
                var parts = propName.Split('.');

                return typeArray.Any(type => type == $"{parts[0]}.{parts[1]}");
            });
        }


        /// <summary>
        /// Validates the Basic authentication header and returns the matching Xperience user.
        /// </summary>
        /// <returns>An Xperience user, or null if the header is not valid.</returns>
        private UserInfo BasicAuthenticate()
        {
            string username;
            string password;
            IEnumerable<string> headerValues;
            var eventLogService = Service.Resolve<IEventLogService>();
            if (!Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                eventLogService.LogError(nameof(DataStudioController), nameof(BasicAuthenticate), "Authorization header not present in request.");
                return null;
            }

            var authorizationHeader = headerValues.FirstOrDefault();
            if (String.IsNullOrEmpty(authorizationHeader))
            {
                eventLogService.LogError(nameof(DataStudioController), nameof(BasicAuthenticate), "Invalid Authorization header.");
                return null;
            }

            if (SecurityHelper.TryParseBasicAuthorizationHeader(authorizationHeader, out username, out password))
            {
                if (String.IsNullOrEmpty(password))
                {
                    eventLogService.LogError(nameof(DataStudioController), nameof(BasicAuthenticate), "Empty password is not allowed.");
                    return null;
                }

                return AuthenticationHelper.AuthenticateUser(username, password, SiteContext.CurrentSiteName, false, AuthenticationSourceEnum.ExternalOrAPI);
            }

            return null;
        }


        private async Task<DataStudioReport> LoadReport()
        {
            var reportPath = "\\App_Data\\CMSModules\\Kentico.Xperience.Google.DataStudio\\datastudio.json";
            var fullPath = $"{SystemContext.WebApplicationPhysicalPath}\\{reportPath}";
            if (!File.Exists(fullPath))
            {
                return null;
            }

            using (StreamReader r = new StreamReader(fullPath))
            {
                var reportText = await r.ReadToEndAsync();
                return JsonConvert.DeserializeObject<DataStudioReport>(reportText);
            }
        }
    }
}
