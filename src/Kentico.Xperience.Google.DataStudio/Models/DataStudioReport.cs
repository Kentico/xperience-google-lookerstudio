using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    /// <summary>
    /// Represents the physical report file stored in the Xperience application's filesystem.
    /// </summary>
    public class DataStudioReport
    {
        /// <summary>
        /// The <see cref="FieldSet"/>s of the report which is used by the Google Data Studio
        /// connector to determine which fields should be available for selection.
        /// </summary>
        public IEnumerable<FieldSet> FieldSets
        {
            get;
            set;
        }


        /// <summary>
        /// A collection of anonymous objects containing the report data.
        /// </summary>
        public IEnumerable<JObject> Data
        {
            get;
            set;
        }
    }
}
