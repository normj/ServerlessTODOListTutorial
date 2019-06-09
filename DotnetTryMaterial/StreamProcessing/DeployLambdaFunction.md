# Deploy Lambda Function

To deploy the Lambda function we can use either the [AWS Toolkit for Visual Studio](https://marketplace.visualstudio.com/items?itemName=AmazonWebServices.AWSToolkitforVisualStudio2017) or the 
[Amazon.Lambda.Tools](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) .NET Core global tool.

## AWS Identity and Access Management (IAM) Role

When this function is deployed an IAM role is required. The role provides AWS credentials to the Lambda function 
that can be used to access other AWS services. When you construct a service client from the 
AWS SDK for .NET without specifing credentials the SDK will locate the credentials for the assigned role.

All roles used for Lambda should have access to CloudWatch Logs to support Lambda saving logs. In this Lambda
function we also need to access to Amazon Simple Email Service to send emails.

If you don't have an IAM role that has the required permissions skipped the [Create Role](#create-role)
section of this page to create an IAM role and then return back here.

## Visual Studio

In the solution explorer right click on the **ServerlessTODOList.StreamProcessor** project and select 
**Publish to AWS Lambda**. This will launch the deployment wizard.


![Publish from solution explorer](./images/SolutionExplorerPublishToLambda.png)

### Wizard Page 1

In the first page we need to specify the name of the Lambda function and identify what .NET method to call. 
To identify the .NET method we have to set the Assembly, Type and Method name. To Lambda this will
get translated to the **Handler** field with a value of <assembly-name>::<type-name>::<method-name>.

Note the bottom checkbox to **"Save settings to aws-lambda-tools-defaults.json for future deployments"**.
With this value checked all of the settings made in the wizard will be persisted into the aws-lambda-tools-defaults.json
file. This makes it so redeployments can reuse all of the same previous settings as well make it easy to switch to
the **Amazon.Lambda.Tools** .NET Core global tool.

![Lambda Wizard Page 1](./images/LambdaWizardPage1.png)

### Wizard Page 2

The only required field that must be set is the IAM role that you want to use to 
provide AWS credentials to the Lambda function.

![Lambda Wizard Page 2](./images/LambdaWizardPage2.png)


### Push Upload

Once you push upload under the covers the tooling will execute the `dotnet publish` command to build
the project and then create zip file of the .NET assembly and all of its required dependencies.

## Amazon.Lambda.Tools .NET Core Global Tool

If you don't have Visual Studio or you want to automate the deployment the **Amazon.Lambda.Tools** .NET Core Global Tool 
capture the Lambda deployment tooling from Visual Studio into a command line experience.

To ensure you have installed Amazon.Lambda.Tools exeute the following command.

```
dotnet tool -g Amazon.Lambda.Tools
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