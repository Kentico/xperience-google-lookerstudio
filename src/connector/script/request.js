/**
 * Requests the report JSON data array from the Xperience administration.
 *
 * @param {Object} request Data request parameters.
 */
const getReportData = (request) => {
  // Get the object types requested to reduce report size
  const objectTypes = getRequestedObjectTypes(request);
  const credentials = getCredentials();
  const url = `${credentials.path}/${DATA_ENDPOINT}?start=${request.dateRange.startDate}&end=${request.dateRange.endDate}&objectTypes=${objectTypes.join(',')}`;
  const response = sendRequest(url, credentials.username, credentials.password);

  return JSON.parse(response.getContentText());
}

const getReportFields = () => {
  const credentials = getCredentials();
  const url = `${credentials.path}/${FIELDS_ENDPOINT}`;
  const response = sendRequest(url, credentials.username, credentials.password);

  return JSON.parse(response.getContentText());
}

const validateCredentials = (path, username, password) => {
  if (!path || !username || !password) {
    return false;
  }

  const url = `${path}/${VALIDATION_ENDPOINT}`;
  const response = sendRequest(url, username, password);

  return response.getResponseCode() === 200;
}

const sendRequest = (url, username, password) => {
  const options = {
    headers: {'Authorization': 'Basic ' + Utilities.base64Encode(`${username}:${password}`)}
  };
  
  return UrlFetchApp.fetch(url, options);
}
