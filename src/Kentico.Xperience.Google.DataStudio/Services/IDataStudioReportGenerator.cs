using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    /// <summary>
    /// Retrieves data from the Xperience database and generates a phyiscal JSON file in the
    /// application's filesystem.
    /// </summary>
    public interface IDataStudioReportGenerator
    {
        /// <summary>
        /// Gets a collection of anonymous objects for the specified <paramref name="objectType"/> from
        /// the Xperience database, where the properties of each object are a key/value pair of the
        /// Google Data Studio field names and their values.
        /// </summary>
        /// <param name="objectType">The object type to retrieve data for.</param>
        /// <returns>A collection of anonymous objects representing the data of the object type.</returns>
        Task<IEnumerable<JObject>> GetData(string objectType);


        /// <summary>
        /// Generates the physical JSON report file.
        /// </summary>
        Task GenerateReport();
    }
}
