// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides functionality for rendering text based templates.
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Parses the specified template to render.
        /// </summary>
        /// <param name="template">The template to parse and render.</param>
        void Parse(string template);

        /// <summary>
        /// Registers a type used by the template.
        /// </summary>
        /// <param name="type">The type to register with the template.</param>
        void RegisterType(Type type);

        /// <summary>
        /// Renders the template.
        /// </summary>
        /// <param name="context">The values and parameters to render the template with.</param>
        /// <returns>The rendered template.</returns>
        string Render(TemplateContext context);
    }
}
