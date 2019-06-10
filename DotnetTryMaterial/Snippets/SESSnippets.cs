using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Snippets
{
    public class SESSnippets
    {
        public static async Task SendVerificationAsync()
        {
            #region send_verification_email
            using (var sesClient = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
            {
                var request = new VerifyEmailAddressRequest
                {
                    EmailAddress = ""
                };

                if(!string.IsNullOrEmpty(request.EmailAddress))
                {
                    await sesClient.VerifyEmailAddressAsync(request);
                    Console.WriteLine("Email send, check your email to verify the email address");
                }
                else
                {
                    Console.Error.WriteLine("You must set an email in the request to send the verification email");
                }
            }
            #endregion
        }
    }
}
