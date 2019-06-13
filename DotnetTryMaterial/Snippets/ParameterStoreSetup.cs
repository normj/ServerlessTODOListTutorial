using System;
using System.Threading.Tasks;

using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

using Microsoft.Extensions.Configuration;



namespace Snippets
{
    public class ParameterStoreSetup
    {
        public static async Task AddParametersAsync()
        {
            #region add_parameter_store_values

            // Set Cognito config values here
            var userPoolId = "";
            var userPoolClientId = "";
            var userPoolClientSecret = "";
            if (!ValidateInput())
                return;
            
            using (var ssmClient = new AmazonSimpleSystemsManagementClient())
            {
                await ssmClient.PutParameterAsync(new PutParameterRequest
                {
                    Type = ParameterType.String,
                    Description = "Cognito User Pool Id",
                    Name = "/ServerlessTODOList/AWS/UserPoolId",
                    Value = userPoolId
                });
                Console.WriteLine($"Parameter /ServerlessTODOList/AWS/UserPoolId set to {userPoolId}");
                
                await ssmClient.PutParameterAsync(new PutParameterRequest
                {
                    Type = ParameterType.String,
                    Description = "Cognito User Pool Client Id",
                    Name = "/ServerlessTODOList/AWS/UserPoolClientId",
                    Value = userPoolClientId
                });
                Console.WriteLine($"Parameter /ServerlessTODOList/AWS/UserPoolClientId set to {userPoolClientId}");

                await ssmClient.PutParameterAsync(new PutParameterRequest
                {
                    Type = ParameterType.SecureString,
                    Description = "Cognito User Pool Client Secret",
                    Name = "/ServerlessTODOList/AWS/UserPoolClientSecret",
                    Value = userPoolClientSecret
                });
                Console.WriteLine($"Parameter /ServerlessTODOList/AWS/UserPoolClientSecret set to {userPoolClientSecret}");
                
            }
            #endregion

            bool ValidateInput()
            {
                if (string.IsNullOrEmpty(userPoolId) || userPoolId.StartsWith("<"))
                {
                    Console.Error.WriteLine("You must set the userPoolId variable");
                    return false;
                }
                if (string.IsNullOrEmpty(userPoolClientId) || userPoolId.StartsWith("<"))
                {
                    Console.Error.WriteLine("You must set the userPoolClientId variable");
                    return false;
                }
                if (string.IsNullOrEmpty(userPoolClientSecret) || userPoolId.StartsWith("<"))
                {
                    Console.Error.WriteLine("You must set the userPoolClientSecret variable");
                    return false;
                }

                return true;
            }
        }
        
        public static Task TestParametersAsync()
        {
            #region test_parameter_store_values
            
            var builder = new ConfigurationBuilder();
            
            builder.AddSystemsManager("/ServerlessTODOList/");

            IConfiguration configuration = builder.Build();
            
            Console.WriteLine($"User Pool Id: {configuration["AWS:UserPoolId"]}");
            Console.WriteLine($"User Pool Client Id: {configuration["AWS:UserPoolClientId"]}");
            Console.WriteLine($"User Pool Client Secret: {configuration["AWS:UserPoolClientSecret"]}");
            
            #endregion

            return Task.CompletedTask;
        }
    }
}