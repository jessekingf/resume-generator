// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Values and parameters for rendering a template.
    /// </summary>
    public class TemplateContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateContext"/> class.
        /// </summary>
        public TemplateContext()
        {
            this.Values = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateContext"/> class.
        /// </summary>
        /// <param name="alias">The alias name used in the template.</param>
        /// <param name="value">The values to be rendered in the template.</param>
        public TemplateContext(string alias, object value)
            : this()
        {
            this.SetValue(alias, value);
        }

        /// <summary>
        /// Gets the values to rendered in the template.
        /// Key:   The alias name used in the template.
        /// Value: The value to be rendered in the template.
        /// </summary>
        public Dictionary<string, object> Values
        {
            get;
        }

        /// <summary>
        /// Sets a value used by the template.
        /// </summary>
        /// <param name="alias">The alias name used in the template.</param>
        /// <param name="value">The values to be rendered in the template.</param>
        public void SetValue(string alias, object value)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentException("The value cannot be null or empty.", nameof(alias));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Values.Add(alias, value);
        }
    }
}
