using System;

using Amazon.CDK;
using Amazon.CDK.AWS.CloudFormation;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.Codepipeline.Actions;
using Amazon.CDK.AWS.IAM;

using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace ServerlessTodoListPipelineCdk
{
    public class ServerlessTodoListPipelineCdkStack : Stack
    {
        public ServerlessTodoListPipelineCdkStack(Construct parent, string id, IStackProps props) : base(parent, id, props)
        {
            var frontendBuild = new PipelineProject(this, "CodeBuild", new PipelineProjectProps
            {
                BuildSpec = BuildSpec.FromSourceFilename("./Application/Final/ServerlessTODOList.Frontend/buildspec.yml"),
                Environment = new BuildEnvironment
                {
                    BuildImage = LinuxBuildImage.STANDARD_2_0
                }
            });

            var s3PolicyStatement = new PolicyStatement();
            s3PolicyStatement.Effect = Effect.ALLOW;
            s3PolicyStatement.AddActions("s3:*");
            s3PolicyStatement.AddResources("arn:aws:s3:::normj-east1/", "arn:aws:s3:::normj-east1/*");
            frontendBuild.Role.AddToPolicy(s3PolicyStatement);

            var sourceOutput = new Artifact_();
            var buildOutput = new Artifact_("BuildOutput");

            var pipeline = new Pipeline(this, "Pipeline", new PipelineProps
            {
                Stages = new StageProps[]
                {
                    new StageProps
                    {
                        StageName = "Source",
                        Actions = new IAction[]
                        {
                            new GitHubSourceAction(new GitHubSourceActionProps
                            {
                                ActionName = "GitHubSource",
                                Branch = "master",
                                Repo = "ServerlessTODOListTutorial",
                                Owner = "normj",
                                OauthToken = SecretValue.PlainText(FetchGitHubPersonalAuthToken()),
                                Output = sourceOutput
                            })
                        }
                    },
                    new StageProps
                    {
                        StageName = "Build",
                        Actions = new IAction[]
                        {
                            new CodeBuildAction(new CodeBuildActionProps
                            {
                                ActionName = "BuildServerlessTODOListFrontend",
                                Project = frontendBuild,
                                Input = sourceOutput,
                                Outputs = new Artifact_[]{ buildOutput }
                            })
                        }
                    },
                    new StageProps
                    {
                        StageName = "Deploy",
                        Actions = new IAction[]
                        {
                            new CloudFormationCreateUpdateStackAction(new CloudFormationCreateUpdateStackActionProps
                            {
                                ActionName = "DeployFrontend",
                                Capabilities = new CloudFormationCapabilities[]{ CloudFormationCapabilities.ANONYMOUS_IAM, CloudFormationCapabilities.AUTO_EXPAND },
                                TemplatePath = ArtifactPath_.ArtifactPath("BuildOutput", "updated.template"),
                                StackName = "ServerlessFrontendCdk",
                                AdminPermissions = true
                            })
                        }
                    }
                }
            });
        }

        public string FetchGitHubPersonalAuthToken()
        {
            Console.WriteLine("Fetching GitHub personal auth token from Parameter Store");
            Console.Write("... ");
            using(var client = new AmazonSimpleSystemsManagementClient())
            {
                var request = new GetParameterRequest { Name = "/GitHubPersonalTokens/ServerlessTODOList", WithDecryption = true };
                var response = client.GetParameterAsync(request).Result;

                Console.WriteLine("Token found");
                return response.Parameter.Value;
            }
        }
    }
}
