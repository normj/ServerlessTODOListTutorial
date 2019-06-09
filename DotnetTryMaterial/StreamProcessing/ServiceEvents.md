# Using Lambda to Handle Service Events

Many AWS services trigger events when something changes. For example with Amazon S3 when a new object is uploaded 
to an S3 bucket an event is triggered.

A Lambda function can be configured to respond to the event. Here are some examples of AWS services that can be 
configured to trigger Lambda functions for their events.

* Amazon S3
* Amazon SNS
* Amazon SQS
* AWS Step Functions
* Kinesis
* Amazon DynamoDB
* [Others](https://docs.aws.amazon.com/lambda/latest/dg/lambda-services.html)

<!-- Generated Navigation -->
---

* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* **Using Lambda to Handle Service Events**
  * [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)
  * [Enable DynamoDB Stream](../StreamProcessing/EnableDynamoDBStream.md)
  * [Assign Task Lambda Function](../StreamProcessing/LookAtLambdaFunction.md)
  * [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)
  * [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)
  * [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)
  * [Tips for troubleshooting Lambda functions](../StreamProcessing/TroubleshootingLambda.md)
  * [Stream processing wrap up](../StreamProcessing/StreamProcessingWrapup.md)

Continue on to next page: [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)

