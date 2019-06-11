using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;

namespace Snippets
{
    public class EnableDynamoDBStream
    {
        public static async Task EnableAsync()
        {
            #region enable_stream_status
            using (var ddbClient = new AmazonDynamoDBClient())
            {
                var request = new UpdateTableRequest
                {
                    TableName = "TODOList",
                    StreamSpecification = new StreamSpecification
                    {
                        StreamEnabled = true,
                        StreamViewType = StreamViewType.NEW_AND_OLD_IMAGES
                    }
                };

                var response = await ddbClient.UpdateTableAsync(request);
                if((response.TableDescription.StreamSpecification?.StreamEnabled).GetValueOrDefault())
                {
                    Console.WriteLine($"Stream enabled with view type {response.TableDescription.StreamSpecification.StreamViewType}");
                }
                else
                {
                    Console.WriteLine($"Stream is disabled");
                }                
            }
            #endregion
        }

        public static async Task CheckStatusAsync()
        {
            #region check_stream_status
            using (var ddbClient = new AmazonDynamoDBClient())
            {
                var request = new DescribeTableRequest
                {
                    TableName = "TODOList"
                };

                var response = await ddbClient.DescribeTableAsync(request);
                Console.WriteLine($"Table {response.Table.TableName} status is {response.Table.TableStatus}");
                if ((response.Table.StreamSpecification?.StreamEnabled).GetValueOrDefault())
                {
                    Console.WriteLine($"Stream enabled with view type {response.Table.StreamSpecification.StreamViewType}");
                }
                else
                {
                    Console.WriteLine($"Stream is disabled");
                }
            }
            #endregion
        }

        public static async Task TestStreamAsync()
        {
            var config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2,
                ConsistentRead = true
            };
            var Context = new DynamoDBContext(new AmazonDynamoDBClient());

            #region test_stream_read

            // Save list to table
            var list = new TODOList
            {
                User = "testuser",
                ListId = Guid.NewGuid().ToString(),
                Complete = false,
                Name = "StreamTest",
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Items = new List<TODOListItem>
                {
                    new TODOListItem { Description = "Task1", Complete = true },
                    new TODOListItem { Description = "Task2", Complete = false }
                }
            };

            await Context.SaveAsync(list);
            
            using (var streamClient = new AmazonDynamoDBStreamsClient())
            {
                // Function for reading records continuously from a shard.
                Func<string, CancellationToken, Task> shardReader = async (iterator, token) =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        var response = (await streamClient.GetRecordsAsync(new GetRecordsRequest { ShardIterator = iterator }));

                        // Update position in shard iterator
                        iterator = response.NextShardIterator;

                        // This is what you would write in a Lambda function processing DynamoDB Streams.
                        foreach (var record in response.Records)
                        {
                            var newVersion = record.Dynamodb.NewImage;
                            Console.WriteLine($"Item read: {newVersion["User"].S}/{newVersion["ListId"].S}");
                        }
                    }
                };


                // Find the arn for the DynamoDB Stream
                var streamArn = (await streamClient.ListStreamsAsync(new ListStreamsRequest { TableName = "TODOList" }))
                                    .Streams.FirstOrDefault()?.StreamArn;

                Console.WriteLine($"The stream arn is {streamArn} shards");

                // A stream is made of shards.
                var shards = (await streamClient.DescribeStreamAsync(new DescribeStreamRequest { StreamArn = streamArn }))
                                    .StreamDescription.Shards;

                Console.WriteLine($"The stream currently has {shards.Count} shards");


                // Execute a separate reader for each shard.
                var cancelSource = new CancellationTokenSource();
                var readerTasks = new Task[shards.Count];
                for(int i = 0; i < readerTasks.Length; i++)
                {
                    var shardIteratorRequest = new GetShardIteratorRequest
                    {
                        StreamArn = streamArn,
                        ShardId = shards[i].ShardId,
                        ShardIteratorType = ShardIteratorType.TRIM_HORIZON
                    };

                    var shardIterator = (await streamClient.GetShardIteratorAsync(shardIteratorRequest))
                                        .ShardIterator;
                    Console.WriteLine($"Shard Iterator {i}: {shardIterator}");

                    readerTasks[i] = shardReader(shardIterator, cancelSource.Token);
                }

                Task.WaitAll(readerTasks, TimeSpan.FromSeconds(5));
                cancelSource.Cancel();
            }
            #endregion
        }
    }
}
