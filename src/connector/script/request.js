/**
 * Requests the report JSON file from the Xperience administration and stores it in memory.
 *
 * @param {Object} request Data request parameters.
 * @throws Throws a Google App Script exception if the report can't be retrieved.
 */
const getReport = (request) => {
  const credentials = getCredentials();
  const start = new Date(request.dateRange.startDate).toISOString();
  const end = new Date(request.dateRange.endDate).toISOString();

  // Get the object types requested to reduce report size
  const objectTypes = getRequestedObjectTypes(request);

  const url = `${credentials.path}/${REPORT_ENDPOINT}?start=${start}&end=${end}&objectTypes=${objectTypes.join(',')}`;
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
