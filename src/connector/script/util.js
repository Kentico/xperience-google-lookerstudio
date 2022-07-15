/**
 * Converts a list of requested fields into an array of unique strings containing the Xperience object types that were requested.
 * 
 * @param {Object} request Data request parameters.
 * @returns {String[]} An array of Xperience object types without duplicate values.
 */
const getRequestedObjectTypes = (request) => {
  const requestedFields = request.fields.map(field => field.name);
  const objecTypes = requestedFields.map(field => {
    const parts = field.split('.');

    return `${parts[0]}.${parts[1]}`;
  });

  return [...new Set(objecTypes)];
}

/**
 * Converts a datetime string into the YYYYMMDDHHmmss format.
 * 
 * @param {String} input A string representing a datetime. 
 * @returns {String} The specified datetime string in the YYYYMMDDHHmmss format.
 */
const getDataStudioDateTime = (input) => {
  const date = new Date(input);
  const month = date.getMonth() + 1;
  const day = date.getDate();
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const seconds = date.getSeconds();
  
  return [
    date.getFullYear(),
    (month>9 ? '' : '0') + month,
    (day>9 ? '' : '0') + day,
    (hours>9 ? '' : '0') + hours,
    (minutes>9 ? '' : '0') + minutes,
    (seconds>9 ? '' : '0') + seconds
  ].join('');
}