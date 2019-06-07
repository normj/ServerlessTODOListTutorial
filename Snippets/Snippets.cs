using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Snippets
{
    class Program
    {
        static void Main(
            string region = null,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null)
        {
            try
            {
                switch (region)
                {
                    case "create_table":
                        CreateTable.CreateTableAsync().Wait();
                        break;
                    case "check_status":
                        CreateTable.CheckStatusAsync().Wait();
                        break;
                    case "update_table":
                        CreateTable.UpdateTableAsync().Wait();
                        break;
                    case "delete_table":
                        CreateTable.DeleteTableAsync().Wait();
                        break;

                    case "service_client_put":
                        DDBServiceClientAPI.PutItemAsync().Wait();
                        break;
                    case "service_client_update":
                        DDBServiceClientAPI.UpdateItemAsync().Wait();
                        break;
                    case "service_client_get":
                        DDBServiceClientAPI.GetItemAsync().Wait();
                        break;
                    case "service_client_query":
                        DDBServiceClientAPI.QueryItemsAsync().Wait();
                        break;
                    case "service_client_delete":
                        DDBServiceClientAPI.DeleteItemAsync().Wait();
                        break;
                    
                    case "datamodel_construct_client":
                        new DotNetDynamoDBDataModel();
                        break;
                    case "datamodel_construct_save":
                        new DotNetDynamoDBDataModel().SaveTODOListAsync().Wait();
                        break;
                }

                switch (session)
                {
                    case "datamodel":
                        var d = new DotNetDynamoDBDataModel();
                        d.SaveTODOListAsync().Wait();
                        break;
                }
            }
            catch (AggregateException e)
            {
//                throw e.InnerException;
                throw;
            }
        }
    }
}
