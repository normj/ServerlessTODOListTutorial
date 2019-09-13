# Final Wrap Up


You have reached the end. Throughout this tutorial we covered:

* Amazon DynamoDB
  * How to create tables and what are the different data types.
  * How to access items from a table with the AWS SDK for .NET
  * How to enable DynamoDB Streams for a table
* ASP.NET Core Providers
  * AWSSDK.Extensions.NETCore.Setup - Adding AWS services to dependency injection framework.
  * <a href="https://github.com/aws/aws-aspnet-cognito-identity-provider" target="_blank">Amazon.AspNetCore.Identity.Cognito</a> - Using Amazon Cognito for identity.
  * <a href="https://github.com/aws/aws-ssm-data-protection-provider-for-aspnet" target="_blank">Amazon.AspNetCore.DataProtection.SSM</a> - Storing encryption keys for data protection.
  * <a href="https://github.com/aws/aws-dotnet-extensions-configuration" target="_blank">Amazon.Extensions.Configuration.SystemsManager</a> - Integrating Parameter Store to configuration framework.
* Building a Lambda function to process service events in this case DynamoDB
  * Enabling Lambda for an ASP.NET Core application
  * Deploying functions
  * Configuring event sources
  * Troubleshooting
* Deploy to AWS Fargate
  * Adding Dockerfile to ASP.NET Core Application.
  * Publishing from Visual Studio to Elastic Container Service with Fargate.
  * Configuring an Application Load Balancer for containers running application.

Useful Links for .NET developers interested in AWS.

| Names | Links |
|-|-|
|GitHub home for .NET development on AWS|[https://github.com/aws/dotnet](https://github.com/aws/dotnet)| 
|AWS SDK for .NET|[https://github.com/aws/aws-sdk-net](https://github.com/aws/aws-sdk-net)|
|AWS Lambda for .NET Core|[https://github.com/aws/aws-lambda-dotnet](https://github.com/aws/aws-lambda-dotnet)|
|AWS .NET Core Global Tools|[https://github.com/aws/aws-extensions-for-dotnet-cli](https://github.com/aws/aws-extensions-for-dotnet-cli)|
|AWS .NET Twitter Handle|[https://twitter.com/awsfornet](https://twitter.com/awsfornet)|
| All things .NET at AWS | [https://aws.amazon.com/net](https://aws.amazon.com/net)


### From the Author <a href="https://twitter.com/socketnorm" target="_blank">Norm Johanson</a> with a little help from <a href="https://twitter.com/kneekey23" target="_blank">Nicki Stone</a>
Thank you for going through this tutorial and trying out this experiment of using **dotnet try**. I would greatly 
appeciate feedback on using **dotnet try** and if this is something I should do more of. Feel free to open 
up GitHub issues for feedback or fork the repo and send those PRs to fix my typos and grammar issues.

<!-- Generated Navigation -->
---

* [Getting Started](./GettingStarted.md)
* [What is a serverless application?](./WhatIsServerless.md)
* [Common AWS Serverless Services](./CommonServerlessServices.md)
* [What are we going to build in this tutorial](./WhatAreWeBuilding.md)
* [TODO List AWS Services Used](./TODOListServices.md)
* [Using DynamoDB to store TODO Lists](./DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](./StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](./ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](./DeployingFrontend/DeployingFrontend.md)
* [Tear Down](./TearDown.md)
* **Final Wrap Up**

