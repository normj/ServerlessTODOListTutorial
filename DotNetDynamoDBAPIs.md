# AWS SDK for .NET Data Plane APIs

The NuGet package [AWSSDK.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2/) contains 3 different APIs save and get items from tables. 


| Name           | Namespace | Description|
|----------------|-----------|------------|
| Service Client | <ul><li>Amazon.DynamoDBv2</li><li>Amazon.DynamoDBv2.Model</li></ul> | The **AmazonDynamoDBClient** service client provides a 1 to 1 mapping with the service APIs. |
| Document Model | <ul><li>Amazon.DynamoDBv2.DocumentModel</li></ul> | Represents an item as a Document which is similar to dictionary. Recommended when the data stored for each record varies. |
| Data Model     | <ul><li>Amazon.DynamoDBv2.DataModel</li></ul> | Model your data as .NET objects and use **DynamoDBContext** object to serialize and deserialize the .NET objects into DynamoDB. |
