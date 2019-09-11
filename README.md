# Building a AWS Serverless TODO List Application

This is a tutorial I created for a couple of AWS talks I gave in June 2019. It shows how to build a very simple TODO list application. Along the way you will learn how to use several AWS services like Amazon DynamoDB and AWS Lambda.


## dotnet try

At Microsoft's 2019 Build conference a new .NET Global tool called <a href="https://github.com/dotnet/try" target="_blank">dotnet try</a> was released which allows the creation of interactive .NET documentation. 

In the interest of always trying new things I decided to use dotnet try to create this tutorial. This tool is still in early development and breaking changes could still occur. The tutorial was written and tested with version <a href="https://www.nuget.org/packages/dotnet-try/1.0.19317.5" target="blank">1.0.19317.5</a>


## Setup

To get started with this tutorial follow the following steps.

* Ensure .NET Core 2.1 is installed. All of the code was written for .NET Core 2.1.
  * https://dotnet.microsoft.com/download/dotnet-core/2.1
* Ensure .NET Core 3.0 SDK is installed. The tool dotnet try requires .NET Core 3.0
  * https://dotnet.microsoft.com/download/dotnet-core/3.0
* Either the AWS Toolkit for Visual Studio or <a href="https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools" target="_blank">Amazon.Lambda.Tools</a> .NET Core Global Tool
  * <a href="https://marketplace.visualstudio.com/items?itemName=AmazonWebServices.AWSToolkitforVisualStudio2017" target="_blank">AWS Toolkit for Visual Studio Download</a>
  * Amazon.Lambda.Tools .NET Core Global Tool - `dotnet tool install -g Amazon.Lambda.Tools`
* Clone this repository
  * `git clone https://github.com/normj/ServerlessTODOListTutorial.git`
* Install dotnet try
  * `dotnet tool install -g dotnet-try`
* Start dotnet try in the directory the repo was cloned
  * `dotnet try`


## Let's Start

If you are seeing this page after running `dotnet try` then click [here to get started](./DotnetTryMaterial/GettingStarted.md).  