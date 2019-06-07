using System;
using System.Collections.Generic;
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
            #region datamodel_construct_save
            var list = new TODOList
            {
                User =  "testuser",
                ListId = "generated-list-id",
                Complete =  false,
                Name = "List from DataModel",
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Items = new List<TODOListItem>
                {
                    new TODOListItem { Description = "Task1", Complete = true },
                    new TODOListItem { Description = "Task2", Complete = false }
                }
            };
            
            await this.Context.SaveAsync(list);
            Console.WriteLine("TODO List item saves");
            #endregion
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

        public bool Complete { get; set; }
    }    
    #endregion
}