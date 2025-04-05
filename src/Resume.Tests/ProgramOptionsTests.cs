namespace Resume.Tests;

using System;
using Common.Extensions;
using Common.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/// <summary>
/// A test fixture for the <see cref="ProgramOptions"/> class.
/// </summary>
[TestClass]
public class ProgramOptionsTests
{
    /// <summary>
    /// The file system mock to inject for the tests.
    /// </summary>
    private Mock<IFileSystem> fileSystemMock;

    /// <summary>
    /// Setup run before each test.
    /// </summary>
    [TestInitialize]
    public void SetupTest()
    {
        this.fileSystemMock = new Mock<IFileSystem>();
    }

    /// <summary>
    /// A constructor test where the file system parameter is null.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    public void ProgramOptions_NullFileSystem_Throws()
    {
        // Setup the input.
        string[] args = [@"--help"];

        try
        {
            // Run the test.
            ProgramOptions target = new ProgramOptions(args, null);
        }
        catch (ArgumentNullException ex)
        {
            // Validate the results.
            Assert.AreEqual("fileSystem", ex.ParamName);
            throw;
        }
    }

    /// <summary>
    /// A constructor test where the arguments are null.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_NullArgs_DisplayHelpSet()
    {
        // Setup the input.
        string[] args = null;

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayHelp);
    }

    /// <summary>
    /// A constructor test where there are no arguments (empty).
    /// </summary>
    [TestMethod]
    public void ProgramOptions_NoArgs_DisplayHelpSet()
    {
        // Setup the input.
        string[] args = Array.Empty<string>();

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayHelp);
    }

    /// <summary>
    /// A constructor test where the help flag was passed in the arguments.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_HelpFlag_DisplayHelpSet()
    {
        // Setup the input.
        string[] args = [@"--help"];

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayHelp);
    }

    /// <summary>
    /// A constructor test where the short style help flag was passed in the arguments.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_HelpFlagShort_DisplayHelpSet()
    {
        // Setup the input.
        string[] args = [@"-h"];

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayHelp);
    }

    /// <summary>
    /// A constructor test where the question mark style help flag was passed in the arguments.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_HelpFlagQuestionMark_DisplayHelpSet()
    {
        // Setup the input.
        string[] args = [@"/?"];

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayHelp);
    }

    /// <summary>
    /// A constructor test where the version flag was passed in the arguments.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_VersionFlag_DisplayVersionSet()
    {
        // Setup the input.
        string[] args = [@"--version"];

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayVersion);
    }

    /// <summary>
    /// A constructor test where the short style version flag was passed in the arguments.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_VersionFlagShort_DisplayVersionSet()
    {
        // Setup the input.
        string[] args = [@"-v"];

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.IsTrue(target.DisplayVersion);
    }

    /// <summary>
    /// A constructor test with valid input and output paths.
    /// </summary>
    [TestMethod]
    public void ProgramOptions_ValidPaths_PathsSet()
    {
        // Setup the input.
        string inputPath = @"/temp/resume.json";
        string outputPath = @"/temp/output/";
        string[] args = [inputPath, outputPath];

        this.fileSystemMock.Setup(fs => fs.FileExists(inputPath))
            .Returns(true);
        this.fileSystemMock.Setup(fs => fs.DirectoryExists(outputPath))
           .Returns(true);

        // Run the test.
        ProgramOptions target = new ProgramOptions(args, this.fileSystemMock.Object);

        // Validate the results.
        Assert.AreEqual(inputPath, target.InputPath);
        Assert.AreEqual(outputPath, target.OutputPath);
    }

    /// <summary>
    /// A constructor test where the input path is not found.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOptionException))]
    public void ProgramOptions_InputPathNotFound_Throws()
    {
        // Setup the input.
        string inputPath = @"/temp/resume.json";
        string outputPath = @"/temp/output/";
        string[] args = [inputPath, outputPath];

        this.fileSystemMock.Setup(fs => fs.FileExists(inputPath))
            .Returns(false);
        this.fileSystemMock.Setup(fs => fs.DirectoryExists(outputPath))
           .Returns(true);

        // Run the test.
        _ = new ProgramOptions(args, this.fileSystemMock.Object);
    }

    /// <summary>
    /// A constructor test where the output path is not found.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOptionException))]
    public void ProgramOptions_OutputPathNotFound_Throws()
    {
        // Setup the input.
        string inputPath = @"/temp/resume.json";
        string outputPath = @"/temp/output/";
        string[] args = [inputPath, outputPath];

        this.fileSystemMock.Setup(fs => fs.FileExists(inputPath))
            .Returns(true);
        this.fileSystemMock.Setup(fs => fs.DirectoryExists(outputPath))
           .Returns(false);

        // Run the test.
        _ = new ProgramOptions(args, this.fileSystemMock.Object);
    }

    /// <summary>
    /// A constructor test where not enough arguments have been specified.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOptionException))]
    public void ProgramOptions_NotEnoughArgs_Throws()
    {
        // Setup the input.
        string[] args = [@"arg1"];

        // Run the test.
        _ = new ProgramOptions(args, this.fileSystemMock.Object);
    }

    /// <summary>
    /// A constructor test where to many arguments have been specified.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOptionException))]
    public void ProgramOptions_TooManyArgs_Throws()
    {
        // Setup the input.
        string[] args = [@"arg1", @"arg2", @"arg3"];

        // Run the test.
        _ = new ProgramOptions(args, this.fileSystemMock.Object);
    }
}
