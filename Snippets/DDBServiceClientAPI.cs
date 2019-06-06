using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Snippets
{
    public class DDBServiceClientAPI
    {
        public static async Task SaveItemAsync()
        {
            #region service_client_save
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new PutItemRequest
                {
                    TableName = "TODOList",
                    Item = new Dictionary<string, AttributeValue>
                    {
                        { "User", new AttributeValue{S = "testuser" } },
                        { "ListId", new AttributeValue{S = "generated-list-id" } },
                        { "Name", new AttributeValue{S = "Test List" } },
                        { "Complete", new AttributeValue{BOOL = false } },
                        { "CreateDate", new AttributeValue{S = DateTime.UtcNow.ToString() } },
                        { "UpdateDate", new AttributeValue{S = DateTime.UtcNow.ToString() } },
                        { "Items", new AttributeValue{ L = new List<AttributeValue>
                                {
                                    new AttributeValue{S = "TODO Task1" },
                                    new AttributeValue{S = "TODO Task2" }
                                }
                            }
                        }
                    }
                };

                var response = await ddbClient.PutItemAsync(request);
                Console.WriteLine($"TODO List saved");
            }
            #endregion
        }

        public static async Task GetItemAsync()
        {
            #region service_client_get
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new GetItemRequest
                {
                    TableName = "TODOList",
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "User", new AttributeValue{ S = "testuser" } },
                        { "ListId", new AttributeValue{ S = "generated-list-id" } }
                    }
                };

                var response = await ddbClient.GetItemAsync(request);
                if(response.Item.Count > 0)
                {
                    Dictionary<string, AttributeValue> item = response.Item;
                    Console.WriteLine($"List Name {response.Item["Name"].S}");
                    PrintItem(item);
                }
                else
                {
                    Console.WriteLine("TODO List item not found");
                }

            }
            #endregion
        }

        public static async Task QueryItemsAsync()
        {
            #region service_client_query
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new QueryRequest
                {
                    TableName = "TODOList",
                    KeyConditionExpression = "#u = :hashKey",
                    ExpressionAttributeNames = new Dictionary<string, string>
                    {
                        { "#u", "User" }
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":hashKey", new AttributeValue{ S = "testuser" } } 
                    }
                };

                var response = await ddbClient.QueryAsync(request);
                Console.WriteLine($"Items found: {response.Items.Count}");
                foreach(var item in response.Items)
                {
                    Console.WriteLine("");
                    PrintItem(item);
                }
            }
            #endregion
        }

        public static async Task DeleteItemAsync()
        {
            #region service_client_delete
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new DeleteItemRequest
                {
                    TableName = "TODOList",
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "User", new AttributeValue{ S = "testuser" } },
                        { "ListId", new AttributeValue{ S = "generated-list-id" } }
                    }
                };

                var response = await ddbClient.DeleteItemAsync(request);
                Console.WriteLine("Item Deleted");
            }
            #endregion
        }


        private static void PrintItem(Dictionary<string, AttributeValue> item)
        {
            foreach(var kvp in item)
            {
                Console.Write($"\t{kvp.Key}: ");
                if(kvp.Value.S != null)
                {
                    Console.WriteLine(kvp.Value.S);
                }
                else if(kvp.Value.IsBOOLSet)
                {
                    Console.WriteLine(kvp.Value.BOOL);
                }
                else if (kvp.Value.IsLSet)
                {
                    Console.WriteLine();
                    foreach(var i in kvp.Value.L)
                    {
                        Console.WriteLine($"\t\t: {i.S}");
                    }
                }
            }
        }
    }
}
