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
            switch (region)
            {
                case "current_aws_profile":
                    SetConfiguration.SetAWSProfile();
                    return;
                case "current_aws_region":
                    SetConfiguration.SetAWSRegion();
                    return;
            }
            
            Environment.SetEnvironmentVariable("AWS_PROFILE", SetConfiguration.GetAWSProfile());
            Environment.SetEnvironmentVariable("AWS_REGION", SetConfiguration.GetAWSRegion());
            
            
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

                case "service_client_put":
                    DDBServiceClientAPI.PutItemAsync().Wait();
                    break;
                case "service_client_update":
                    DDBServiceClientAPI.UpdateItemAsync().Wait();
                    break;
                case "service_client_get":
                    DDBServiceClientAPI.GetItemAsync().Wait();
                    break;
                case "service_client_query":
                    DDBServiceClientAPI.QueryItemsAsync().Wait();
                    break;
                case "service_client_delete":
                    DDBServiceClientAPI.DeleteItemAsync().Wait();
                    break;

                case "enable_stream_status":
                    EnableDynamoDBStream.EnableAsync().Wait();
                    break;
                case "check_stream_status":
                    EnableDynamoDBStream.CheckStatusAsync().Wait();
                    break;
                case "test_stream_read":
                    EnableDynamoDBStream.TestStreamAsync().Wait();
                    break;

                case "send_verification_email":
                    SESSnippets.SendVerificationAsync().Wait();
                    break;

                case "add_dynamodb_event_source":
                    ConfigureEventSourceMapping.AddEventSourceAsync().Wait();
                    break;

                case "test_save_lambda_todo":
                    TestDynamoDBLambdaFunction.SaveTODOListAsync().Wait();
                    break;

                case "setup_streamprocessor_role":
                    IAMRoleSetups.SetupStreamProcessorRoleAsync().Wait();
                    break;
            }

            switch (session)
            {
                case "datamodel":
                    ExecuteDataModelSession().Wait();
                    break;
            }
        }

        private static async Task ExecuteDataModelSession()
        {
            var d = new DotNetDynamoDBDataModel();
            await d.SaveTODOListAsync();
            await d.LoadTODOListAsync();
            await d.QueryTODOListAsync();
            await d.DeleteTODOListAsync();
        }
    }
}
