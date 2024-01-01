// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.PDF;

using System;
using System.IO;
using System.Threading.Tasks;
using Common.Threading;
using Microsoft.Win32;
using PuppeteerSharp;

/// <summary>
/// Provides functionality for generating PDF documents using a Chromium-based
/// web browser in headless mode.
/// </summary>
public class ChromiumPdfGenerator : IPdfGenerator
{
    /// <summary>
    /// The location of the Edge web browser installation.
    /// </summary>
    private const string EdgePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe";

    /// <summary>
    /// The registry key to check whether Chrome is installed.
    /// </summary>
    private const string ChromeRegistryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Google Chrome";

    /// <summary>
    /// The registry key value to get the Chrome install directory.
    /// </summary>
    private const string ChromeRegistryValue = "InstallLocation";

    /// <summary>
    /// The name of the Chrome web browser executable.
    /// </summary>
    private const string ChromeExecutable = "chrome.exe";

    /// <summary>
    /// The folder name to download Chrome to.
    /// </summary>
    private const string ChromiumDownloadFolder = ".local-chromium";

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGenerator"/> class.
    /// </summary>
    public ChromiumPdfGenerator()
        : this(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGenerator"/> class.
    /// </summary>
    /// <param name="downloadEnabled">Whether to download Chrome if it is not installed.</param>
    public ChromiumPdfGenerator(bool downloadEnabled)
    {
        this.DownloadEnabled = downloadEnabled;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to download Chrome if it is not installed.
    /// </summary>
    public bool DownloadEnabled
    {
        get;
        set;
    }

    /// <summary>
    /// Generates a PDF from an HTML file.
    /// </summary>
    /// <param name="htmlPath">The path to the HTML file to generator the PDF from.</param>
    /// <param name="pdfPath">The path to save the generated PDF file.</param>
    public void FromHtml(string htmlPath, string pdfPath)
    {
        if (string.IsNullOrEmpty(htmlPath))
        {
            throw new ArgumentException("Path cannot be null or empty", nameof(htmlPath));
        }

        if (string.IsNullOrEmpty(pdfPath))
        {
            throw new ArgumentException("Path cannot be null or empty", nameof(pdfPath));
        }

        AsyncHelper.RunSync(() => this.ToPdfAsync(htmlPath, pdfPath));
    }

    private async Task ToPdfAsync(string htmlPath, string pdfPath)
    {
        string chromePath = await this.GetChromiumPath();
        LaunchOptions options = new()
        {
            ExecutablePath = chromePath,
            Headless = true,
        };

        using Browser browser = await Puppeteer.LaunchAsync(options);
        using Page page = await browser.NewPageAsync();

        await page.GoToAsync(htmlPath);
        await page.PdfAsync(pdfPath, new PdfOptions());
    }

    private async Task<string> GetChromiumPath()
    {
        // Check if Edge is already installed (typically available on most Windows installs).
        if (File.Exists(EdgePath))
        {
            return EdgePath;
        }

        // Check if Chrome is already installed.
        string chromePath = this.GetInstalledChromePath();
        if (!string.IsNullOrEmpty(chromePath))
        {
            return chromePath;
        }

        // Chrome not installed, check if download is enabled.
        if (!this.DownloadEnabled)
        {
            throw new InvalidOperationException("Chrome is not installed and downloading it is disabled.");
        }

        // Download a copy of Chromium.
        string downloadFolder = Path.Combine(AppContext.BaseDirectory, ChromiumDownloadFolder);
        return await this.DownloadChromium(downloadFolder);
    }

    private string GetInstalledChromePath()
    {
        string chromePath = Registry.GetValue(ChromeRegistryKey, ChromeRegistryValue, null) as string;
        if (string.IsNullOrEmpty(chromePath))
        {
            return null;
        }

        chromePath = Path.Combine(chromePath, ChromeExecutable);
        if (!File.Exists(chromePath))
        {
            return null;
        }

        return chromePath;
    }

    private async Task<string> DownloadChromium(string path)
    {
        BrowserFetcherOptions options = new()
        {
            Path = path,
        };
        BrowserFetcher fetcher = new(options);
        RevisionInfo info = await fetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
        return info.ExecutablePath;
    }
}
