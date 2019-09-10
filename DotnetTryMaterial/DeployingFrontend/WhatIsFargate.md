# What is for Fargate

Fargate is a compute type for [Elastic Container Service (ECS)](https://docs.aws.amazon.com/ecs/?id=docs_gateway) that allows you to deploy Docker containers without
managing any EC2 instances. When you deploy to ECS with Fargate you will specify the amount of CPU and Memory needed
and Fargate will ensure the compute resources are made available and then run your docker container.


## ECS Tasks vs Services

ECS runs containers either as a task or a service inside an ECS cluster. A task is for container applications that are meant to startup, do 
their work and then shutdown. Batch processing is a common example of when to use a task. A service is for 
containers like web servers that are meant to run indefinitely. A service is made up of a collection tasks and 
the service monitors the tasks. If any of the tasks stop running the service will start a new task.

## Load Balancer

Tasks for an ECS service will get unique IP address and public DNS when they are started. Because we will potentially have
more then one task supporting our application and the HTTP endpoints are unique for each task we will need
a load balancer from the Elastic Load Balancing service to provide a single consistent HTTP/HTTPS endpoint.
The ECS service managing the tasks for the service will also ensure the tasks are registered to the load balancer
when they are started.

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
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
  * [ASP.NET Core as a Lambda Function?](../DeployingFrontend/AspNetCoreAsLambda.md)
  * [Preparing for Lambda Deployment](../DeployingFrontend/LambdaPrepare.md)
  * [Deploy to Lambda using CloudFormation](../DeployingFrontend/LambdaDeploy.md)
  * **What is for Fargate**
  * [Deploying to Fargate](../DeployingFrontend/FargateDeploy.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Deploying to Fargate](../DeployingFrontend/FargateDeploy.md)

