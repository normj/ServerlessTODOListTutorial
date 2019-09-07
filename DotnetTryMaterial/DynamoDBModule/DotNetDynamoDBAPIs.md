# Accessing DynamoDB items with the AWS SDK for .NET

The NuGet package [AWSSDK.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2/) contains 3 different APIs save and get items from tables. In our TODO list application we will use the Data Model API but we will briefly cover each of the 3 API to get an understanding what is available.


| Name           | Namespace | Description|
|----------------|-----------|------------|
| Service Client | <ul><li>Amazon.DynamoDBv2</li><li>Amazon.DynamoDBv2.Model</li></ul> | The **AmazonDynamoDBClient** service client provides a 1 to 1 mapping with the service APIs. |
| Document Model | <ul><li>Amazon.DynamoDBv2.DocumentModel</li></ul> | Represents an item as a Document which is similar to dictionary. Recommended when the data stored for each item varies with different attributes. |
| Data Model     | <ul><li>Amazon.DynamoDBv2.DataModel</li></ul> | Model your data as .NET objects and use **DynamoDBContext** object to serialize and deserialize the .NET objects into DynamoDB. |

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [What is Amazon DynamoDB](../DynamoDBModule/WhatIsDynamoDB.md)
  * [Creating DynamoDB table](../DynamoDBModule/CreateTable.md)
  * **Accessing DynamoDB items with the AWS SDK for .NET**
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
  * [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)

