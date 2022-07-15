/**
 * Gets a collection of fields to display.
 * 
 * @returns {Object} The {@link https://developers.google.com/apps-script/reference/data-studio/fields Fields} of the connector.
 */
const getFields = () => {
  const fieldSets = getReportFields();
  const fields = connector.getFields();
  // Loop through each FieldSet
  for (const fieldSet of fieldSets) {
    const objectType = fieldSet[KEY_FIELDSETS_OBJECTTYPE_PROPERTY];
    const fieldDefinitons = fieldSet[KEY_FIELDSETS_FIELDS_PROPERTY];
    // Loop through each FieldDefinition
    for (const fieldDefiniton of fieldDefinitons) {
      const dataType = getDataType(fieldDefiniton[KEY_FIELDDEFINITIONS_TYPE]);
      const fieldName = fieldDefiniton[KEY_FIELDDEFINITIONS_NAME];

      fields.newDimension()
        .setId(`${objectType}.${fieldName}`)
        .setName(fieldName)
        .setType(dataType);
    }
  }

  return fields;
}

/**
 * Converts the provided data type string to the corresponding Data Studio enum.
 * 
 * @param {String} dataType An data type string representation.
 * @returns {Object} A Data Studio {@link https://developers.google.com/apps-script/reference/data-studio/field-type FieldType}.
 */
const getDataType = (dataType) => {
  switch (dataType) {
  case 'BOOLEAN':
    return FieldTypes.BOOLEAN;
  case 'COUNTRY':
    return FieldTypes.COUNTRY;
  case 'COUNTRY_CODE':
    return FieldTypes.COUNTRY_CODE;
  case 'CITY':
    return FieldTypes.CITY;
  case 'NUMBER':
    return FieldTypes.NUMBER;
  case 'YEAR_MONTH_DAY_SECOND':
    return FieldTypes.YEAR_MONTH_DAY_SECOND;
  case 'URL':
    return FieldTypes.URL;
  case 'TEXT':
  default:
    return FieldTypes.TEXT;
  }
}

/**
 * Required function which provides the schema for the given request.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#getschema}
 * @param {Object} request Data request parameters.
 * @returns {object} A {@link https://developers.google.com/apps-script/reference/data-studio/get-schema-response GetSchemaResponse}.
 */
const getSchema = (request) => {
  return connector.newGetSchemaResponse()
    .setFields(getFields())
    .build();
}

/**
 * Required function which provides user configuration options.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#getconfig}
 * @param {Object} request Data request parameters.
 * @return {object} The built {@link https://developers.google.com/apps-script/reference/data-studio/config Config}.
 */
const getConfig = (request) => {
  const config = connector.getConfig();
  config.setDateRangeRequired(true);
 
  return config.build();
}
