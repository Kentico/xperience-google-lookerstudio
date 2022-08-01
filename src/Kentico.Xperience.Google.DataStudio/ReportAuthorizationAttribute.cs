using CMS.Core;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;

using Kentico.Xperience.Google.DataStudio.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Kentico.Xperience.Google.DataStudio
{
    /// <summary>
    /// A custom authorization attribute which validates Basic authentication headers against
    /// the Xperience users.
    /// </summary>
    public class ReportAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private readonly IEventLogService eventLogService;


        /// <summary>
        /// Initializes a new instance of the <see cref="ReportAuthorizationAttribute"/> class.
        /// </summary>
        public ReportAuthorizationAttribute()
        {
            eventLogService = Service.Resolve<IEventLogService>();
        }


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = BasicAuthenticate(actionContext.Request.Headers);
            if (user == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }


        /// <summary>
        /// Validates the Basic authentication header and returns the matching Xperience user.
        /// </summary>
        /// <param name="headers">The request headers.</param>
        /// <returns>An Xperience user, or null if the header is not valid.</returns>
        private UserInfo BasicAuthenticate(HttpRequestHeaders headers)
        {
            string username, password;
            IEnumerable<string> headerValues;
            if (!headers.TryGetValues("Authorization", out headerValues))
            {
                eventLogService.LogError(nameof(ReportAuthorizationAttribute), nameof(BasicAuthenticate), "Authorization header not present in request.");
                return null;
            }

            var authorizationHeader = headerValues.FirstOrDefault();
            if (String.IsNullOrEmpty(authorizationHeader))
            {
                eventLogService.LogError(nameof(ReportAuthorizationAttribute), nameof(BasicAuthenticate), "Invalid Authorization header.");
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
    }
}