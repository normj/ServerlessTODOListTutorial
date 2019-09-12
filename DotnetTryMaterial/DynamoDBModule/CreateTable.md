# Creating DynamoDB table

## Create Table

Before we can start saving TODO lists into DynamoDB we first have to create a table. A table does not have a schema but the key 
attributes must be defined.

### Table Key

A table key is made of 2 parts. The first is a **Hash key** which is used by DynamoDB to partition the data. For best performance choose an attribute
that evenly distributes the data to avoid having large partitions or hot spots. The second part of the key is optional 
which is a **Range key**. The range key can be used to do queries within a hash key.

For our TODO List app we will use **User** as the hash key and **ListId** for the range key. This will allow us to quickly get 
the TODO lists for a particular user.

### Table Indexes
DynamoDB also supports <a href="https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/SecondaryIndexes.html" target="_blank">local indexes and global indexes</a> to provide more query capabilities. Local indexes allow you to choose another
range key with the table's hash key. A global indexes allows you choose both a new hash and range key. For the TODO list app 
we will not use indexes as we don't need them.

### Provisioning

A table has 2 possible billing modes. The first mode is called **Provisioned** where you set the read and write capacity needed for the table. Amazon CloudWatch and AWS Auto Scaling can be
used to monitor the usage versus what has been provisioned and automatically adjust the provisioning. The second mode is called 
<a href="https://aws.amazon.com/blogs/aws/amazon-dynamodb-on-demand-no-capacity-planning-and-pay-per-request-pricing/" target="_blank">On-Demand</a> where you pay per
request instead of for provision requests. DynamoDB's guidance is to use provisioned mode if the usage is predictable and on-demand when the traffic
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
DynamoDB <a href="https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/BackupRestore.html" target="_blank">developer guide</a> for details.

When you delete a table all of its data and indexes will be deleted as well.


**Note:** Only do this step if you are not going to continue through the tutorial.

```cs --source-file ../Snippets/CreateTable.cs --project ../Snippets/Snippets.csproj --region delete_table
```

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
  * **Creating DynamoDB table**
  * [Accessing DynamoDB items with the AWS SDK for .NET](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Document Model API](../DynamoDBModule/DotNetDynamoDBDocumentModel.md)
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
  * [Amazon DynamoDB wrap up](../DynamoDBModule/DynamoDBWrapUp.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Accessing DynamoDB items with the AWS SDK for .NET](../DynamoDBModule/DotNetDynamoDBAPIs.md)

