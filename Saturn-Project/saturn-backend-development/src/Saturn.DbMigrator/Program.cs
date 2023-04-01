using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Saturn.DbMigrator;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Saturn", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Saturn", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        await CreateHostBuilder(args).RunConsoleAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
#if DEBUG
                .AddAppSettingsSecretsJson()
#else
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var envName = Environment.GetEnvironmentVariable("RUNNING_ENVIRONMENT")!.ToLower();
                    var credentials = GetAmazonCredentialsFromEnvironmentVariables();
                    var region = GetRegionEndpoint();
                    config.AddSystemsManager(configureSource =>
                    {
                        configureSource.Path = $"/{envName}";
                        configureSource.Optional = false;
                        configureSource.AwsOptions = new AWSOptions()
                        {
                            Credentials = credentials,
                            Region = region
                        };
                    });
                })
#endif
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<DbMigratorHostedService>();
            });

    private static RegionEndpoint GetRegionEndpoint()
    {
        var region = Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION");
        if (region == "us-east-2")
        {
            return RegionEndpoint.USEast2;
        }
        return RegionEndpoint.USEast1;
    }

    private static BasicAWSCredentials GetAmazonCredentialsFromEnvironmentVariables()
    {
        var awsId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        var awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        if (!string.IsNullOrWhiteSpace(awsId) && !string.IsNullOrWhiteSpace(awsSecretKey))
        {
            Console.WriteLine(awsId);
            return new BasicAWSCredentials(awsId, awsSecretKey);
        }
        // This method is private and has only one specific purpose so if it fails that it doesn't have to return anything or throw an exception.
        // That is why is this particular case I am returning a null.
        return null;
    }
}
