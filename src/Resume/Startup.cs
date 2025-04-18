namespace Resume;

using System.Runtime.InteropServices;
using Common.Extensions;
using Common.IO;
using Common.Markdown;
using Common.PDF;
using Common.Serialization;
using Common.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resume.Core;
using Resume.Core.Model;

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
        services.AddTransient<ResumeController>();
        services.AddTransient<ISerializer, JsonSerializer>();
        services.AddTransient<IMarkdownConverter, MarkdownConverter>();
        services.AddTransient<IFileSystem, FileSystem>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            services.AddTransient<IPdfGenerator, ChromiumPdfGeneratorWindows>();
        }
        else
        {
            services.AddTransient<IPdfGenerator, ChromiumPdfGeneratorLinux>();
        }

        services.AddTransient<ITemplate>(provider =>
        {
            string templateText = typeof(ResumeController).Assembly
                .ReadResourceFile("ResumeMarkdownTemplate.liquid");

            LiquidTemplate template = new(templateText);

            template.RegisterType(typeof(Resume));
            template.RegisterType(typeof(EducationProgram));
            template.RegisterType(typeof(Job));
            template.RegisterType(typeof(Skill));
            template.RegisterType(typeof(Address));

            return template;
        });
    }
}
