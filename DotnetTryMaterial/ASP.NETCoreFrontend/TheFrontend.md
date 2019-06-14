# The Serverless TODO List Frontend

Now that we have learned how DynamoDB and Lambda works lets take a deeper look atwq the application we are building.

In our solution folder we have already taken a look at the Lambda project **ServerlessTODOList.StreamProcessor**. 
Now lets take a look at the other projects.

|Project|Description|
|-|-|
|ServerlessTODOList.Common|Defines the .NET Types we use with DynamoDB|
|ServerlessTODOList.DataAccess|Uses the Data Model API to save and retrieve the .NET Types defined in ServerlessTODOList.Common|
|ServerlessTODOList.Frontend|Our ASP.NET Core Application that we can use to view and edit our TODO Lists|

In this section we are going to focus on **ServerlessTODOList.Frontend**. The basic ASP.NET Core UI components are written but there
are a few more changes we want to make to have our ASP.NET Core application take better advantage of AWS services.

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* **The Serverless TODO List Frontend**
  * [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)
  * [Persisting ASP.NET Core Data Protection Keys](../ASP.NETCoreFrontend/ParameterStoreDataProtection.md)
  * [AWS Systems Manager Parameter Store for Managing Configuration](../ASP.NETCoreFrontend/ParameterStoreConfigurationProvider.md)
  * [ASP.NET Core wrap up](../ASP.NETCoreFrontend/FrontendWrapup.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)

