// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Helper methods for getting assembly information.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the directory of an assembly by a given type.
        /// </summary>
        /// <param name="type">The type to locate the assembly with.</param>
        /// <returns>The directory the assembly is located in.</returns>
        public static string GetAssemblyDirectory(Type type)
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(type).Location);
        }

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
