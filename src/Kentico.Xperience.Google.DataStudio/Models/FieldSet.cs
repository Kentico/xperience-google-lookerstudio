using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    /// <summary>
    /// Represents a single object type's configuration within a <see cref="DataStudioReport"/>.
    /// </summary>
    public class FieldSet
    {
        /// <summary>
        /// The code name of the Xperience object type.
        /// </summary>
        public string ObjectType
        {
            get;
            set;
        }


        /// <summary>
        /// A collection of <see cref="FieldDefinition"/>s which determines what fields are generated in
        /// the report file and are available for selection in Google Data Studio.
        /// </summary>
        public IEnumerable<FieldDefinition> Fields
        {
            get;
            set;
        }


        /// <summary>
        /// The <see cref="FieldDefinition.Name"/> of a field which can be used to filter data based on a
        /// start and end date. Typically, this is the field which contains the object's "created on" date.
        /// </summary>
        public string DateFilterField
        {
            get;
            set;
        }
    }
}
