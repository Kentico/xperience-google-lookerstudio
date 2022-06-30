using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    public class DataStudioReport
    {
        public IEnumerable<FieldSet> FieldSets
        {
            get;
            set;
        }

        public IEnumerable<JObject> Data
        {
            get;
            set;
        }
    }
}
