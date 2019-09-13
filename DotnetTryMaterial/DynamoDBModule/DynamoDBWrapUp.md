# Amazon DynamoDB wrap up

For our TODO List serverless application we will use the Data Model API. In your open solution for the application, the **ServerlessTODOList.DataAccess** project
has the **TODOListDataAccess.cs** file which contains all of the operations the application needs to access the TODO Lists stored in DynamoDB.


```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

using ServerlessTODOList.Common;


namespace ServerlessTODOList.DataAccess
{
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
        }

        public async Task<IList<TODOList>> GetTODOListsForUserAsync(string user)
        {
            try
            {
                var lists = await this.Context.QueryAsync<TODOList>(user).GetRemainingAsync();
                return lists;
            }
            catch (Exception e)
            {
                throw new TODOListDataAccessException("Error getting TODO lists for user", e);
            }
        }

        public async Task<TODOList> GetTODOListAsync(string user, string listId)
        {
            try
            {
                var todoList = await this.Context.LoadAsync<TODOList>(user, listId);
                return todoList;
            }
            catch(Exception e)
            {
                throw new TODOListDataAccessException("Error getting TODO list", e);
            }
        }

        public async Task SaveTODOListAsync(TODOList list)
        {
            try
            {
                if (string.IsNullOrEmpty(list.ListId))
                    list.ListId = Guid.NewGuid().ToString();

                if (list.CreateDate == DateTime.MinValue)
                    list.CreateDate = DateTime.UtcNow;

                list.UpdateDate = DateTime.UtcNow;

                await this.Context.SaveAsync(list);
            }
            catch (Exception e)
            {
                throw new TODOListDataAccessException("Error saving TODO list", e);
            }
        }

        public async Task DeleteTODOList(string user, string listId)
        {
            try
            {
                await this.Context.DeleteAsync<TODOList>(user, listId);
            }
            catch (Exception e)
            {
                throw new TODOListDataAccessException("Error deleting TODO list", e);
            }
        }
    }
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
  * [Creating DynamoDB table](../DynamoDBModule/CreateTable.md)
  * [Accessing DynamoDB items with the AWS SDK for .NET](../DynamoDBModule/DotNetDynamoDBAPIs.md)
  * [CRUD operations with Amazon DynamoDB Service Client](../DynamoDBModule/DDBServiceClientAPI.md)
  * [CRUD operations with Document Model API](../DynamoDBModule/DotNetDynamoDBDocumentModel.md)
  * [CRUD operations with Data Model API](../DynamoDBModule/DotNetDynamoDBDataModel.md)
  * **Amazon DynamoDB wrap up**
* [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)
* [Getting ASP.NET Core ready for Serverless](../ASP.NETCoreFrontend/TheFrontend.md)
* [Deploying ASP.NET Core as a Serverless Application](../DeployingFrontend/DeployingFrontend.md)
* [Tear Down](../TearDown.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Handling service events with Lambda](../StreamProcessing/ServiceEvents.md)

