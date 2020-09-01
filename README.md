
# Blog System

Demo runs here : [https://blogdemoclient.azurewebsites.net/](https://blogdemoclient.azurewebsites.net/)

## Tech stack

- ASP.NET Core 2.1 Web API
- Entity Framework Core + Sqlite
- Identity Server 4 (oauth/oidc based)
- FluentValidation
- Unit of Work + Repository DIP The Dependency Inversion Principle (DIP)
- Using Serilog for the Global Error Handling and Logging
- restful API (using HATEOAS = Hypermedia As The Engine Of Application State,Media Type Vendor-specific media type)
- Angular 6 + Angular Material

## Getting started

1. fork the project

2. git clone project

3.  `cd to the api project folder`,run `dotnet watch run` to run the API server `http://localhost:6001`

4.  `cd to the BlogIdp project folder`,run `dotnet watch run` to run the BlogIdp server `http://localhost:5001`. Same way to run the MVCclient `http://localhost:7001`,it is test part for the API and Idp server.

5. `cd to the client project folder`

6.  `npm install`

7.  `ng serve` to start the front-end project ，then visit `http://localhost:4200`

  

## Steps to deploy on Azure Web App
1.   Run `ng build` to build the client project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

2.  Create the apps on Azure. Deploy the dist folder to the web app. Make sure put the web.config file under the`wwwroot/` as well. Angular on azure need the web.config for routing or read json file(local run ng serve is different with using IIS,need to add web.config ).By the way, WebApp windows environment need add the node.js version app settings.

3. Deploy the project using SQLite in .NET Core Azure Web App 
	#### 1. Create a Release Package

	Open your terminal and make sure you're in the right folder.  

	Run the following command to create a release package in a sub folder called publish:

	```dotnet publish -c Release -o ./publish```

	#### 2. Copy database to the new folder
	Copy-Paste your database, for example 'AspIdUsers.db', into the newly created 'publish' folder.
	#### 3. Publish to Azure

	There are few ways to publish to Azure.Choose anyone if you like.
     I always use Visual Studio Code and have the Azure App Service extension. 
     #### 4. Set the Connection String

	Once the app has been deployed to Azure, you set your connection string in the Azure App Service.

	You can find the setting under  _Settings -> Configuration -> Connection strings._	There you create a new connection string
		

		[
			{
			"name": "ConnectionStrings",
			"value": "{ \"DefaultConnection\": \"Data Source=AspIdUsers.db;\" }",
			"type": "Custom",
			"slotSetting": true
			}
		]

	 #### 5. Set the Application settings
	 
	 You can find the setting under _Settings -> Configuration -> Applications Settings 
	1. Adding the settings to web apps for your related API address and BlogIdp address.	

			[
				 {
					"name": "APIAddress",
					"value":"https://blogdemoapi.azurewebsites.net",
					"slotSetting": true
				 }
			]
							
	

	3. Adding `ASPNETCORE_DETAILEDERRORS = true` to show the errors.
 	

			[
				 {
					"name": "ASPNETCORE_DETAILEDERRORS",
					"value": "true",
					"slotSetting": true
				 }
			]
	3. Adding the `ASPNETCORE_ENVIRONMENT = Development`. Restart the WebApp and then check to see if that works.