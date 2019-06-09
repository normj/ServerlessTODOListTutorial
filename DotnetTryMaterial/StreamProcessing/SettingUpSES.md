# Setting up Amazon Simple Email Service (SES)

SES is a cloud-based email sending service. It designed to handle any size workloads whether that is marketers
send large marketing message or for small applications like ours to send emails to people that 
have been assigned a new task.

AWS accounts that start using SES are initial in what is called sandbox mode. Where each mail that is sent has to be
to a verified email address. The sandbox mode is used to help prevent fraud and abuse, and to help protect your reputation 
as a sender, SES apply certain restrictions to new Amazon SES accounts.

Checkout the developer guide about [setting up email](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/setting-up-email.html) or [moving your account of the sandbox](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/request-production-access.html).

## Verifing Email Address

Since your account is most likely in the sandbox mode to see the emails our Lambda function send you
need to verify each of the emails you plan on testing with for both the From and To. You can verify
emails either through the [AWS console](https://console.aws.amazon.com/ses/home?region=us-east-1#verified-senders-email:) or by enter an email in the code snippet below and executing it.

```cs --source-file ../Snippets/SESSnippets.cs --project ../Snippets/Snippets.csproj --region send_verification_email
```