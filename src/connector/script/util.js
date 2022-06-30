/**
 * Throws an exception, logs debug text, and displays a message to the user.
 * 
 * @param {String} debugText The text to add to the script execution log.
 * @param {String} userText The text to display to the user.
 * @throws {@link https://developers.google.com/apps-script/reference/data-studio/user-error UserError}
 */
const throwError = (debugText, userText) => {
  connector.newUserError()
    .setDebugText(debugText)
    .setText(userText)
    .throwException();
}

const getRequestedObjectTypes = (request) => {
  const requestedFields = request.fields.map(field => field.name);
  const objecTypes = requestedFields.map(field => {
    const parts = field.split('.');

    return `${parts[0]}.${parts[1]}`;
  });

  return [...new Set(objecTypes)];
}

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