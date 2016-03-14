# Augustus

## Configuration Settings

For the Console Apps and test harnesses a connection string is used.
There are two options. The first is preferred as no password is stored. 
However it can be problematic if you have multiple accounts for testing.
To find the CRM url simply login to CRM and use the url given there of the format `https://xxx.dynamics.com`. Goto the Office 365 Admin page select Azure AD this will take you to the Azure portal. Here select the True Clarity AD, select Applications and select augustus. You will be able to get the client id and the redirect uris from here. I generally put the token cache store path in the solution folder for dev.

```
<appSettings>
  <add key="crm:ConnectionString" value="AuthType=OAuth; Url={url}; AppId={client id}; RedirectUri={redirect uris}; TokenCacheStorePath={path}; LoginPrompt=Auto"/>
</appSettings>
```

or simpler but less secure

```
<appSettings>
  <add key="crm:ConnectionString" value="AuthType=Office365; Url={url}; AppId={client id}; Username={username}; Password={password}"/>
</appSettings>
```

For the web application integration with the Azure AD means using a different method for authentication.
There are two applications registered with Azure AD. For development use `AugustusWebPortalDev` and in production use `Augustus.Web.Portal`.
The keys required are shown below.
Domain is created by the visual studio wizard but doesn't appear to be used.
You will need to create a new client secret for each development instance.

```
<appSettings>
  
  <!-- AugustusWebPortalDev -->
  <add key="crm:Url" value="{url}"/>
  <add key="ida:ClientId" value="{client id}"/>
  <add key="ida:ClientSecret" value="{client secret}"/>
  <add key="ida:AADInstance" value="https://login.microsoftonline.com/"/>
  <add key="ida:TenantId" value="{azure AD guid}"/>
  <add key="ida:PostLogoutRedirectUri" value="https://localhost:44300/"/>
  <!--<add key="ida:Domain" value="{primary domain}" />-->

</appSettings>
```

I tend to use `machine.config` found here `%WinDir%\Microsoft.NET\Framework\v4.0.30319\Config`.

## Azure AD Permissions

http://crm.davidyack.com/building-multi-tenant-web-apps-talking-to-crm/

1. Find the ‘Permissions to Other Applications’ section and add a new application
2. Select ‘Dynamics CRM Online’ and save
3. Back to the screen with the ‘Permissions to Other Applications’, add the delegate permission ‘Access CRM Online as organization users’

## Generated Code

Install the [Microsoft Dynamics CRM 2016 SDK](https://www.microsoft.com/en-us/download/details.aspx?id=50032). In the `SDK\Bin` folder create a batch file with the following. The command takes sometime to run without feedback. Be patient.

```
cd {root}/SDK/Bin
CrmSvcUtil.exe /url:{url}/XRMServices/2011/Organization.svc /out:GeneratedCode.cs /serviceContextName:CrmServiceContext /username:"{email}" /password:"{password}"
``` 

## NuGet

This is the one package required to add CRM access to a project Microsoft.CrmSdk.XrmTooling.CoreAssembly [8.0.2].  




