# Augustus

## Connection String

To find the CRM url simply login to CRM and use the url given there of the format `https://xxx.dynamics.com`. Goto the Office 365 Admin page select Azure AD this will take you to the Azure portal. Here select the True Clarity AD, select Applications and select augustus. You will be able to get the client id and the redirect uris from here. I generally put the token cache store path in the solution folder for dev.

```
<appSettings>
    <add key="augustusCRM" value="AuthType=OAuth;Url={url};AppId={client id};RedirectUri={redirect uris};TokenCacheStorePath={path};LoginPrompt=Auto"/>
  </appSettings>
```

## Generated Code

Install the [Microsoft Dynamics CRM 2016 SDK](https://www.microsoft.com/en-us/download/details.aspx?id=50032). In the `SDK\Bin` folder create a batch file with the following. The command takes sometime to run without feedback. Be patient.

```
cd {root}/SDK/Bin
CrmSvcUtil.exe /url:{url}/XRMServices/2011/Organization.svc /out:GeneratedCode.cs /username:"{email}" /password:"{password}"
```  




