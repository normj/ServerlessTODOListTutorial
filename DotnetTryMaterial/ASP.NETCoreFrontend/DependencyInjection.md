# Dependency Injection

ASP.NET Core has a built in dependency injection framework to make it easy for your Controllers and Razor pages to get
services they require. Services are registered with the dependency injection framework in the **Startup** 
class's `ConfigureServices` method.

The services for our DynamoDB data access layer have already been registered in the provided code but let's take look at how it was done.

## Injecting AWS Services

The NuGet package <a href="https://www.nuget.org/packages/AWSSDK.Extensions.NETCore.Setup/" target="_blank">AWSSDK.Extensions.NETCore.Setup</a> adds 
new extension methods to the `IServiceCollection` object. We can add DynamoDB by adding the following line of code.

```csharp
services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();
```

The `AddAWSService` method uses the service interface, in this case `IAmazonDynamoDB`, to identify the AWS service. When an object is created that 
takes in an `IAmazonDynamoDB` as part of the constructor the service client will be create from the `IServiceCollection`.
Since the `IConfiguration` object is also added to the `IServiceCollection` the AWSSDK.Extensions.NETCore.Setup package
will look for the configuration values like region and profile that we recently set to construct the service client.

## Adding our data access interface

In the DynamoDB section of this tutorial we showed `ITODOListDataAccess` interface and its implementation `TODOListDataAccess` which provides our APIs to
access our TODO lists. The interface `ITODOListDataAccess` is also added to the `IServiceCollection` with the following line of code.

```csharp
services.AddSingleton(typeof(ITODOListDataAccess), typeof(TODOListDataAccess));
```

## How does it work

Now that we have both the AWS DynamoDB service client and our custom data access layer registered lets take a look at how the dependency injection work.

If we take a look at the file **ServerlessTODOList.Frontend\Pages\MyLists.cshtml.cs** which contains the class **MyListsModel** you can see it 
takes in an instance of **ITODOListDataAccess** as part of its constructor. When MyListsModel is created by the ASP.NET Core framework it looks
to **IServiceCollection** for an instance of **ITODOListDataAccess**. 

Since **ITODOListDataAccess** was registered as a singleton to the **IServiceCollection** it will be created the first time it is requested. 
**TODOListDataAccess** was specified to **IServiceCollection** as the implementer of **ITODOListDataAccess** but TODOListDataAccess takes in an 
instance of **IAmazonDynamoDB**. So now **IServiceCollection** will create an instance of **IAmazonDynamoDB** and pass that into the constructor of 
TODOListDataAccess which will then be passed in **MyListsModel**.

```csharp
public class MyListsModel : PageModel
{
    ITODOListDataAccess DataAccess { get; set; }

    public IList<TODOList> TODOLists { get; set; }

    public MyListsModel(ITODOListDataAccess dataAccess)
    {
        this.DataAccess = dataAccess;
    ...
```

```csharp
public class TODOListDataAccess : ITODOListDataAccess
{
    DynamoDBContext Context { get; set; }

    public TODOListDataAccess(IAmazonDynamoDB ddbClient)
    {
        var config = new DynamoDBContextConfig
        {
            Conversion = DynamoDBEntryConversion.V2,
            ConsistentRead = true
        };
        this.Context = new DynamoDBContext(ddbClient);
    ...
```




## Full version of ConfigureServices

Here is the full version of `ConfigureServices` from the **Startup** class that handles our dependency injection. This method will have more
changes in upcoming sections.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<CookiePolicyOptions>(options =>
    {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    // AddAWSService is provided by the AWSSDK.Extensions.NETCore.Setup NuGet package.
    services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();
    services.AddSingleton(typeof(ITODOListDataAccess), typeof(TODOListDataAccess));

    // Default SQL Server Entity Framework Identity setup
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
    services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
}
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
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
  * **Dependency Injection**
  * [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)
  * [Persisting ASP.NET Core Data Protection Keys](../ASP.NETCoreFrontend/ParameterStoreDataProtection.md)
  * [AWS Systems Manager Parameter Store for Managing Configuration](../ASP.NETCoreFrontend/ParameterStoreConfigurationProvider.md)
  * [ASP.NET Core wrap up](../ASP.NETCoreFrontend/FrontendWrapup.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Using Amazon Cognito for Identity](../ASP.NETCoreFrontend/WebIdentity.md)

