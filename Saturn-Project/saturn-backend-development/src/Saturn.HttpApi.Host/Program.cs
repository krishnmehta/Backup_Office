using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Saturn;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting Saturn.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
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
                        Credentials = credentials, Region = region
                    };
                });
            })
#endif
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<SaturnHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

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
