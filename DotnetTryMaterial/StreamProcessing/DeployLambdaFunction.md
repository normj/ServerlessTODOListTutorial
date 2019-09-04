# Deploy Lambda Function

To deploy the Lambda function we can use either the [AWS Toolkit for Visual Studio](https://marketplace.visualstudio.com/items?itemName=AmazonWebServices.AWSToolkitforVisualStudio2017) or the 
[Amazon.Lambda.Tools](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) .NET Core global tool.

## AWS Identity and Access Management (IAM) Role

When this function is deployed an IAM role is required. The role provides AWS credentials to the Lambda function 
that can be used to access other AWS services. When you construct a service client from the 
AWS SDK for .NET without specifing credentials the SDK will locate the credentials for the assigned role.

All roles used for Lambda should have access to **CloudWatch Logs** to support Lambda saving logs. In this Lambda
function we also need to access to **Amazon Simple Email Service** to send emails and read access for the **DynamoDB Stream**.

If you don't have an IAM role that has the required permissions then skip down to the [Create Role](#create-role)
section at the bottom of this page to create an IAM role and then return back here.

## Visual Studio

![Publish from solution explorer](./images/SolutionExplorerPublishToLambda.png)

In the solution explorer right click on the **ServerlessTODOList.StreamProcessor** project and select 
**Publish to AWS Lambda**. This will launch the deployment wizard.

### Wizard Page 1

![Lambda Wizard Page 1](./images/LambdaWizardPage1.png)

In the first page we need to specify the name of the Lambda function and identify what .NET method to call. 
To identify the .NET method we have to set the Assembly, Type and Method name. To Lambda this will
get translated to the **Handler** field with a value of &lt;assembly-name>::&lt;type-name>::&lt;method-name>.

### Profile and Region
Be sure the profile and region are set to the profile and region that was set at the beginning of the tutorial and is where
the DynamoDB table was created.

### Persist Settings
Note the bottom checkbox to **"Save settings to aws-lambda-tools-defaults.json for future deployments"**.
With this value checked all of the settings made in the wizard will be persisted into the aws-lambda-tools-defaults.json
file. This makes it so redeployments can reuse all of the previous settings as well make it easy to switch to
the **Amazon.Lambda.Tools** .NET Core global tool.


### Wizard Page 2

![Lambda Wizard Page 2](./images/LambdaWizardPage2.png)

### IAM Role
The only required field that must be set is the IAM role that you want to use to 
provide AWS credentials to the Lambda function.

### Memory
This controls how much memory will be allocated to Lambda function while it is processing an event. The amount of memory you allocate also controls
how much CPU power the Lambda function will get. If a Lambda function is not processing fast enough for your requirements try increasing the
memory settings for the function.

### Timeout
How much time a Lambda function will have to process an event before Lambda will cancel the process.

### VPC Subnets and Security Groups
If a function needs to access resources that are inside a VPC, like an RDS database, then select subnets and security groups for the VPC to allow
the Lambda function to access the resources. An Elastic Network Interface (ENI) will be attached to the VPC to provide access into the VPC. Additional IAM
permissions are required for the IAM role to create the ENI. Check out the Lambda [developer guide](https://docs.aws.amazon.com/lambda/latest/dg/vpc.html) for more info.

### DLQ Resource
Dead Letter Queue (DLQ) can be either an SQS queue or SNS Topic. When a Lambda function throws an unhandled exception while processing an event Lambda will retry the event a few times. If the function still fails Lambda will send the event to the DLQ if one is assigned. This can be useful troubleshooting and debugging failed events.

### Environment
Configuration values can be set as environment variables for the Lambda function.

## Push Upload

Once you push upload under the covers the tooling will execute the `dotnet publish` command to build
the project and then create zip file of the .NET assembly and all of its required dependencies.

## Amazon.Lambda.Tools .NET Core Global Tool

If you don't have Visual Studio or you want to automate the deployment the **Amazon.Lambda.Tools** .NET Core Global Tool can be used.

To ensure you have installed Amazon.Lambda.Tools execute the following command.

```
dotnet tool install -g Amazon.Lambda.Tools
```

The **aws-lambda-tools-defaults.json** will already have defaults for most all of the values required
to create the Lambda function. The only values that need to set are the Lambda function name and IAM Role.

To deploy the function execute the command from the directory of **ServerlessTODOList.StreamProcessor** execute the following command.

```
dotnet lambda deploy-function ServerlessTODOListStreamProcessor --function-role <iam-role-name>
```






## Create Role

This code can be used to create an IAM Role with the appropriate permissions required for 
the ServerlessTODOList.StreamProcessor Lambda function. If you already have a role then this
section is not necessary 
**ServerlessTODOList.StreamProcessor** function.

```cs --source-file ../Snippets/IAMRoleSetups.cs --project ../Snippets/Snippets.csproj --region setup_streamprocessor_role
```

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
  * **Deploy Lambda Function**
  * [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)
  * [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)
  * [Tips for troubleshooting Lambda functions](../StreamProcessing/TroubleshootingLambda.md)
  * [Stream processing wrap up](../StreamProcessing/StreamProcessingWrapup.md)
* [Building the ASP.NET Core Frontend](../ASP.NETCoreFrontend/TheFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)

