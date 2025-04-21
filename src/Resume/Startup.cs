namespace Resume;

using System.Runtime.InteropServices;
using Common.IO;
using Common.Markdown;
using Common.PDF;
using Common.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resume.Commands;
using Resume.Core;
using Resume.Core.Renderers;

/// <summary>
/// Configures the application on startup.
/// </summary>
internal class Startup
{
    /// <summary>
    /// Creates the application host.
    /// </summary>
    /// <param name="args">The application arguments.</param>
    /// <returns>The application host.</returns>
    public static IHost CreateHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            })
            .Build();
    }

    /// <summary>
    /// Configures the application services.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    private static void ConfigureServices(IServiceCollection services)
    {
        RegisterCommands(services);
        RegisterResumeGeneration(services);
    }

    private static void RegisterCommands(IServiceCollection services)
    {
        services.AddTransient<HelpCommand>();
        services.AddTransient<ResumeCommand>();
        services.AddTransient<VersionCommand>();
    }

    private static void RegisterResumeGeneration(IServiceCollection services)
    {
        services.AddTransient<ResumeController>();

        services.AddTransient<ISerializer, JsonSerializer>();
        services.AddTransient<IMarkdownConverter, MarkdownConverter>();
        services.AddTransient<IFileSystem, FileSystem>();

        services.AddTransient<IResumeTextRenderer, ResumeMarkdownRenderer>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            services.AddTransient<IPdfGenerator, ChromiumPdfGeneratorWindows>();
        }
        else
        {
            services.AddTransient<IPdfGenerator, ChromiumPdfGeneratorLinux>();
        }
    }
}
