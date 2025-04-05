namespace Common.PDF;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Provides functionality for generating PDF documents using a Chromium-based
/// web browser in headless mode on Linux.
/// </summary>
public class ChromiumPdfGeneratorLinux : ChromiumPdfGeneratorBase
{
    private const string ChromiumInstallPath = @"/usr/bin/chromium";

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGeneratorLinux"/> class.
    /// </summary>
    /// <param name="downloadEnabled">Whether to download Chrome if it is not installed.</param>
    public ChromiumPdfGeneratorLinux(bool downloadEnabled = false)
        : base(downloadEnabled)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new PlatformNotSupportedException("Only supported in Linux.");
        }

        // TODO: Inject filesystem.
    }

    /// <inheritdoc />
    protected override string GetChromiumPath()
    {
        if (File.Exists(ChromiumInstallPath))
        {
            return ChromiumInstallPath;
        }

        return null;
    }
}
