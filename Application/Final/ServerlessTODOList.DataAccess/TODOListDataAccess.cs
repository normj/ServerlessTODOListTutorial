﻿using System;
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
