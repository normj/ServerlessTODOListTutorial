using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ServerlessTODOList.Frontend
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder.AddSystemsManager("/ServerlessTODOList/");
                })
                .UseStartup<Startup>();
        }
    }
}
