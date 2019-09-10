using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace Snippets
{
    public class ConfigureEventSourceMapping
    {
        public static async Task AddEventSourceAsync()
        {
            #region add_dynamodb_event_source
            using (var streamClient = new AmazonDynamoDBStreamsClient())
            using (var lambdaClient = new AmazonLambdaClient())
            {
                // TODO: Enter Lambda function name, this is most likely: ServerlessTODOListStreamProcessor
                var functionName = "";
                if(string.IsNullOrEmpty(functionName))
                {
                    Console.Error.WriteLine("You must set the name of the Lambda function you deployed to the \"functionName\" variable");
                    return;
                }

                var listRequest = new ListStreamsRequest
                {
                    TableName = "TODOList"
                };

                var listResponse = await streamClient.ListStreamsAsync(listRequest);
                if(listResponse.Streams.Count == 0)
                {
                    Console.Error.WriteLine($"A stream is not enabled for the table {listRequest.TableName}");
                    return;
                }

                var streamArn = listResponse.Streams[0].StreamArn;
                Console.WriteLine($"Stream ARN is {streamArn}");

                var request = new CreateEventSourceMappingRequest
                {
                    FunctionName = functionName,
                    EventSourceArn = streamArn,
                    StartingPosition = EventSourcePosition.LATEST,
                    BatchSize = 100
                };

                await lambdaClient.CreateEventSourceMappingAsync(request);
                Console.WriteLine($"Event source mapping made between stream {streamArn} and function {functionName}");
            }
            #endregion
        }
    }
}
