using Newtonsoft.Json;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    public class FieldDefinition
    {
        public string Name {
            get;
            set;
        }


        public string DataType
        {
            get;
            set;
        }


        [JsonIgnore]
        public bool Anonymize
        {
            get;
            set;
        }
    }
}
