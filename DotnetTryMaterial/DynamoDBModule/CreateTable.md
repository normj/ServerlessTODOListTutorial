# Using the AWS SDK for .NET

The NuGet package [AWSSDK.DynamoDBv2](https://www.nuget.org/packages/AWSSDK.DynamoDBv2/) is used to access all of the operations the DynamoDB service
provides. This includes both the control plane API like creating tables as well as the data plane API for putting and getting items from tables.

## Create Table

Before we can start saving TODO lists into DynamoDB we first have to create a table. A table does not require a schema but the key 
attributes must be defined.

### Table Key

A key is made of 2 parts. The first is a **Hash key** which is used by DynamoDB to partition the data. For best performance choose an attribute
that evenly distributes the data to avoid having large partitions that can cause hot spots. The second part of the key is optional 
which is a **Range key**. The range key can be used to do queries within a hash key.

For our TODO List app we are using to use **User** as the hash key and **ListId** for the range key. This will allow us to quickly get 
the TODO lists for a particular user.

### Table Indexes
DynamoDB also supports [local indexes and global indexes](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/SecondaryIndexes.html) to provide more query capabilities. Local indexes allow you to choose another
range key along with the table hash key and a global indexes allows you choose both a new hash and range key. For the TODO list app 
we will not use indexes.

### Provisioning

A table has 2 possible billing modes. The first mode called Provisioned where you set the read and write capacity needed for the table. Amazon CloudWatch and Autoscaling can be
used to monitor the usage versus provisioned and automatically adjust the provisioning. The second mode is called 
[On-Demand](https://aws.amazon.com/blogs/aws/amazon-dynamodb-on-demand-no-capacity-planning-and-pay-per-request-pricing/) where you pay per
request instead of for provision requests. The guidance is to use provisioned mode if the usage is predictable and on-demand when the traffic
is difficult to predict.


```cs --source-file ../Snippets/CreateTable.cs --project ../Snippets/Snippets.csproj --region create_table
```


## Check Status

When a table is created or modified the change does not happen instantly. We can describe the table to get details about the table as 
well as it's current status.

```cs --source-file ../Snippets/CreateTable.cs --project ../Snippets/Snippets.csproj --region check_status
```

## Update Table

Once a table is created the provisioning can be updated. The table will go into an **UPDATING** status while the DynamoDB service takes
care of the reprovisioning. The table will still be accessible during the reprovisioning process.

```cs --source-file ../Snippets/CreateTable.cs --project ../Snippets/Snippets.csproj --region update_table
```



## Delete Table

Before deleting a table a backup can be made and the later restored to a new table. Checkout the 
DynamoDB [developer guide](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/BackupRestore.html) for details.

When you delete a table all of its data and indexes will be deleted as well.


**Note:** Only do this step if you are not going to continue through the tutorial.

```cs --source-file ../Snippets/CreateTable.cs --project ../Snippets/Snippets.csproj --region delete_table
```

<!-- Generated Navigation -->
---

* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [What is Amazon DynamoDB](../DynamoDBModule/WhatIsDynamoDB.md)
  * **Using the AWS SDK for .NET**
  * [AWS SDK for .NET Data Plane APIs](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)

Continue on to next page: [AWS SDK for .NET Data Plane APIs](../DynamoDBModule/DotNetDynamoDBAPIs.md)

