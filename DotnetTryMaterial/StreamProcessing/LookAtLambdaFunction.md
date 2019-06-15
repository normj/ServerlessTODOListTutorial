# Assign Task Lambda Function

In our application's solution the **ServerlessTODOList.StreamProcessor** project contains the .NET Core Lambda function that will respond to events from the stream.
You can see none of the shard management code is here, we just implement the `foreach` statement.

```csharp
public async Task FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
{
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
                    Source = FROM_EMAIL,
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
                context.Logger.LogLine($"Sent email to {request.Destination.ToAddresses[0]}");
                context.Logger.LogLine(emailBody);
            }
        }
        catch(Exception e)
        {
            context.Logger.LogLine($"Error processing record: {e.Message}");
            context.Logger.LogLine(e.StackTrace);
        }
    }
}
```

<!-- Generated Navigation -->
---

* [Getting Started](../GettingStarted.md)
* [What is a serverless application?](../WhatIsServerless.md)
* [Common AWS Serverless Services](../CommonServerlessServices.md)
* [What are we going to build in this tutorial](../WhatAreWeBuilding.md)
* [TODO List AWS Services Used](../TODOListServices.md)
* [Using DynamoDB to store TODO Lists](../DynamoDBModule/WhatIsDynamoDB.md)
* [Using Lambda to Handle Service Events](../StreamProcessing/ServiceEvents.md)
  * [TODO List Task Assignments](../StreamProcessing/TODOTaskListAssignment.md)
  * [Enable DynamoDB Stream](../StreamProcessing/EnableDynamoDBStream.md)
  * **Assign Task Lambda Function**
  * [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)
  * [Setting up Amazon Simple Email Service (SES)](../StreamProcessing/SettingUpSES.md)
  * [Configuring DynamoDB as an event source](../StreamProcessing/ConfigureLambdaEventSource.md)
  * [Testing Lambda Function](../StreamProcessing/TestingLambdaFunction.md)
  * [Tips for troubleshooting Lambda functions](../StreamProcessing/TroubleshootingLambda.md)
  * [Stream processing wrap up](../StreamProcessing/StreamProcessingWrapup.md)
* [Building the ASP.NET Core Frontend](../ASP.NETCoreFrontend/TheFrontend.md)
* [Final Wrap Up](../FinalWrapup.md)

Continue on to next page: [Deploy Lambda Function](../StreamProcessing/DeployLambdaFunction.md)

