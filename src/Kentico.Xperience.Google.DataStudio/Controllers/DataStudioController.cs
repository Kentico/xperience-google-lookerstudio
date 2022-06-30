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
using System.Web.Http;

namespace Kentico.Xperience.Google.DataStudio.Controllers
{
    public class DataStudioController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetReport()
        {
            var user = BasicAuthenticate();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var report = LoadReport();
            if (report == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            ApplyObjectTypeFilter(report);
            ApplyDateFilter(report);

            return Request.CreateResponse(HttpStatusCode.OK, report);
        }


        [HttpGet]
        public HttpResponseMessage GetReportFields()
        {
            var user = BasicAuthenticate();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var report = LoadReport();
            if (report == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, report.FieldSets);
        }


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


        private void ApplyDateFilter(DataStudioReport report)
        {
            var start = QueryHelper.GetString("start", String.Empty);
            var end = QueryHelper.GetString("end", String.Empty);
            if (String.IsNullOrEmpty(start) || String.IsNullOrEmpty(end))
            {
                return;
            }

            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
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


        private void ApplyObjectTypeFilter(DataStudioReport report)
        {
            var objectTypes = QueryHelper.GetString("objectTypes", String.Empty);
            if (String.IsNullOrEmpty(objectTypes))
            {
                return;
            }

            var typeArray = objectTypes.Split(',');
            report.Data = report.Data.Where(obj => {
                var propName = obj.Properties().FirstOrDefault().Name;
                var parts = propName.Split('.');

                return typeArray.Any(type => type == $"{parts[0]}.{parts[1]}");
            });
        }


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


        private DataStudioReport LoadReport()
        {
            var reportPath = $"{SystemContext.WebApplicationPhysicalPath}\\{DataStudioConstants.REPORT_PATH}";
            if (!File.Exists(reportPath))
            {
                return null;
            }

            using (StreamReader r = new StreamReader(reportPath))
            {
                return JsonConvert.DeserializeObject<DataStudioReport>(r.ReadToEnd());
            }
        }
    }
}
