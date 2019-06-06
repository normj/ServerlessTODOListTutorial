using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Snippets
{
    class CreateTable
    {
        public static async Task CreateTableAsync()
        {
            #region create_table
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new CreateTableRequest
                {
                    TableName = "TODOList",
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition{AttributeName = "User", AttributeType = ScalarAttributeType.S },
                        new AttributeDefinition{AttributeName = "ListId", AttributeType = ScalarAttributeType.S }
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement { KeyType = KeyType.HASH, AttributeName = "User" },
                        new KeySchemaElement { KeyType = KeyType.RANGE, AttributeName = "ListId" }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 4, WriteCapacityUnits = 1 }
                };

                var response = await ddbClient.CreateTableAsync(request);
                Console.WriteLine($"Table {response.TableDescription.TableName} create and current status is {response.TableDescription.TableStatus}");
            }
            #endregion
        }

        public static async Task UpdateTableAsync()
        {
            #region update_table
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new UpdateTableRequest
                {
                    TableName = "TODOList",
                    BillingMode = BillingMode.PROVISIONED,
                    ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 2 }
                };

                var response = await ddbClient.UpdateTableAsync(request);
                Console.WriteLine($"Table {response.TableDescription.TableName} updated and current status is {response.TableDescription.TableStatus}");
            }
            #endregion
        }

        public static async Task CheckStatusAsync()
        {
            #region check_status
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new DescribeTableRequest
                {
                    TableName = "TODOList"
                };

                var response = await ddbClient.DescribeTableAsync(request);
                Console.WriteLine($"Table {response.Table.TableName} status is {response.Table.TableStatus}");
                if (response.Table.BillingModeSummary == null)
                {
                    Console.WriteLine($"Provisioned Reads: {response.Table.ProvisionedThroughput.ReadCapacityUnits}, Provisioned Writes: {response.Table.ProvisionedThroughput.WriteCapacityUnits}");
                }
                else
                {
                    Console.WriteLine($"Billing Mode: {response.Table.BillingModeSummary.BillingMode}");
                }
            }
            #endregion
        }


        public static async Task DeleteTableAsync()
        {
            #region delete_table
            using (var ddbClient = new AmazonDynamoDBClient(RegionEndpoint.USEast2))
            {
                var request = new DeleteTableRequest
                {
                    TableName = "TODOList"
                };

                var response = await ddbClient.DeleteTableAsync(request);
                Console.WriteLine($"Table {response.TableDescription.TableName} is deleted and current status is {response.TableDescription.TableStatus}");
            }
            #endregion
        }
    }
}
