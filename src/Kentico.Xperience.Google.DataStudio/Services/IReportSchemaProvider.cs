using Kentico.Xperience.Google.DataStudio.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Data;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    /// <summary>
    /// Contains methods which determine the which fields appear in the physical report file, in
    /// Google Data Studio, and the structure of the data in the report.
    /// </summary>
    public interface IReportSchemaProvider
    {
        /// <summary>
        /// Gets a collection of <see cref="FieldSet"/>s which determine the fields available in
        /// Google Data Studio and the data in the phyiscal report file.
        /// </summary>
        /// <returns>The <see cref="FieldSet"/>s to use in reporting.</returns>
        IEnumerable<FieldSet> GetFieldSets();


        /// <summary>
        /// Converts data from the Xperience database into an anonymous object to be added to the report.
        /// </summary>
        /// <remarks>Typically, the value of each <paramref name="row"/> column is added as a single property
        /// to the returned object. However, this method allows for more complex scenarios where the value of
        /// a column is used as the data source for multiple custom report fields.</remarks>
        /// <param name="objectType">The Xperience object type of the provided <paramref name="row"/> data.</param>
        /// <param name="row">The data retrieved from the Xperience database to be converted.</param>
        /// <returns>An anonymous object representing a single Xperience object.</returns>
        JObject ProcessObject(string objectType, DataRow row);
    }
}
