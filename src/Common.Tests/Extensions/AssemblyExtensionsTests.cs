// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Tests.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using Common.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// TestFixture for the <see cref="Common.Extensions.AssemblyExtensions"/> class.
    /// </summary>
    [TestClass]
    public class AssemblyExtensionsTests
    {
        /// <summary>
        /// A test for the Assembly ReadResourceFile extension method with valid input.
        /// </summary>
        [TestMethod]
        public void ReadResourceFile_ValidInput_ResourceTextReturned()
        {
            // Setup the test.
            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "Extensions.TestResourceFile.txt";
            string expectedResults = "This is a test resource file!";

            // Run the test.
            string results = assembly.ReadResourceFile(filename);

            // Validate the results.
            Assert.AreEqual(expectedResults, results);
        }

        /// <summary>
        /// A test for the Assembly ReadResourceFile extension method with a null filename parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadResourceFile_NullFilename_ThrowsArgumentException()
        {
            // Setup the test.
            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = null;

            try
            {
                // Run the test.
                string results = assembly.ReadResourceFile(filename);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual("filename", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for the Assembly ReadResourceFile extension method with an empty filename parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadResourceFile_EmptyFilename_ThrowsArgumentException()
        {
            // Setup the test.
            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = string.Empty;

            try
            {
                // Run the test.
                string results = assembly.ReadResourceFile(filename);
            }
            catch (ArgumentException ex)
            {
                // Validate the results.
                Assert.AreEqual("filename", ex.ParamName);
                throw;
            }
        }

        /// <summary>
        /// A test for the Assembly ReadResourceFile extension method where the file name specified does not exist in the assembly.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadResourceFile_EmptyFilename_ThrowsFileNotFoundException()
        {
            // Setup the test.
            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "Invalid";

            try
            {
                // Run the test.
                string results = assembly.ReadResourceFile(filename);
            }
            catch (FileNotFoundException ex)
            {
                // Validate the results.
                Assert.AreEqual(filename, ex.FileName);
                throw;
            }
        }
    }
}
