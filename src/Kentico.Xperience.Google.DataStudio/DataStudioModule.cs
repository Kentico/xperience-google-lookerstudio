using CMS;
using CMS.DataEngine;

using Kentico.Xperience.Google.DataStudio;

using System.Net.Http.Headers;
using System.Web.Http;

[assembly: RegisterModule(typeof(DataStudioModule))]
namespace Kentico.Xperience.Google.DataStudio
{
    public class DataStudioModule : Module
    {
        public DataStudioModule() : base(nameof(DataStudioModule))
        {
        }


        protected override void OnInit()
        {
            base.OnInit();

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "GetReport",
                "xperience-google-datastudio/getreport",
                defaults: new { controller = "DataStudio", action = "GetReport" }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "ValidateCredentials",
                "xperience-google-datastudio/validatecredentials",
                defaults: new { controller = "DataStudio", action = "ValidateCredentials" }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "GetReportFields",
                "xperience-google-datastudio/getreportfields",
                defaults: new { controller = "DataStudio", action = "GetReportFields" }
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
