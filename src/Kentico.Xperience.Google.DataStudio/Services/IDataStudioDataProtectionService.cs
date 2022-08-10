using CMS.DataEngine;

using Kentico.Xperience.Google.DataStudio.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kentico.Xperience.Google.DataStudio.Services
{
    /// <summary>
    /// Contains methods which modify the contents of the physical report file to be GDPR compliant.
    /// See <see href="https://docs.xperience.io/configuring-xperience/data-protection"/>.
    /// </summary>
    public interface IDataStudioDataProtectionService
    {
        /// <summary>
        /// Converts the value of any field where <see cref="FieldDefinition.Anonymize"/> is true into
        /// a hashed value.
        /// </summary>
        /// <param name="fieldSets">The <see cref="FieldSet"/>s of the report.</param>
        /// <param name="data">The anonymous objects to apply hashing to.</param>
        /// <returns>The <paramref name="data"/> after applying hashing.</returns>
        List<JObject> AnonymizeData(IEnumerable<FieldSet> fieldSets, List<JObject> data);


        /// <summary>
        /// Verifies whether the provided <paramref name="infoObject"/> should be added to the physical report
        /// file or not.
        /// </summary>
        /// <param name="infoObject">The object to verify.</param>
        /// <returns><c>True</c> if the object should be added to the report.</returns>
        Task<bool> IsObjectAllowed(BaseInfo infoObject);
    }
}