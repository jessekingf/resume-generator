namespace Common.PDF;

using System;
using System.IO;
using System.Threading.Tasks;
using Common.Threading;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;

/// <summary>
/// Provides common functionality for generating PDF documents using a Chromium-based
/// web browser in headless mode.
/// </summary>
public abstract class ChromiumPdfGeneratorBase : IPdfGenerator
{
    /// <summary>
    /// The folder name to download Chrome to.
    /// </summary>
    private const string ChromiumDownloadFolder = ".local-chromium";

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGeneratorBase"/> class.
    /// </summary>
    /// <param name="downloadEnabled">Whether to download Chrome if it is not installed.</param>
    protected ChromiumPdfGeneratorBase(bool downloadEnabled = false)
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

    /// <summary>
    /// Converts the HTML to a PDF using a headless Chromium-based browser instance.
    /// </summary>
    /// <param name="htmlPath">The path to the HTML document.</param>
    /// <param name="pdfPath">The path to write the resulting PDF document.</param>
    /// <returns>The asynchronous operation.</returns>
    protected virtual async Task ToPdfAsync(string htmlPath, string pdfPath)
    {
        string chromePath = this.GetChromiumPath();
        if (string.IsNullOrEmpty(chromePath))
        {
            if (!this.DownloadEnabled)
            {
                throw new InvalidOperationException("Chrome is not installed and downloading it is disabled.");
            }

            string downloadFolder = Path.Combine(AppContext.BaseDirectory, ChromiumDownloadFolder);
            chromePath = await this.DownloadChromium(downloadFolder);
        }

        LaunchOptions options = new()
        {
            ExecutablePath = chromePath,
            Headless = true,
        };

        using IBrowser browser = await Puppeteer.LaunchAsync(options);
        using IPage page = await browser.NewPageAsync();

        await page.GoToAsync($"file://{htmlPath}");
        await page.PdfAsync(pdfPath, new PdfOptions());
    }

    /// <summary>
    /// Get the path to the Chromium executable.
    /// </summary>
    /// <returns>The Chromium executable path or null if not installed.</returns>
    protected abstract string GetChromiumPath();

    /// <summary>
    /// Downloads a Chromium instance.
    /// </summary>
    /// <param name="path">The path do download Chromium to.</param>
    /// <returns>The path to the downloaded Chromium instance.</returns>
    protected virtual async Task<string> DownloadChromium(string path)
    {
        BrowserFetcherOptions options = new()
        {
            Path = path,
        };
        BrowserFetcher fetcher = new(options);
        InstalledBrowser browserInfo = await fetcher.DownloadAsync();
        return browserInfo.GetExecutablePath();
    }
}
