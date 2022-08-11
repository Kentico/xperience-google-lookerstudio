using Kentico.Xperience.Google.DataStudio.Models;

namespace Kentico.Xperience.Google.DataStudio
{
    /// <summary>
    /// Text representations of the Google Data Studio FieldType enum used in <see cref="FieldDefinition.DataType"/>.
    /// Note that this is used to identify the format of the field's data when it is referenced in a Google Data Studio
    /// report, not the format used in the physical report file.
    /// </summary>
    /// <remarks>See <see href="https://developers.google.com/apps-script/reference/data-studio/field-type"/>.</remarks>
    public static class DataStudioFieldType
    {
        /// <summary>
        /// A true or false boolean value.
        /// </summary>
        public const string BOOLEAN = "BOOLEAN";


        /// <summary>
        /// A country such as United States.
        /// </summary>
        public const string COUNTRY = "COUNTRY";


        /// <summary>
        /// A country code such as US.
        /// </summary>
        public const string COUNTRY_CODE = "COUNTRY_CODE";


        /// <summary>
        /// A city such as Mountain View.
        /// </summary>
        public const string CITY = "CITY";


        /// <summary>
        /// A decimal number.
        /// </summary>
        public const string NUMBER = "NUMBER";


        /// <summary>
        /// Free-form text.
        /// </summary>
        public const string TEXT = "TEXT";


        /// <summary>
        /// A URL as text such as https://google.com.
        /// </summary>
        public const string URL = "URL";


        /// <summary>
        /// Year, month, day, hour, minute, and second in the format of YYYYMMDDHHmmss such as 20170317023017.
        /// </summary>
        /// <remarks>SQL datetime values in the physical report file are automatically converted into the required
        /// format in the Google Data Studio connector.</remarks>
        public const string YEAR_MONTH_DAY_SECOND = "YEAR_MONTH_DAY_SECOND";
    }
}