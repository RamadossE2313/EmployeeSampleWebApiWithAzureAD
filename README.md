# EmployeeSampleWebApiWithAzureAD
In this application web api is proctected using AzureAd and token generated through swagger and calling the web api

**Steps to follow to secure web api using AzureAD**
<h4>Create two app registrations</h4>
  <ul>
	  <li>EmployeeWebApi( Webapi without redirect uri)
	   <img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture1.png"/></li>
	  <li>EmployeeClientApp(Swagger)( webapplication with redirect uri)<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture4.png"/></li>
  </ul>
   
**(EmployeeWebApi app Registration)**
<ul>
	<li>Click Expose an Api button  --> Set application ID URI and click save button.
<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture2.png"/></li>
	<li>Click Expose an Api button  --> Click add a scope button and provide the scope information and click add scope button
	<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture3.png"/></li>
	<li>Click Expose an Api button  --> Click add a client application button and provide the clientapp(EmployeeClientApp(Swagger)) url and select the scope and click the add application<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture7.png"/></li>
</ul>

**EmployeeClientApp(Swagger)**
<ul>
	<li>Click Add a Permission an Api button --> Select My APIs --> Select WebApi and select Permission and click Add Permission button.
	<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture5.png"/>
	<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture6.png"/>
</li>
	<li>Click Authentication button --> Click add a platform button and select singe page applications and provide redirect url 	(https://localhost:yourport/swagger/oauth2-redirect.html) and select access token(used for implicit flows).
<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture8.png"/>
<img src="https://github.com/RamadossE2313/EmployeeSampleWebApiWithAzureAD/blob/master/Images/Picture9.png"/></li>
</ul>

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

**Mistake:**
<b>RedirecUrI: https://localhost:7279/swagger/oauth2-redirect.html</b>

<h4>Blog Post: <a href="https://rishiram2313.blogspot.com/2023/08/securing-web-api-using-azure-ad-and.html">securing-web-api-using-azure-ad-and.html</a></h4>
