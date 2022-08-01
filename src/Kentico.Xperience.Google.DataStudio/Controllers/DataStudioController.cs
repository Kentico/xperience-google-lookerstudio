using CMS.Core;
using CMS.Helpers;

using Kentico.Xperience.Google.DataStudio.Models;
using Kentico.Xperience.Google.DataStudio.Services;

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kentico.Xperience.Google.DataStudio.Controllers
{
    /// <summary>
    /// A .NET Web API controller which receives requests for the report from the Google Data Studio connector.
    /// </summary>
    [ReportAuthorization]
    public class DataStudioController : ApiController
    {
        private readonly IDataStudioReportProvider reportProvider;

        private DateTime StartTime
        {
            get
            {
                DateTime retVal;
                var start = QueryHelper.GetString("start", String.Empty);
                if (!DateTime.TryParse(start, out retVal))
                {
                    return DateTime.MinValue;
                }

                return retVal;
            }
        }


        private DateTime EndTime
        {
            get
            {
                DateTime retVal;
                var end = QueryHelper.GetString("end", String.Empty);
                if (!DateTime.TryParse(end, out retVal))
                {
                    return DateTime.MaxValue;
                }

                retVal.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                return retVal;
            }
        }


        private string[] ObjectTypes
        {
            get
            {
                var types = QueryHelper.GetString("objectTypes", String.Empty);
                if (String.IsNullOrEmpty(types))
                {
                    return null;
                }

                return types.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DataStudioController"/> class.
        /// </summary>
        public DataStudioController()
        {
            reportProvider = Service.Resolve<IDataStudioReportProvider>();
        }


        /// <summary>
        /// Returns only the <see cref="DataStudioReport.Data"/> property of the report which is filtered
        /// by query string parameters which include a time frame and list of object types.
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetReportData()
        {
            if (ObjectTypes == null || StartTime == DateTime.MinValue || EndTime == DateTime.MaxValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var data = await reportProvider.GetReportData(StartTime, EndTime, ObjectTypes).ConfigureAwait(false);
            if (data == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        /// <summary>
        /// Returns only the <see cref="DataStudioReport.FieldSets"/> property of the report.
        /// </summary>
        [HttpGet]
        public async Task<HttpResponseMessage> GetReportFields()
        {
            var fieldSets = await reportProvider.GetReportFields().ConfigureAwait(false);
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
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
