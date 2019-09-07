# Stream processing wrap up

Now we have completed deploying a Lambda function that handles processing service events. In this example we 
are handled changes done to our DynamoDB Stream but we could just as easily used Lambda to handle events 
from S3 when objects are upload or when messages are sent to a SQS queues. Lambda takes care of 
our scaling needs we can focus on our business logic.

To recap in this section we covered:

* DynamoDB Streams
* Deploying Lambda Functions
* Configuring Event Sources for Lambda Functions
* Testing and Troubleshooting of Lambda functions

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Using Lambda to Handle Service Events](../StreamProcessing/ServiceEvents.md)
  * [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)
  * [Enable DynamoDB Stream](../StreamProcessing/EnableDynamoDBStream.md)
  * [Assign Task Lambda Function](../StreamProcessing/LookAtLambdaFunction.md)
  * [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)
  * [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)
  * [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)
  * [Tips for troubleshooting Lambda functions](../StreamProcessing/TroubleshootingLambda.md)
  * **Stream processing wrap up**
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)

