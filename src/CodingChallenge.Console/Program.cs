﻿using CodingChallenge.Application;
using CodingChallenge.Infrastructure;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodingChallenge.Console;

partial class Program
{
    static IConfiguration configuration;
    static ILogger logger;
    static ServiceProvider serviceProvider;

    static void Main(string[] args)
    {
        LoadConfiguration();
        SetupLogger();
        ConfigureServices(new ServiceCollection());
        var commandParser = serviceProvider.GetService<NFTRecordConsoleRunner>();
        var result = CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
        .WithParsedAsync(async options => await commandParser.RunOptionsAsync(options)).GetAwaiter().GetResult()
        .WithNotParsed(errs => commandParser.HandleParseError(errs));
    }
    static void LoadConfiguration()
    {
        configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables().Build();
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationBaseDependencies();
        services.AddInfrastructureDependencies(configuration);
        services.AddSingleton<ILogger>(logger);
        services.AddTransient<NFTRecordCommandController, NFTRecordCommandController>();
        services.AddTransient<NFTRecordConsoleRunner, NFTRecordConsoleRunner>();
        serviceProvider = services.BuildServiceProvider();
    }

    static void SetupLogger()
    {
        var loggerFactory = LoggerFactory.Create(
                builder => builder
                            // add debug output as logging target
                            //.AddDebug()
                            .AddConsole()
                            // set minimum level to log
                            .SetMinimumLevel(LogLevel.Information));
        logger = loggerFactory.CreateLogger("CodingChallenge Console App");
    }

}