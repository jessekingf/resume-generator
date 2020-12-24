// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume.Core.Tests
{
    using System;
    using System.IO;
    using Common.IO;
    using Common.Markdown;
    using Common.PDF;
    using Common.Reflection;
    using Common.Serialization;
    using Common.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Resume.Core.Model;

    /// <summary>
    /// A test fixture for the <see cref="ResumeController"/>  class.
    /// </summary>
    [TestClass]
    public class ResumeControllerTests
    {
        /// <summary>
        /// The serializer to load the resume data with.
        /// </summary>
        private ISerializer serializer;

        /// <summary>
        /// Handles converting Markdown.
        /// </summary>
        private IMarkdownConverter markdownConverter;

        /// <summary>
        /// PDF document generator.
        /// </summary>
        private Mock<IPdfGenerator> pdfGeneratorMock;

        /// <summary>
        /// File system mock for the unit tests.
        /// </summary>
        private Mock<IFileSystem> fileSystemMock;

        /// <summary>
        /// Initialization run before each test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            // Use actual serializer and markdown implementations for the tests.
            this.serializer = new JsonSerializer();
            this.markdownConverter = new MarkdownConverter();

            // Mock the file system and PDF calls.
            this.pdfGeneratorMock = new Mock<IPdfGenerator>();
            this.fileSystemMock = new Mock<IFileSystem>();
        }

        /// <summary>
        /// A good case constructor test.
        /// </summary>
        [TestMethod]
        public void ResumeController_ValidParameters_Success()
        {
            ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A constructor test with a null serializer parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResumeController_NullSerializer_Throws()
        {
            try
            {
                _ = new ResumeController(null, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("serializer", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A constructor test with a null markdown converter parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResumeController_NullMarkdownConverter_Throws()
        {
            try
            {
                _ = new ResumeController(this.serializer, null, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("markdownConverter", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A constructor test with a null PDF generator parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResumeController_NullPdfGenerator_Throws()
        {
            try
            {
                _ = new ResumeController(this.serializer, this.markdownConverter, null, this.fileSystemMock.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("pdfGenerator", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A constructor test with a null file system parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResumeController_NullFileSystem_Throws()
        {
            try
            {
                _ = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("fileSystem", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume with a null resume JSON path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateResume_NullResumeJsonPath_Throws()
        {
            // Setup the test.
            string resumeJsonPath = null;
            string outputPath = @"c:\another\path";
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual(nameof(resumeJsonPath), ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume with an empty resume JSON path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateResume_EmptyResumeJsonPath_Throws()
        {
            // Setup the test.
            string resumeJsonPath = string.Empty;
            string outputPath = @"c:\another\path";
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual(nameof(resumeJsonPath), ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume with a null output path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateResume_NullOutputPath_Throws()
        {
            // Setup the test.
            string resumeJsonPath = @"c:\path\resume.json";
            string outputPath = null;
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual(nameof(outputPath), ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume with an empty output path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateResume_EmptyOutputPath_Throws()
        {
            // Setup the test.
            string resumeJsonPath = @"c:\path\resume.json";
            string outputPath = string.Empty;
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual(nameof(outputPath), ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume with a null template.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateResume_NullTemplate_Throws()
        {
            // Setup the test.
            string resumeJsonPath = @"c:\path\resume.json";
            string outputPath = @"c:\another\path";
            ITemplate markdownTemplate = null;

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplate);
            }
            catch (ArgumentNullException ex)
            {
                // Validate the results.
                Assert.AreEqual(nameof(markdownTemplate), ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume where the JSON resume input is not found.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GenerateResume_JsonResumeNotFound_Throws()
        {
            // Setup the test.
            string resumeJsonPath = @"c:\path\resume.json";
            string outputPath = @"c:\another\path";
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            this.fileSystemMock.Setup(fs => fs.FileExists(resumeJsonPath))
                .Returns(false);
            this.fileSystemMock.Setup(fs => fs.DirectoryExists(outputPath))
                .Returns(true);

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (FileNotFoundException ex)
            {
                // Validate the results.
                Assert.AreEqual(resumeJsonPath, ex.FileName);
                throw;
            }
        }

        /// <summary>
        /// A test for GenerateResume where the output path is not found.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void GenerateResume_OutputPathNotFound_Throws()
        {
            // Setup the test.
            string resumeJsonPath = @"c:\path\resume.json";
            string outputPath = @"c:\another\path";
            Mock<ITemplate> markdownTemplateMock = new Mock<ITemplate>();

            this.fileSystemMock.Setup(fs => fs.FileExists(resumeJsonPath))
                .Returns(true);
            this.fileSystemMock.Setup(fs => fs.DirectoryExists(outputPath))
                .Returns(false);

            try
            {
                // Run the test.
                ResumeController target = new ResumeController(this.serializer, this.markdownConverter, this.pdfGeneratorMock.Object, this.fileSystemMock.Object);
                target.GenerateResume(resumeJsonPath, outputPath, markdownTemplateMock.Object);
            }
            catch (DirectoryNotFoundException ex)
            {
                // Validate the results.
                Assert.AreEqual($"The output directory does not exist: {outputPath}", ex.Message);
                throw;
            }
        }

        //// TODO: Good case generation tests.
    }
}
