# CRUD operations with Document Model API

The Document Model API is useful when the data for your DynamoDB items can be different for each 
item. You use a **Document** object to store unstructured data very similar to working with a 
IDictionary<string, object>.

## The Table class

The Table class is the main entry point when using the Document Model API. You use the Table class to
perform all of your data operations on the items stored in the DynamoDB table. Under the covers
the Table class uses the IAmazonDynamoDB service client we saw in the previous section to interact
with the DynamoDB service.

The Table class translates all of the fields set on the Document object to the underlying DynamoDB types
for the AttributeValue class we saw in the previous section.

### Loading the Table

To load the use the `LoadTable` or `TryLoadTable` static method on Table class. Loading the
table **does not** mean load the data. When you call the load method it will load the
meta data for the table. This includes the key and index information.

```csharp
var table = Table.LoadTable(dynamoDBServiceClient, "TODOList", DynamoDBEntryConversion.V2);
```

**Note:** In this example **DynamoDBEntryConversion.V2** is passed into the load method. This should
be set for all new applications as it supports the BOOL, L and M DynamoDB types. The original V1 conversion was 
created before those types existed and switching the default to V2 would be a breaking change to existing users.

## Put TODO List

The example shows how to save a new TODO List. Notice how the child tasks stored with the TODO List Document as documents
them selves. Under the covers the Table object is going to translate the task to the DynamoDB types: **Tasks => L (List) -> M (Map) -> (S, B)**

```cs --source-file ../Snippets/DynamoDBDocumentModel.cs --project ../Snippets/Snippets.csproj --region document_model_put
```

## Get TODO List

To get the item back call the `GetItemAsync` method from the Table which will return back the Document. Casting can be
used when accessing simple fields like string, int, DateTime and Bool. For complex data like we need to use operations
like `AsListOfDocument` to convert the data back to a list of documents. Check out the intellisense to see the
other **As** method to see how you can force conversion.

```cs --source-file ../Snippets/DynamoDBDocumentModel.cs --project ../Snippets/Snippets.csproj --region document_model_get
```

## Query a user's TODO List

This example shows how to perform a query. Remember a query is when you search in your DynamoDB table using the range key under a certain hash key.

When you start a query it returns back a **Search** object. The data from DynamoDB will be returned back in pages. To get a page of 
data call the `GetNextSetAsync` method. In our case we want to return back all of the lists for a user so we call `GetRemainingAsync`
which will traverse through all of the pages to return our data. 

```cs --source-file ../Snippets/DynamoDBDocumentModel.cs --project ../Snippets/Snippets.csproj --region document_model_query
```

## Delete a TODO List

To delete an item call the `DeleteItemAsync` method passing in the key information.

```cs --source-file ../Snippets/DynamoDBDocumentModel.cs --project ../Snippets/Snippets.csproj --region document_model_delete
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
  * **CRUD operations with Document Model API**
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
  * [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)

