/**
 * Optional function which toggles debug features.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#isadminuser}
 * @returns `true` if debug features should be enabled.
 */
const isAdminUser = () => {
  return true;
}
  
/**
 * Required function which sets the connector's authentication type.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#getauthtype}
 * @returns A {@link https://developers.google.com/apps-script/reference/data-studio/get-auth-type-response GetAuthTypeResponse}.
 */
const getAuthType = () => {
  return connector
    .newAuthTypeResponse()
    .setAuthType(AuthTypes.PATH_USER_PASS)
    .build();
}

/**
 * Gets the user's credentials for Basic authentication.
 * 
 * @returns The user's credentials from {@link https://developers.google.com/apps-script/reference/properties/properties-service#getUserProperties() user properties}.
 */
const getCredentials = () => {
  const properties = PropertiesService.getUserProperties();

  return {
    path: properties.getProperty(PROPERTY_USERPATH),
    username: properties.getProperty(PROPERTY_USERNAME),
    password: properties.getProperty(PROPERTY_PASSWORD)
  };
};

/**
 * Required function which stores the user's credentials.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#setcredentials}
 * @param {Object} request Data request parameters.
 */
const setCredentials = (request) => {
  let path = request.pathUserPass.path;
  if (path.endsWith('/')) {
    path = path.slice(0,-1);
  }

  const username = request.pathUserPass.username;
  const password = request.pathUserPass.password;
  const isValid = validateCredentials(path, username, password);

  if (isValid) {
    PropertiesService
      .getUserProperties()
      .setProperty(PROPERTY_USERPATH, path)
      .setProperty(PROPERTY_USERNAME, username)
      .setProperty(PROPERTY_PASSWORD, password);
  }

  return connector.newSetCredentialsResponse()
    .setIsValid(isValid)
    .build();
};

/**
 * Required function which clears the user's credentials.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#resetauth}
 */
const resetAuth = () => {
  PropertiesService
    .getUserProperties()
    .deleteProperty(PROPERTY_USERPATH)
    .deleteProperty(PROPERTY_USERNAME)
    .deleteProperty(PROPERTY_PASSWORD);
}
  
/**
 * Required function which validates the user's credentials.
 * 
 * @see {@link https://developers.google.com/datastudio/connector/reference#isauthvalid}
 * @returns {Boolean} `true` if the credentials are valid.
 */
const isAuthValid = () => {
  const credentials = getCredentials();

  return validateCredentials(credentials.path, credentials.username, credentials.password);
}
