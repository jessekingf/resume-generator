// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Resume
{
    using System;
    using System.IO;
    using Resume.Core;

    /// <summary>
    /// The entry class of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The application arguments.</param>
        public static void Main(string[] args)
        {
            //// TODO: Proper argument parsing and error handling.
            if (args.Length != 2)
            {
                Console.Error.Write("Invalid augments.");
                Environment.Exit(1);
            }

            string resumeJsonPath = Path.GetFullPath(args[0]);
            string outputPath = Path.GetFullPath(args[1]);

            //// TODO: Validate parameters/paths.

            ResumeController resumeController = new ResumeController();
            resumeController.GenerateResume(resumeJsonPath, outputPath);
        }
    }
}
