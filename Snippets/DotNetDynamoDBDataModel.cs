using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Snippets
{
    public class DotNetDynamoDBDataModel
    {
        private DynamoDBContext Context { get; set; }

        public DotNetDynamoDBDataModel( )
        {
            #region datamodel_construct_client
            var config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2,
                ConsistentRead = true
            };
            Context = new DynamoDBContext(new AmazonDynamoDBClient(RegionEndpoint.USEast2));  
            
            Console.WriteLine("Constructed DynamoDBContext");
            #endregion
        }
        
        public async Task SaveTODOListAsync()
        {
            Console.WriteLine("----- Executing Save -----");
            #region datamodel_construct_save
            var list = new TODOList
            {
                User =  "testuser",
                ListId = "generated-list-id",
                Complete =  false,
                Name = "ExampleList",
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Items = new List<TODOListItem>
                {
                    new TODOListItem { Description = "Task1", Complete = true },
                    new TODOListItem { Description = "Task2", Complete = false }
                }
            };
            
            await this.Context.SaveAsync(list);
            Console.WriteLine("List saved");
            #endregion
            Console.WriteLine();
        }

        public async Task LoadTODOListAsync()
        {
            Console.WriteLine("----- Executing Load -----");
            #region datamodel_construct_load

            var list = await this.Context.LoadAsync<TODOList>("testuser", "generated-list-id");
            if(list != null)
            {
                Console.WriteLine($"Found list {list.Name}");
                foreach(var task in list.Items)
                {
                    Console.WriteLine($"\t{task.Description}");
                }
            }
            else
            {
                Console.WriteLine("List not found");
            }
            #endregion
            Console.WriteLine();
        }

        public async Task QueryTODOListAsync()
        {
            Console.WriteLine("----- Executing Query -----");
            #region datamodel_construct_query

            AsyncSearch<TODOList> search = this.Context.QueryAsync<TODOList>("testuser");

            var lists = await search.GetRemainingAsync();

            Console.WriteLine($"Total lists found: {lists.Count}");
            foreach(var list in lists)
            {
                Console.WriteLine($"List {list.Name}");
                foreach (var task in list.Items)
                {
                    Console.WriteLine($"\t{task.Description}");
                }
            }
            #endregion
            Console.WriteLine();
        }

        public async Task DeleteTODOListAsync()
        {
            Console.WriteLine("----- Executing Delete -----");
            #region datamodel_construct_delete

            await this.Context.DeleteAsync<TODOList>("testuser", "generated-list-id");
            Console.WriteLine("Deleted list");

            #endregion
            Console.WriteLine();
        }

    }
    
    #region data_model_classes
    public class TODOList
    {
        public string User { get; set; }

        public string ListId { get; set; }

        public string Name { get; set; }

        public List<TODOListItem> Items { get; set; }

        public bool Complete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }        
    }
    
    public class TODOListItem
    {
        public string Description { get; set; }

        public string AssignedEmail { get; set; }

        public bool Complete { get; set; }
    }    
    #endregion
}