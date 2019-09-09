# Deploying ASP.NET Core as a Serverless Application

Now that our application is all coded and only using serverless services for its data storage and configuration it is time
to look into deploying our application.

For serverless compute options for our an ASP.NET Core application we have 2 options to pick from. The first is using **AWS Lambda** running
 the ASP.NET Core application as a Lambda function. The second is using **AWS Fargate** running the application as a docker container.

## Lambda vs Fargate

Both options allow us to focus on building and deploying applications without maintaining and patching any servers. They also
have many differences in their application lifecycle and how you scale them.

| | Lambda | Fargate
| - | - | - |
| Cost | Cost per request, no charge for idle time. | Cost for the length container is running including idle time |
| Scaling | Horizonal scaling by concurrent events<br />Vertical scaling by memory settings<br />Single request per execution environment | Horizonal scaling using AWS Auto Scaling <br />Vertical scaling by memory and cpu settings<br />Multiple request handled at a time by docker container |
| Performance | Occasional cold start during horizonal scaling | No cold starts as docker containers is always on |
| Programming Model | Restrictions on request and response payload size <br />No direct connection to the client | Runs as a regular ASP.NET Core application hosted by Kestrel the ASP.NET Core web server. |

My advice is investigate if Lambda will work for your requirements to take advantage of the easy scaling and pay per request model. If
your application does not fit the Lambda model then fallback to Fargate.


## Upcoming sections

Ahead in this tutorial are steps for both Lambda and Fargate. You can choose to follow either or both depending on what you want to learn.



<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* **Deploying ASP.NET Core as a Serverless Application**
  * [ASP.NET Core as a Lambda Function?](../DeployingFrontend/AspNetCoreAsLambda.md)
  * [Preparing for Lambda Deployment](../DeployingFrontend/LambdaPrepare.md)
  * [Deploy to Lambda using CloudFormation](../DeployingFrontend/LambdaDeploy.md)
  * [What is for Fargate](../DeployingFrontend/WhatIsFargate.md)
  * [Deploying to Fargate](../DeployingFrontend/FargateDeploy.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [ASP.NET Core as a Lambda Function?](../DeployingFrontend/AspNetCoreAsLambda.md)

