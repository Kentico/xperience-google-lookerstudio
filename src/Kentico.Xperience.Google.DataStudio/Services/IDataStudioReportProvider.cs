using Kentico.Xperience.Google.DataStudio.Models;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    /// <summary>
    /// Reads data from the physical Google Data Studio report file and returns only the requested data.
    /// </summary>
    public interface IDataStudioReportProvider
    {
        /// <summary>
        /// Reads the phyiscal report file and returns only requested data based on a time range and
        /// Xperience object types.
        /// </summary>
        /// <returns>The filtered <see cref="DataStudioReport.Data"/> property, or null if the report
        /// can't be loaded.</returns>
        Task<IEnumerable<JObject>> GetReportData(DateTime start, DateTime end, string[] objectTypes);


        /// <summary>
        /// Reads the phyiscal report file and returns the fields that should be allowed for selection in
        /// Google Data Studio.
        /// </summary>
        /// <returns>The <see cref="DataStudioReport.FieldSets"/> property, or null if the report can't
        /// be loaded.</returns>
        Task<IEnumerable<FieldSet>> GetReportFields();
    }
}