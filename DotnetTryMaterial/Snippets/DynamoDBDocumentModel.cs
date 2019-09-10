using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace Snippets
{
    public class DynamoDBDocumentModel
    {
        public static async Task PutTODOListAsync()
        {
            #region document_model_put
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "TODOList", DynamoDBEntryConversion.V2);
                
                var todoList = new Document();
                todoList["User"] = "document-testuser";
                todoList["ListId"] = "generated-list-id";
                todoList["Name"] = "Test List with Document Model";
                todoList["Complete"] = false;
                todoList["CreateDate"] = DateTime.UtcNow;
                todoList["UpdateDate"] = DateTime.UtcNow;

                var tasks = new List<Document>();
                
                var task1 = new Document();
                task1["Description"] = "Task1";
                task1["Complete"] = true;
                tasks.Add(task1);
                
                var task2 = new Document();
                task2["Description"] = "Task2";
                task2["Complete"] = false;
                tasks.Add(task2);

                todoList["Tasks"] = tasks;

                await table.PutItemAsync(todoList);
                Console.WriteLine($"TODO List saved with Document Model");
            }
            #endregion
        }

        public static async Task GetTODOListAsync()
        {
            #region document_model_get
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "TODOList", DynamoDBEntryConversion.V2);

                var todoList = await table.GetItemAsync("document-testuser", "generated-list-id");
                Console.WriteLine($"Found list: {todoList["Name"]}");
                
                var tasks = todoList["Tasks"].AsListOfDocument();
                Console.WriteLine($"Number of tasks: {tasks.Count}");
                foreach (var task in tasks)
                {
                    Console.WriteLine($"\t{task["Description"]}");
                }
            }            
            #endregion
        }

        public static async Task QueryTODOListAsync()
        {
            #region document_model_query
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "TODOList", DynamoDBEntryConversion.V2);

                var queryFilter = new QueryFilter();
                //queryFilter.AddCondition("ListId", QueryOperator.BeginsWith, "custom");

                var search = table.Query("document-testuser", queryFilter);

                var lists = await search.GetRemainingAsync();

                Console.WriteLine($"Total lists found: {lists.Count}");
                foreach (var todoList in lists)
                {
                    Console.WriteLine($"List Name: {todoList["Name"]}");
                    Console.WriteLine($"List ID:{todoList["ListId"]}");
                    var tasks = todoList["Tasks"].AsListOfDocument();
                    foreach (var task in tasks)
                    {
                        Console.WriteLine($"\t{task["Description"]}");
                    }
                }
            }
            #endregion
        }

        public static async Task DeleteTODOListAsync()
        {
            #region document_model_delete
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "TODOList", DynamoDBEntryConversion.V2);

                await table.DeleteItemAsync("document-testuser", "generated-list-id");
                Console.WriteLine("Deleted list");
            }
            #endregion
        }
    }
}