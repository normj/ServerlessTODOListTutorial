using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace Snippets
{
    public class TearDown
    {
        #region resources_to_tear_down
        public string DynamoDBTableName { get; } = "TODOList";
        public string DynanmoDBStreamLambdaFunction { get; } =  "ServerlessTODOListStreamProcessor";
        public string CognitoUserPool { get; } = "ServerlessTODOList";
        public string ParameterStorePrefix { get; } = "/ServerlessTODOList";
        public string FrontendCloudFormationStack { get; } =  "ServerlessTODOList";
        #endregion

        public async Task TearDownResourcesAsync()
        {
            using (var cfClient = new AmazonCloudFormationClient())
            {
                var request = new DeleteStackRequest
                {
                    StackName = this.FrontendCloudFormationStack
                };
                
                try
                {
                    await cfClient.DeleteStackAsync(request);
                    Console.WriteLine($"Frontend CloudFormation Stack {this.FrontendCloudFormationStack} is deleted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Frontend CloudFormation Stack {this.FrontendCloudFormationStack} was not deleted: {e.Message}");
                }                  
            }
            
            using (var lambdaClient = new AmazonLambdaClient())
            {
                var request = new DeleteFunctionRequest
                {
                    FunctionName = this.DynanmoDBStreamLambdaFunction
                };
                
                try
                {
                    await lambdaClient.DeleteFunctionAsync(request);
                    Console.WriteLine($"Function {this.DynanmoDBStreamLambdaFunction} is deleted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Function {this.DynanmoDBStreamLambdaFunction} was not deleted: {e.Message}");
                }                
            }            
            
            using (var cognitoClient = new AmazonCognitoIdentityProviderClient())
            {
                var userPool = (await cognitoClient.ListUserPoolsAsync(new ListUserPoolsRequest{MaxResults = 60})).UserPools
                    .FirstOrDefault(x => string.Equals(this.CognitoUserPool, x.Name));

                if (userPool != null)
                {
                    var request = new DeleteUserPoolRequest
                    {
                        UserPoolId = userPool.Id
                    };
                    
                    try
                    {
                        await cognitoClient.DeleteUserPoolAsync(request);
                        Console.WriteLine($"Cognito User Pool {this.CognitoUserPool} is deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Cognito User Pool {this.CognitoUserPool} was not deleted: {e.Message}");
                    }                      
                }
            }

            using (var ssmClient = new AmazonSimpleSystemsManagementClient())
            {
                try
                {
                    var parameters = (await ssmClient.GetParametersByPathAsync(new GetParametersByPathRequest
                    {
                        Path = this.ParameterStorePrefix,
                        Recursive = true
                    })).Parameters;
                    
                    Console.WriteLine($"Found {parameters.Count} SSM parameters starting with {this.ParameterStorePrefix}");

                    foreach (var parameter in parameters)
                    {
                        try
                        {
                            await ssmClient.DeleteParameterAsync(new DeleteParameterRequest {Name = parameter.Name});
                            Console.WriteLine($"Parameter {parameter.Name} is deleted");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Parameter {parameter.Name} was not deleted: {e.Message}");
                        }
                    
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error deleting SSM Parameters: {e.Message}");
                }
            }
            
            using (var ddbClient = new AmazonDynamoDBClient())
            {
                var request = new DeleteTableRequest
                {
                    TableName = this.DynamoDBTableName
                };

                try
                {
                    await ddbClient.DeleteTableAsync(request);
                    Console.WriteLine($"Table {this.DynamoDBTableName} is deleted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Table {this.DynamoDBTableName} was not deleted: {e.Message}");
                }
            }            
        }
    }
}
