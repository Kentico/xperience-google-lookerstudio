using Kentico.Xperience.Google.DataStudio.Models;

using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    /// <summary>
    /// Provides the <see cref="FieldSet"/>s which appear in the physical report file and Google Data Studio.
    /// </summary>
    public interface IDataStudioFieldSetProvider
    {
        /// <summary>
        /// Gets a collection of <see cref="FieldSet"/>s which determine the fields available in
        /// Google Data Studio and the data in the phyiscal report file.
        /// </summary>
        /// <returns>The <see cref="FieldSet"/>s to use in reporting.</returns>
        IEnumerable<FieldSet> GetFieldSets();
    }
}
