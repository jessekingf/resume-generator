// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Reflection
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Helper methods for getting assembly information.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the directory the process executable is located in.
        /// </summary>
        /// <returns>The directory the process executable is located in.</returns>
        public static string GetEntryAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}
