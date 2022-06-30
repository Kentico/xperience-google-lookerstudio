using System.Collections.Generic;

namespace Kentico.Xperience.Google.DataStudio.Models
{
    public class FieldSet
    {
        public string ObjectType
        {
            get;
            set;
        }


        public IEnumerable<FieldDefinition> Fields
        {
            get;
            set;
        }


        public string DateFilterField
        {
            get;
            set;
        }
    }
}
