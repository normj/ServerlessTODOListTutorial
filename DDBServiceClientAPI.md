# CRUD operations with Amazon DynamoDB Service Client

The service client **AmazonDynamoDBClient** provides a 1 to 1 mapping of the DynamoDB service API. Using the service client gives access to the full API but using the API
is more verbose and low level. You will have to implement your own custom logic for
transforming your own data structures into the request objects used for DynamoDB.

At the service client level an item is a **Dictionary<string, AttributeValue>** where the string is an attribute name like **Name** and AttributeValue contains the value. 
AttributeValue has different property for each data type that DynamoDB supports. Only
one property should be set. 

| Property | Type |
|--|--|
| S | String |
| SS | Set of unique strings |
| N | Numeric (Note: the numeric is converted to a string on the AttributeValue)  |
| NS | Set of unique numerics |
| B | Binary |
| BS | Set of unique binaries |
| BOOL | Boolean |
| L | List of AttributeValues|
| M | Map of string to AttributeValues |


## Put Item

The **PutItem** operation either creates a new item or replaces an existing item with the item in the request. 

```cs --source-file ./Snippets/DDBServiceClientAPI.cs --project ./Snippets/Snippets.csproj --region service_client_put
```
## Update Item

The **UpdateItem** operation can be used to do a partial update of the item in DynamoDB. This is really useful when you are only changing a few fields and can save you from consuming write units for attributes that you are changing.

```cs --source-file ./Snippets/DDBServiceClientAPI.cs --project ./Snippets/Snippets.csproj --region service_client_update
```

## Get Item

Using the Hash and range key the item is returned as a **Dictionary<string, AttrubuteValue>**. 

#### AttributesToGet

If your use case only needs a limited set of attributes then by using the **AttributesToGet** property the amount of read units consumed can be reduced for just the attributes returned.

#### Consistent Read

DynamoDB is an eventually consistent system. Which means after a write operation an immediate read might not return the same data as was just written. If your application can't handle eventually consistent read then set the **ConsistentRead** property on the 
GetItemRequest to true. Using **ConsistentRead** does consume additional read unit from
the provision capacity.

```cs --source-file ./Snippets/DDBServiceClientAPI.cs --project ./Snippets/Snippets.csproj --region service_client_get
```

## Query

Query allows you search the range key within a the hash key. In this example the 
expression set for **KeyConditionExpression** only includes the hash key so all 
TODO lists will be returned for the user.

#### Paging

Many operations in AWS like the query operation use paging to support large return 
payloads. Operation that support paging will have a property on the response object containing the state of where the search stopped. To request the next page set the marker 
property from the response object to the same request object and call the operation again. For the query operation the **LastEvaluatedKey** property on the response needs to be set to the **ExclusiveStartKey** on the request.

```csharp
request.ExclusiveStartKey = response?.LastEvaluatedKey;
```

```cs --source-file ./Snippets/DDBServiceClientAPI.cs --project ./Snippets/Snippets.csproj --region service_client_query
```


## Delete Item

To delete the item pass in the table name and the key information.

```cs --source-file ./Snippets/DDBServiceClientAPI.cs --project ./Snippets/Snippets.csproj --region service_client_delete
```