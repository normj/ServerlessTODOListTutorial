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
