# Augustus

## Connection String

To find the CRM url simply login to CRM and use the url given there of the format `https://xxx.dynamics.com`. Goto the Office 365 Admin page select Azure AD this will take you to the Azure portal. Here select the True Clarity AD, select Applications and select augustus. You will be able to get the client id and the redirect uris from here. I generally put the token cache store path in the solution folder for dev.

```
<appSettings>
    <add key="augustusCRM" value="AuthType=OAuth;Url={url};AppId={client id};RedirectUri={redirect uris};TokenCacheStorePath={path};LoginPrompt=Auto"/>
  </appSettings>
```




