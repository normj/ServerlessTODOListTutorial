using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snippets
{
    public class TearDown
    {
        #region resources_to_tear_down
        public string DynamoDBTableName = "TODOList";
        #endregion

        public async Task TearDownResourcesAsync()
        {
            using (var ddbClient = new AmazonDynamoDBClient())
            {
                var request = new DeleteTableRequest
                {
                    TableName = this.DynamoDBTableName
                };

                try
                {
                    var response = await ddbClient.DeleteTableAsync(request);
                    Console.WriteLine($"Table {response.TableDescription.TableName} is deleted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Table {this.DynamoDBTableName} was not deleted: {e.Message}");
                }
            }
        }
    }
}
