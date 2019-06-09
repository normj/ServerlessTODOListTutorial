# Enable DynamoDB Stream

A DynamoDB Stream is enabled by updating the DynamoDB table.

### Stream View Type

When you configure an event you can choose what data is sent in the event.

| View Type | Description |
|-|-|
|KEYS_ONLY| Only the keys are are sent into the event. |
|NEW_IMAGE| The entire new version of the item in DynamoDB |
|OLD_IMAGE| The entire old version of the item in DynamoDB |
|NEW_AND_OLD_IMAGES| Both the old and new versions of the item are sent in the event payload |

For our use case we need both the old and new versions of the item to compare any changes to the task assignments.


```cs --source-file ../Snippets/EnableDynamoDBStream.cs --project ../Snippets/Snippets.csproj --region enable_stream_status
```

## Check Status

Like updating a table's provisioning when enable a stream it will change the status of the table to updating while 
the service provisions the stream. The table will be accessible while the stream is being enabled.


```cs --source-file ../Snippets/EnableDynamoDBStream.cs --project ../Snippets/Snippets.csproj --region check_stream_status
```


## Testing DynamoDB Stream

This code below a is simplistic version of what it takes to read from a DynamoDB stream that does not
handle failures scaling issues. When we use Lambda to handle reading from a DynamoDB Stream all we have to write 
is what the `foreach` inside the `shardReader` Func<>. Lambda takes care of managing the scaling and failures 
for the shards of a DynamoDB Stream.

```cs --source-file ../Snippets/EnableDynamoDBStream.cs --project ../Snippets/Snippets.csproj --region test_stream_read
```

<!-- Generated Navigation -->
---

* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Using Lambda to Handle Service Events](../StreamProcessing/ServiceEvents.md)
  * [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)
  * **Enable DynamoDB Stream**
  * [Assign Task Lambda Function](../StreamProcessing/LookAtLambdaFunction.md)
  * [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)
  * [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)

Continue on to next page: [Assign Task Lambda Function](../StreamProcessing/LookAtLambdaFunction.md)

