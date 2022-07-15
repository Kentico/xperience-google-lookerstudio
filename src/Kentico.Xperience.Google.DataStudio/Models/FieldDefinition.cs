using Newtonsoft.Json;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    /// <summary>
    /// Represents a single field in a <see cref="DataStudioReport"/>.
    /// </summary>
    public class FieldDefinition
    {
        /// <summary>
        /// The name of the Xperience database column from which the data is retrieved.
        /// </summary>
        public string Name {
            get;
            set;
        }


        /// <summary>
        /// The <see cref="DataStudioFieldType"/> of the field.
        /// </summary>
        public string DataType
        {
            get;
            set;
        }


        /// <summary>
        /// If true, a hash is applied to the field's value while generating the report file.
        /// </summary>
        [JsonIgnore]
        public bool Anonymize
        {
            get;
            set;
        }
    }
}
