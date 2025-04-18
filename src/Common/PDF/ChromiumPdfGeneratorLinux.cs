namespace Common.PDF;

using System;
using System.Runtime.InteropServices;
using Common.IO;

/// <summary>
/// Provides functionality for generating PDF documents using a Chromium-based
/// web browser in headless mode on Linux.
/// </summary>
public class ChromiumPdfGeneratorLinux : ChromiumPdfGeneratorBase
{
    /// <summary>
    /// The path to the Chromium executable on Linux.
    /// </summary>
    private const string ChromiumInstallPath = @"/usr/bin/chromium";

    /// <summary>
    /// Provides access to the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGeneratorLinux"/> class.
    /// </summary>
    /// <param name="fileSystem">Provides access to the file system.</param>
    public ChromiumPdfGeneratorLinux(IFileSystem fileSystem)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new PlatformNotSupportedException("Only supported in Linux.");
        }

        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <inheritdoc />
    protected override string GetChromiumPath()
    {
        if (this.fileSystem.FileExists(ChromiumInstallPath))
        {
            return ChromiumInstallPath;
        }

        return null;
    }
}
