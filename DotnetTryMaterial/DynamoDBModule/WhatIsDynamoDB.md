# What is Amazon DynamoDB

As we mentioned before we are going to use [DynamoDB](https://aws.amazon.com/dynamodb/) to store our TODO list. DynamoDB was chosen because it provides us
fast access to our data, it simplifies our development with its schemaless design and provisioning without managing servers.

## Fully managed NoSQL datastore
* Define Key and Indexes
* Provisioning
  * Write / Read capacity units
  * Pay per request

## Fast performance
* Provides single-digit millisecond performance
* Amazon DynamoDB Accelerator (DAX) for even faster performance.
  * .NET NuGet package: [AWSSDK.DAX.Client](https://www.nuget.org/packages/AWSSDK.DAX.Client/)

## Support multi region
*  Multi region replication
*  Multi master


## AWS SDK for .NET Access

To access DynamoDB with .NET the NuGet package [AWSSDK.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2/) is used to access all of the operations the DynamoDB service provides. This includes both the control plane API like creating tables as well as the data plane API for putting and getting items from tables.

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* **What is Amazon DynamoDB**
  * [Creating DynamoDB table](../DynamoDBModule/CreateTable.md)
  * [Accessing DynamoDB items with the AWS SDK for .NET](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
  * [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Building the ASP.NET Core Frontend](../ASP.NETCoreFrontend/TheFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Creating DynamoDB table](../DynamoDBModule/CreateTable.md)

