/**
 * Required function which provides the report data.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#getdata}
 * @param {Object} request Data request parameters.
 * @returns {Object} A {@link https://developers.google.com/apps-script/reference/data-studio/get-data-response GetDataResponse}.
 */
const getData = (request) => {
  const report = getReport(request);
  const requestedFields = getFields().forIds(
    request.fields.map(field => field.name)
  );

  return connector.newGetDataResponse()
    .setFields(requestedFields)
    .addAllRows(getFormattedData(requestedFields, report))
    .build();
}
   
/**
 * Formats Xperience objects from the report into arrays of values.
 *
 * @param {Object} requestedFields The {@link https://developers.google.com/apps-script/reference/data-studio/field Fields} requested in the `getData` request.
 * @returns {Array[]} An array containing rows of data.
 */
const getFormattedData = (requestedFields, report) => {
  let rows = [];
  const fields = requestedFields.asArray();
  const objects = report[KEY_DATA_PROPERTY];
  for (const object of objects) {
    const formattedData = fields.map(requestedField => formatData(requestedField, object));
    rows.push(formattedData);
  }

  return rows;
}

const formatData = (requestedField, object) => {
  const fieldName = requestedField.getId();
  if (!Object.keys(object).includes(fieldName)) {
    return '';
  }

  const value = object[fieldName];
  if (requestedField.getType() === FieldTypes.YEAR_MONTH_DAY_SECOND) {
    return getDataStudioDateTime(value);
  }

  return value;
}
