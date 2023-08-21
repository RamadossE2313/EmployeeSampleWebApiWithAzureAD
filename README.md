# EmployeeSampleWebApiWithAzureAD
In this application web api is proctected using AzureAd and token generated through swagger and calling the web api

**Understanding about how the authentication works with azure ad**
When client send the request to web api with token (Bearer token), first we have to validate the token
   --> to validate we are can use openid connect provider(it contains some steps/process to validate the token)
		--> jwt keys --> we have to download this key and cached in the system, later it will be used to make outbound call your azure ad
		--> decode the token --> it contains three parts (eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9(Header)
			.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ (Payload
			.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c (Signature))
		--> verify the signature --> to make sure the signature is correct or not modified
		--> verify the claims --> what is the user, what is the access that user has it, etc

**Steps to follow to secure web api using AzureAD**

1. Create two app registrations

   > EmployeeWebApi( Webapi without redirect uri)
   > EmployeeClientApp(Swagger)( webapplication with redirect uri)
   
**(EmployeeWebApi app Registration)**
2. Click Expose an Api button  --> Set application ID URI and click save button.
3. Click Expose an Api button  --> Click add a scope button and provide the scope information and click add scope button.
4. Click Expose an Api button  --> Click add a client application button and provide the clientapp(EmployeeClientApp(Swagger)) url and select the scope and click the add application. 

**EmployeeClientApp(Swagger)**
5. Click Add a Permission an Api button --> Select My APIs --> Select WebApi and select Permission and click Add Permission button.
6. Click Authentication button --> Click add a platform button and select singe page applications and provide redirect url (https://localhost:yourport/swagger/oauth2-redirect.html) and select access token(used for implicit flows).

**.Net core application web api changes**

https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Program.cs

**AppSetting.json**

 "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId": "",
    "TenantId": "",
    "Scopes": "",
    "SwaggerClientId": ""
  }
