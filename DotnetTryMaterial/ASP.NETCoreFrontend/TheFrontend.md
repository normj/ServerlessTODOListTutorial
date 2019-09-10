# Getting ASP.NET Core ready for Serverless

The **ServerlessTODOList.Frontend** project in our solution is an ASP.NET Core application that will allow us to manage our TODO lists. The application
is setup very similar to a the initial ASP.NET Core project created in Visual Studio using SQL Server for identity and local configuration files. In this
section we will focus on making this application ready to run in AWS and replace SQL Server and local configuration files with AWS serverless technology.

### Setting Region and Profile

Before we can run the application locally we need to configure in our application what AWS region and profile to use. The region and profile we use 
for development might be different then what we use when deployed to AWS. So we will configure the region and profile in the **appsettings.Development.json** file
so that region and profile are only set during development. ASP.NET Core will ignore this file when not run in development. 

In the **appsettings.Development.json** file add the **Region** and **Profile** field in the AWS section like you see below and set the values of those fields to the same value used
as the start of this tutorial.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-ServerlessTODOList-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AWS": {
    "Region": "",
    "Profile": ""
  }
}
```

Alternatively to using **appsettings.Development.json** you could also set these values in User Secrets.

### Run the app

As mentioned before the application is currently configured to use SQL Server for identity. To ensure the database exist locally run the command below in the project directory
to create the database. Once that is done you should be able to run the application locally.

```
dotnet ef database update
```

If you don't have the **dotnet ef** available or are getting errors with it don't worry. Its not important and we will be removing 
the SQL Server dependency very soon in this tutorial.

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* **Getting ASP.NET Core ready for Serverless**
  * [Dependency Injection](../ASP.NETCoreFrontend/DependencyInjection.md)
  * [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)
  * [Persisting ASP.NET Core Data Protection Keys](../ASP.NETCoreFrontend/ParameterStoreDataProtection.md)
  * [AWS Systems Manager Parameter Store for Managing Configuration](../ASP.NETCoreFrontend/ParameterStoreConfigurationProvider.md)
  * [ASP.NET Core wrap up](../ASP.NETCoreFrontend/FrontendWrapup.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Dependency Injection](../ASP.NETCoreFrontend/DependencyInjection.md)

