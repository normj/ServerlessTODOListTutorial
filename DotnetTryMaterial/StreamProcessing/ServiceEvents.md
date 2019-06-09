# Using Lambda to Handle Service Events

Many AWS services trigger events when something changes. For example with Amazon S3 when a new object is uploaded 
to an S3 bucket an event is triggered.

A Lambda function can be configured to respond to the event. Here are some examples of AWS services that can be 
configured to trigger Lambda functions for their events.

* Amazon S3
* Amazon SNS
* Amazon SQS
* AWS Step Functions
* Kinesis
* Amazon DynamoDB
* [Others](https://docs.aws.amazon.com/lambda/latest/dg/lambda-services.html)

