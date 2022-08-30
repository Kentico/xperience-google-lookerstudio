/**
 * Required function which provides the report data.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#getdata}
 * @param {Object} request Data request parameters.
 * @returns {Object} A {@link https://developers.google.com/apps-script/reference/data-studio/get-data-response GetDataResponse}.
 */
const getData = (request) => {
  const data = getReportData(request);
  const requestedFields = getFields().forIds(
    request.fields.map(field => field.name)
  );

  return connector.newGetDataResponse()
    .setFields(requestedFields)
    .addAllRows(getFormattedData(requestedFields, data))
    .build();
}
   
/**
 * Formats Xperience objects from the report into arrays of values.
 *
 * @param {Object} requestedFields The {@link https://developers.google.com/apps-script/reference/data-studio/field Fields} requested in the `getData` request.
 * @param {Object[]} data An array of anonymous objects from the report.
 * @returns {Array[]} An array containing rows of data.
 */
const getFormattedData = (requestedFields, data) => {
  let rows = [];
  const fields = requestedFields.asArray();
  for (const object of data) {
    const formattedData = fields.map(requestedField => formatData(requestedField, object));
    rows.push(formattedData);
  }

  return rows;
}

/**
 * Retrieves a single field's value in Google Data Studio the desired format.
 * 
 * @param {Object} requestedField The {@link https://developers.google.com/apps-script/reference/data-studio/field Field} to retrieve the value of.
 * @param {Object} object The report object to retrieve data from. 
 * @returns {Object} The value of the field.
 */
const formatData = (requestedField, object) => {
  const fieldName = requestedField.getId();
  const value = object[fieldName];

  if (!value) {
    switch (requestedField.getType()) {
    case FieldTypes.NUMBER:
      return 0;
    case FieldTypes.TEXT:
      return '';
    }
  }

  if (requestedField.getType() === FieldTypes.YEAR_MONTH_DAY_SECOND) {
    return getDataStudioDateTime(value);
  }

  return value;
}
