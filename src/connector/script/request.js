/**
 * Requests the report JSON data array from the Xperience administration.
 *
 * @param {Object} request Data request parameters.
 * @returns {Object[]} Anonymous data objects from the report.
 */
const getReportData = (request) => {
  // Get the object types requested to reduce report size
  const objectTypes = getRequestedObjectTypes(request);
  const credentials = getCredentials();
  const url = `${credentials.path}/${DATA_ENDPOINT}?start=${request.dateRange.startDate}&end=${request.dateRange.endDate}&objectTypes=${objectTypes.join(',')}`;
  const response = sendRequest(url, credentials.username, credentials.password);

  return JSON.parse(response.getContentText());
}

/**
 * Requests the report FieldSets JSON array from the Xperience administration.
 * 
 * @returns {Object[]} Anonymous FieldSet objects from the report.
 */
const getReportFields = () => {
  const credentials = getCredentials();
  const url = `${credentials.path}/${FIELDS_ENDPOINT}`;
  const response = sendRequest(url, credentials.username, credentials.password);

  return JSON.parse(response.getContentText());
}

/**
 * Validates the connector's authentication configuration by requesting a validtion endpoint in the Xperience administration.
 * 
 * @param {String} path The absolute URL of the Xperience administration website.
 * @param {String} username The username of an Xperience user.
 * @param {String} password The password of an Xperience user.
 * @returns `true` if the request resulted in a 200 response and the credentials are valid.
 */
const validateCredentials = (path, username, password) => {
  if (!path || !username || !password) {
    return false;
  }

  const url = `${path}/${VALIDATION_ENDPOINT}`;
  const response = sendRequest(url, username, password);

  return response.getResponseCode() === 200;
}

/**
 * Sends a request to the specified URL with the Basic authentication header.
 * 
 * @param {String} url The URL to send the request to.
 * @param {String} username The username of an Xperience user.
 * @param {String} password The password of an Xperience user.
 * @returns {Object} An {@link https://developers.google.com/apps-script/reference/url-fetch/http-response HTTPResponse}.
 */
const sendRequest = (url, username, password) => {
  const options = {
    headers: {'Authorization': 'Basic ' + Utilities.base64Encode(`${username}:${password}`)}
  };
  
  return UrlFetchApp.fetch(url, options);
}
