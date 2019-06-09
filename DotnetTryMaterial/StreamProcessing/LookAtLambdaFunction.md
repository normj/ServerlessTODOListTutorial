# Assign Task Lambda Function

In our application's solution the **ServerlessTODOList.StreamProcessor** project contains the .NET Core Lambda function.
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