# CRUD operations with Data Model API

The Data Model api uses .NET classes to model data that will be stored in and retrieved from DynamoDB. For the 
TODO list application a list is modeled by the **TODOList** class which contains a collection of **TODOListItem** 
for each task in the list.

```cs --source-file ../Snippets/DataModelTypes.cs --project ../Snippets/Snippets.csproj --region data_model_classes --session datamodel
```

## Context Object

The **DynamoDBContext** object is used to save and load .NET object from DynamoDB. The class takes care of translating 
the data from the properties on the **TODOList** and **TODOListItem** to the service clients AttributeValue objects.

| Property | DynamoDB Type |
|----------|---------------|
| User | S |
| ListId | S |
| Complete | BOOL |
| CreateDate | S |
| UpdateDate | S |
| Items | L |
| TODOListItem.Description | S |
| TODOListItem.AssignedEmail | S |
| TODOListItem.Complete | BOOL |

**Note:** In this example the **DynamoDBContextConfig.Conversion** is set to **DynamoDBEntryConversion.V2**. This should
be set for all new applications as it supports the BOOL, L and M DynamoDB types. The original V1 conversion was 
created before those types existed and switching the default to V2 would be a breaking change to existing users.

```cs --source-file ../Snippets/DotNetDynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_client --session datamodel
```

## Save a TODOList

To save a TODOList we need to create the TODOList object and then call the SaveAsync method on the context.

```cs --source-file ../Snippets/DotNetDynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_save --session datamodel
```

## Load a TODOList

To load an object we need to call the LoadAsync method passing in the values for the key.


```cs --source-file ../Snippets/DotNetDynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_load --session datamodel
```

## Query

The QueryAsync returns back a **AsyncSearch** object. This object can be used to get a page of data over an over again 
by calling the **GetNextSetAsync**. Since we want all of the lists for the user we can skip dealing with
pagination and call **GetRemainingAsync** to get all of the lists.


```cs --source-file ../Snippets/DotNetDynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_query --session datamodel
```

## Delete a TODOList

To delete an object we need to call the DeleteAsync method passing in the values for the key.


```cs --source-file ../Snippets/DotNetDynamoDBDataModel.cs --project ../Snippets/Snippets.csproj --region datamodel_construct_delete --session datamodel
```

<!-- Generated Navigation -->
---

* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [What is Amazon DynamoDB](../DynamoDBModule/WhatIsDynamoDB.md)
  * [Using the AWS SDK for .NET](../DynamoDBModule/CreateTable.md)
  * [AWS SDK for .NET Data Plane APIs](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * **CRUD operations with Data Model API**
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Building the ASP.NET Core Frontend](../ASP.NETCoreFrontend/TheFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)

