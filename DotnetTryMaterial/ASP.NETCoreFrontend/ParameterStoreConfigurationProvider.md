# AWS Systems Manager Parameter Store for Managing Configuration

In our application we have configuration elements like the Cognito user pool id stored in our **appsettings.Development.json**. This is
fine for local development but when we are deployed to the Cloud we don't want to have our configuration embedded with our deployment bundle.

## AWS Systems Manager Parameter Store

[AWS Systems Manager Parameter Store](https://docs.aws.amazon.com/systems-manager/latest/userguide/systems-manager-parameter-store.html) provides secure, hierarchical storage for configuration data management and secrets management. You can store data such as passwords, database strings, and license codes as parameter values. You can store values as plain text or encrypted data. 


## Configuration in ASP.NET Core

ASP.NET Core has a built in configuration system that uses a **ConfigurationBuilder** that takes a collection of configuration providers. The builder builds an instance of **IConfiguration** that provides access to configuration data from the registered configuration providers. ASP.NET Core comes with providers for JSON, XML, INI files and environment variables.

## Configuring Parameter Store as a provider

The [Amazon.Extensions.Configuration.SystemsManager](https://www.nuget.org/packages/Amazon.Extensions.Configuration.SystemsManager/) is a configuration provider that will include parameters from Parameter Store to the IConfiguration object. This package is open source and can be found [here](https://github.com/aws/aws-dotnet-extensions-configuration)


To use the package add the NuGet reference to the **ServerlessTODOList.Frontend** through your IDE NuGet package management or use the command below in directory of ServerlessTODOList.Frontend:
```bash
~/ServerlessTODOList.Frontend> dotnet add package Amazon.Extensions.Configuration.SystemsManager
```

After the Amazon.Extensions.Configuration.SystemsManager package has been added
edit the **Program.cs** to add the call to **ConfigureAppConfiguration** like below.

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddSystemsManager("/ServerlessTODOList/");
            })
            .UseStartup<Startup>();
}
```

This will add all of the parameters from Parameter Store starting with **/ServerlessTODOList/** to the ASP.NET Core configuration system. When you access values from the IConfiguration object you 
will do it without the **/ServerlessTODOList/** prefix.

## Add values to Parameter Store

You can add parameter through the [System Manager console](https://console.aws.amazon.com/systems-manager/parameters). Be sure to select the region you plan on deploying the application to. 

You can also set the values with any AWS SDK or CLI. Here is the .NET code to add the Cognito configuration data to Parameter Store. You must
set the **userPoolId, userPoolClientId** and **userPoolClientSecret** to the values that you sent in **appsettings.Development.json** in the 
previous section.


```cs --source-file ../Snippets/ParameterStoreSetup.cs --project ../Snippets/Snippets.csproj --region add_parameter_store_values
```

### Test Parameter Store

Below you can see the **IConfiguration** object is build similar to how
ASP.NET Core's IWebHostBuilder builds the configuration. 

If the parameters have been added to Parameter Store then the following code that builds an **IConfiguration** should pull the parameters out and display them.

```cs --source-file ../Snippets/ParameterStoreSetup.cs --project ../Snippets/Snippets.csproj --region test_parameter_store_values
```

## Wrap up

The Cognito identity provider for ASP.NET Core that we talked about
access configuration from the IConfiguration. That means after adding
the Parameter Store provider to IConfiguration we don't have to make
any changes to our code for setting up the Cognito identity provider.

If you take a look at some of the overloads of **AddSystemsManager** you can see there are more options to how the data is retrieved as
well as configuring an auto reload of data from Parameter store.

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [The Serverless TODO List Frontend](../ASP.NETCoreFrontend/TheFrontend.md)
  * [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)
  * [Persisting ASP.NET Core Data Protection Keys](../ASP.NETCoreFrontend/ParameterStoreDataProtection.md)
  * **AWS Parameter Store for Managing Configuration**
  * [ASP.NET Core wrap up](../ASP.NETCoreFrontend/FrontendWrapup.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [ASP.NET Core wrap up](../ASP.NETCoreFrontend/FrontendWrapup.md)

