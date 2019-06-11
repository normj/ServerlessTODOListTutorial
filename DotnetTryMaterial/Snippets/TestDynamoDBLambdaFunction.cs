using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Snippets
{
    public class TestDynamoDBLambdaFunction
    {
        public static async Task SaveTODOListAsync()
        {
            var config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2,
                ConsistentRead = true
            };
            var Context = new DynamoDBContext(new AmazonDynamoDBClient());

            #region test_save_lambda_todo

            var assignedEmail = "";
            if (string.IsNullOrEmpty(assignedEmail))
            {
                Console.Error.WriteLine("You must set the email to the \"assignedEmail\" variable");
                return;
            }

            var list = new TODOList
            {
                User = "testuser",
                ListId = Guid.NewGuid().ToString(),
                Complete = false,
                Name = "ExampleList",
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Items = new List<TODOListItem>
                {
                    new TODOListItem
                    {
                        Description = "Assigned Task",
                        Complete = false,
                        AssignedEmail = assignedEmail
                    }
                }
            };

            await Context.SaveAsync(list);
            Console.WriteLine("List saved, check your email to see if you get the assignment");
            #endregion
        }
    }
}
