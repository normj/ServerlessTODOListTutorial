# Setting up Amazon Simple Email Service (SES)

SES is a cloud-based email sending service. It designed to handle any size workloads whether that is marketers
sending large amounts of marketing emails or for small applications like ours to send emails to people that 
have been assigned a new task.

AWS accounts that start using SES are initially in what is called sandbox mode, where each mail that is sent has to be
to a verified email address. The sandbox mode is used to help prevent fraud and abuse, and to help protect your reputation 
as a sender, SES applies certain restrictions to new Amazon SES accounts.

Checkout the developer guide about <a href="https://docs.aws.amazon.com/ses/latest/DeveloperGuide/setting-up-email.html" target="_blank">setting up email</a> or <a href="https://docs.aws.amazon.com/ses/latest/DeveloperGuide/request-production-access.html" target="_blank">moving your account of the sandbox</a>.

## Verifing Email Address

Your account is most likely in the sandbox mode. To see the emails our Lambda function will send, you
need to verify each of the emails you plan on testing with both the From and To addresses. You can verify
emails either through the <a href="https://console.aws.amazon.com/ses/home?region=us-east-1#verified-senders-email:" target="_blank">AWS console</a> or by entering an email in the code snippet below and executing it.

```cs --source-file ../Snippets/SESSnippets.cs --project ../Snippets/Snippets.csproj --region send_verification_email
```

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial?](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
  * [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)
  * [Enable DynamoDB Stream](../StreamProcessing/EnableDynamoDBStream.md)
  * [Assign Task Lambda Function](../StreamProcessing/LookAtLambdaFunction.md)
  * [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)
  * **Setting up Amazon Simple Email Service (SES)**
  * [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)
  * [Tips for troubleshooting Lambda functions](../StreamProcessing/TroubleshootingLambda.md)
  * [Stream processing wrap up](../StreamProcessing/StreamProcessingWrapup.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)

