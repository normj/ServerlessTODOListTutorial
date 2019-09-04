using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;

using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

using ServerlessTODOList.Common;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ServerlessTODOList.StreamProcessor
{
    public class Function
    {
        const string FROM_EMAIL_ENV_NAME = "FROM_EMAIL";
        string fromEmail;

        DynamoDBContext Context { get; set; }

        IAmazonSimpleEmailService SESClient { get; set; }

        public Function()
        {
            this.Context = new DynamoDBContext(new AmazonDynamoDBClient(), new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            });

            this.SESClient = new AmazonSimpleEmailServiceClient(Amazon.RegionEndpoint.USEast1);

            if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable(FROM_EMAIL_ENV_NAME)))
            {
                throw new Exception($"The {FROM_EMAIL_ENV_NAME} environment variable to the email address that will be the from address for the emails.");
            }

            fromEmail = Environment.GetEnvironmentVariable(FROM_EMAIL_ENV_NAME);
        }

        public async Task FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to process {dynamoEvent.Records.Count} records...");

            foreach (var record in dynamoEvent.Records)
            {
                try
                {
                    context.Logger.LogLine($"Event ID: {record.EventID}");
                    context.Logger.LogLine($"Event Name: {record.EventName}");

                    var oldVersion = ConvertToTODOList(record.Dynamodb.OldImage);
                    var newVersion = ConvertToTODOList(record.Dynamodb.NewImage);

                    var emailsToTasks = CompareVersions(oldVersion, newVersion);
                    foreach (var kvp in emailsToTasks)
                    {
                        var emailBody = CreateEmailBody(kvp.Value);

                        var request = new SendEmailRequest
                        {
                            Source = fromEmail,
                            Destination = new Destination { ToAddresses = new List<string> { kvp.Key } },
                            Message = new Message
                            {
                                Subject = new Content { Data = "New tasks have been assigned to you." },
                                Body = new Body
                                {
                                    Text = new Content { Data = emailBody }
                                }
                            }
                        };

                        await this.SESClient.SendEmailAsync(request);
                        context.Logger.LogLine($"Sent email to {request.Destination.ToAddresses[0]} from {request.Source}");
                        context.Logger.LogLine(emailBody);
                    }
                }
                catch(Exception e)
                {
                    context.Logger.LogLine($"Error processing record: {e.Message}");
                    context.Logger.LogLine(e.StackTrace);
                }
            }

            context.Logger.LogLine("Stream processing complete.");
        }

        private TODOList ConvertToTODOList(Dictionary<string, AttributeValue> item)
        {
            if (item == null || item.Count == 0)
                return new TODOList();

            var document = Document.FromAttributeMap(item);
            return this.Context.FromDocument<TODOList>(document);
        }

        private Dictionary<string, List<string>> CompareVersions(TODOList oldVersion, TODOList newVersion)
        {
            var emailsToTasks = new Dictionary<string, List<string>>();
            if (newVersion.Items == null)
                return emailsToTasks;

            foreach(var task in newVersion.Items)
            {
                if (string.IsNullOrWhiteSpace(task.AssignedEmail) || task.Complete || string.IsNullOrWhiteSpace(task.Description))
                    continue;

                if(oldVersion?.Items?.FirstOrDefault(x => string.Equals(x.Description, task.Description)) == null)
                {
                    List<string> tasks = null;
                    if(!emailsToTasks.TryGetValue(task.AssignedEmail, out tasks))
                    {
                        tasks = new List<string>();
                        emailsToTasks[task.AssignedEmail] = tasks;
                    }

                    tasks.Add(task.Description);
                }
            }


            return emailsToTasks;
        }

        private string CreateEmailBody(List<string> tasks)
        {
            var body = new StringBuilder();
            body.AppendLine("The following tasks have been assigned to you:");
            foreach(var task in tasks)
            {
                body.AppendLine($"* {task}");
            }

            return body.ToString();
        }
    }
}