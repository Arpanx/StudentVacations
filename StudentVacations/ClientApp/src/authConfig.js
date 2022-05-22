export const msalConfig = {
    auth: {
      clientId: "3a7ae9ed-3d9d-4479-a713-928884767893",
      authority: "https://login.microsoftonline.com/0c362587-8613-4049-b12d-a128cb559263", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
      redirectUri: "https://tutorial-v2-react.azurewebsites.net/",
      redirectUri1: "http://localhost:3000/",
    },
    cache: {
      cacheLocation: "sessionStorage", // This configures where your cache will be stored
      storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
    }
  };
  
  // Add scopes here for ID token to be used at Microsoft identity platform endpoints.
  export const loginRequest = {
   scopes: ["User.Read"]
  };
  
  // Add the endpoints here for Microsoft Graph API services you'd like to use.
  export const graphConfig = {
      graphMeEndpoint: "https://graph.microsoft.com/v1.0/me"
  };