namespace Common.PDF;

using System;
using System.Runtime.InteropServices;
using Common.IO;
using Microsoft.Win32;

/// <summary>
/// Provides functionality for generating PDF documents using a Chromium-based
/// web browser in headless mode on Windows.
/// </summary>
public class ChromiumPdfGeneratorWindows : ChromiumPdfGeneratorBase
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
    /// Provides access to the file system.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChromiumPdfGeneratorWindows"/> class.
    /// </summary>
    /// <param name="fileSystem">Provides access to the file system.</param>
    public ChromiumPdfGeneratorWindows(IFileSystem fileSystem)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("Only supported in Windows.");
        }

        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <inheritdoc />
    protected override string GetChromiumPath()
    {
        // Check if Edge is already installed (typically available on most Windows installs).
        if (this.fileSystem.FileExists(EdgePath))
        {
            return EdgePath;
        }

        // Check if Chrome is already installed.
        string chromePath = this.GetInstalledChromePath();
        if (!string.IsNullOrEmpty(chromePath))
        {
            return chromePath;
        }

        return null;
    }

    private string GetInstalledChromePath()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("Only supported in Windows.");
        }

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
}
