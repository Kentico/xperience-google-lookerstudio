const connector = DataStudioApp.createCommunityConnector();

// Google Data Studio enums
const FieldTypes = connector.FieldType;
const AuthTypes = connector.AuthType;
const AggregationType = connector.AggregationType;

// Xperience endpoints
const REPORT_ENDPOINT = 'xperience-google-datastudio/getreport';
const VALIDATION_ENDPOINT = 'xperience-google-datastudio/validatecredentials';
const FIELDS_ENDPOINT = 'xperience-google-datastudio/getreportfields';

// General constants
const PARAM_SITE = 'site';
const PROPERTY_USERNAME = 'dscc.xperience.user';
const PROPERTY_PASSWORD = 'dscc.xperience.pass';
const PROPERTY_USERPATH = 'dscc.xperience.path';

// Report main level properties
const KEY_FIELDSETS_PROPERTY = 'FieldSets';
const KEY_DATA_PROPERTY = 'Data';

// FieldSet properties
const KEY_FIELDSETS_OBJECTTYPE_PROPERTY = 'ObjectType';
const KEY_FIELDSETS_FIELDS_PROPERTY = 'Fields';
const KEY_FIELDSETS_DATEFILTER_PROPERTY = 'DateFilterField';
const KEY_FIELDSETS_DIMENSION_PROPERTY = 'DefaultDimension';
const KEY_FIELDSETS_METRIC_PROPERTY = 'DefaultMetric';

// FieldDefinition properties
const KEY_FIELDDEFINITIONS_NAME = 'Name';
const KEY_FIELDDEFINITIONS_TYPE = 'DataType';