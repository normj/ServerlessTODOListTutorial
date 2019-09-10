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
        public async Task SaveTODOListAsync()
        {
            #region document_model_save
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
                todoList["Tasks"] = tasks;
                
                var task1 = new Document();
                task1["Description"] = "Task1";
                task1["Complete"] = true;
                tasks.Add(task1);
                
                var task2 = new Document();
                task2["Description"] = "Task2";
                task2["Complete"] = false;
                tasks.Add(task2);

                await table.PutItemAsync(todoList);
            }
            #endregion
        }
        
        public async Task LoadTODOListAsync()
        {
            #region document_model_load
            using (var client = new AmazonDynamoDBClient())
            {
                var table = Table.LoadTable(client, "TODOList", DynamoDBEntryConversion.V2);

                var todoList = await table.GetItemAsync("document-testuser", "generated-list-id");
                Console.WriteLine($"Found list {todoList["Name"]}");
                
                var tasks = todoList["Tasks"] as IList<Document>;

                foreach (var task in tasks)
                {
                    Console.WriteLine($"\t{task["Description"]}");
                }
            }            
            #endregion
        }        
    }
}