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
      const isMetric = fieldDefiniton[KEY_FIELDDEFINITIONS_ISMETRIC];
      const dataType = getDataType(fieldDefiniton[KEY_FIELDDEFINITIONS_TYPE]);
      const fieldName = fieldDefiniton[KEY_FIELDDEFINITIONS_NAME];

      let field;
      if (isMetric) {
        field = fields.newMetric().setAggregation(AggregationType.COUNT);
      }
      else {
        field = fields.newDimension();
      }
      field
        .setId(`${objectType}.${fieldName}`)
        .setName(fieldName)
        .setType(dataType);
    }
  }

  return fields;
}

/**
 * Converts the provided Xperience data type to the corresponding Data Studio type.
 * 
 * @param {String} xperienceDataType An Xperience data type.
 * @returns {Object} A Data Studio {@link https://developers.google.com/apps-script/reference/data-studio/field-type FieldType}.
 */
const getDataType = (xperienceDataType) => {
  switch (xperienceDataType) {
  case 'boolean':
    return FieldTypes.BOOLEAN;
  case 'integer':
  case 'double':
  case 'longinteger':
  case 'decimal':
    return FieldTypes.NUMBER;
  case 'date':
  case 'datetime':
    return FieldTypes.YEAR_MONTH_DAY_SECOND;
  case 'text':
  case 'longtext':
  case 'file':
  case 'guid':
  case 'binary':
  case 'xml':
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
