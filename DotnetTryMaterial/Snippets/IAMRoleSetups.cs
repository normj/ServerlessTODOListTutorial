using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

namespace Snippets
{
    public static class IAMRoleSetups
    {
        public static async Task SetupStreamProcessorRoleAsync()
        {
            #region setup_streamprocessor_role
            using (var iamClient = new AmazonIdentityManagementServiceClient())
            {
                var createRequest = new CreateRoleRequest
                {
                    RoleName = "ServerlessTODOList.StreamProcessor",
                    AssumeRolePolicyDocument = @"
{
  ""Version"": ""2012-10-17"",
  ""Statement"": [
    {
      ""Sid"": """",
      ""Effect"": ""Allow"",
      ""Principal"": {
        ""Service"": ""lambda.amazonaws.com""
      },
      ""Action"": ""sts:AssumeRole""
    }
  ]
}
".Trim()
                };

                try
                {
                    var createResponse = await iamClient.CreateRoleAsync(createRequest);
                    Console.WriteLine($"Role {createResponse.Role.RoleName}");

                    var policies = new string[]
                    {
                        "arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole",
                        "arn:aws:iam::aws:policy/AmazonSESFullAccess"
                    };
                    foreach (var policy in policies)
                    {
                        await iamClient.AttachRolePolicyAsync(new AttachRolePolicyRequest
                        {
                            RoleName = createRequest.RoleName,
                            PolicyArn = policy
                        });
                        Console.WriteLine($"Policy {policy} attached to {createRequest.RoleName}");
                    }
                }
                catch(EntityAlreadyExistsException)
                {
                    Console.Error.WriteLine($"Role with the name {createRequest.RoleName} alreaedy exists.");
                }
            }
            #endregion
        }
    }
}
