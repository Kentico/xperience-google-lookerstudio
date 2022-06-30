using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    public interface IDataStudioReportGenerator
    {
        IEnumerable<JObject> GetData(string objectType);


        string GenerateReport();
    }
}
