# CRUD operations with Data Model API

The Data Model api uses .NET classes to model data that will be stored in and retrieved from DynamoDB. For the 
TODO list application a list is modeled by the **TODOList** class which contains a collection of **TODOListItem** 
for each task in the list.

```cs --source-file ../Snippets/DataModelTypes.cs --project ../Snippets/Snippets.csproj --region data_model_classes --session datamodel
```

## Context Object

The **DynamoDBContext** object is used to save and load .NET objects from DynamoDB. **DynamoDBContext** takes care of translating 
the data from the properties on the **TODOList** and **TODOListItem** to the service clients AttributeValue objects.

| Property | DynamoDB Type |
|----------|---------------|
| User | S |
| ListId | S |
| Complete | BOOL |
| CreateDate | S |
| UpdateDate | S |
| Items | L<M> |
| -> TODOListItem.Description | S |
| -> TODOListItem.AssignedEmail | S |
| -> TODOListItem.Complete | BOOL |

**Note:** In this example the **DynamoDBContextConfig.Conversion** is set to **DynamoDBEntryConversion.V2**. This should
be set for all new applications as it supports the BOOL, L and M DynamoDB types. The original V1 conversion was 
created before those types existed and switching the default to V2 would be a breaking change to existing users.

```cs --source-file ../Snippets/DynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_client --session datamodel
```

## Save a TODO List

To save a TODOList we need to create the TODOList object and then call the SaveAsync method on the context.

```cs --source-file ../Snippets/DynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_save --session datamodel
```

## Load a TODO List

To load an object we need to call the LoadAsync method passing in the values for the key.


```cs --source-file ../Snippets/DynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_load --session datamodel
```

## Query a user's TODO List

The QueryAsync returns back a **AsyncSearch** object. This object can be used to get a page of data over an over again 
by calling the **GetNextSetAsync**. Since we want all of the lists for the user we can skip dealing with
pagination and call **GetRemainingAsync** to get all of the lists.


```cs --source-file ../Snippets/DynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_query --session datamodel
```

## Delete a TODO List

To delete an object we need to call the DeleteAsync method passing in the values for the key.


```cs --source-file ../Snippets/DynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_delete --session datamodel
```

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
  * [Creating DynamoDB table](../DynamoDBModule/CreateTable.md)
  * [Accessing DynamoDB items with the AWS SDK for .NET](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Document Model API](../DynamoDBModule/DotNetDynamoDBDocumentModel.md)
  * **CRUD operations with Data Model API**
  * [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)

