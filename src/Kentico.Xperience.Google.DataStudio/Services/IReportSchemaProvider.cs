using Kentico.Xperience.Google.DataStudio.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Data;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    public interface IReportSchemaProvider
    {
        IEnumerable<FieldSet> GetFieldSets();


        JObject ProcessObject(string objectType, DataRow row);
    }
}
