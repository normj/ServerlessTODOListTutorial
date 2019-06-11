# Getting Started

To get started with this tutorial you need a few things setup first.

## Have an AWS Account

To follow along with this tutorial you will need an AWS account. An AWS 
account can be setup [here](https://portal.aws.amazon.com/billing/signup).

## AWS Credentials

To access AWS you will need AWS credentials which are made up of an Access Key ID and a Secret Key. The credentials need to be saved as a profile with either the [AWS Toolkit for Visual Studio](https://docs.aws.amazon.com/toolkit-for-visual-studio/latest/user-guide/getting-set-up.html) or in the 
[Shared Credentials](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html#creds-file) file.

## Choosing a credentials profile and aws region

To build our application we need to choose which AWS credentials profile
to use and what AWS region to use.

Throughout this tutorial whenever a code snippet is run the **AWS_PROFILE** and **AWS_REGION** environment variable will be set
so that the code snippets will use the appropiate profile and region 
when making calls to AWS.

# Set AWS Credentials Profile

Enter the profile you wish to use for this tutorial and click the execute button.

```cs --source-file ./Snippets/SetConfiguration.cs --project ./Snippets/Snippets.csproj --region current_aws_profile
```

# Set AWS Region

Enter the region you wish to use for this tutorial and click the execute button.

```cs --source-file ./Snippets/SetConfiguration.cs --project ./Snippets/Snippets.csproj --region current_aws_region
```
