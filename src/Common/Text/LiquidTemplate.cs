// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace Common.Text
{
    using System;
    using System.Collections.Generic;
    using Fluid;

    /// <summary>
    /// Renders liquid templates.
    /// </summary>
    /// <seealso cref="Common.Text.ITemplate" />
    public class LiquidTemplate : ITemplate
    {
        /// <summary>
        /// Cached liquid template.
        /// </summary>
        private FluidTemplate fluidTemplate = null;

        /// <summary>
        /// The types registered with the templates.
        /// </summary>
        private List<Type> registeredTypes = new List<Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidTemplate"/> class.
        /// </summary>
        public LiquidTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidTemplate"/> class.
        /// </summary>
        /// <param name="template">The template to parse render.</param>
        public LiquidTemplate(string template)
        {
            this.Parse(template);
        }

        /// <summary>
        /// Gets the parsed template to render.
        /// </summary>
        public string Template
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the types registered with the template.
        /// </summary>
        public IReadOnlyCollection<Type> RegisteredTypes
        {
            get
            {
                return this.registeredTypes;
            }
        }

        /// <summary>
        /// Parses the specified template to render.
        /// </summary>
        /// <param name="template">The template to parse and render.</param>
        public void Parse(string template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            this.Template = template;
            this.fluidTemplate = FluidTemplate.Parse(this.Template);
        }

        /// <summary>
        /// Registers a type used by the template.
        /// </summary>
        /// <param name="type">The type to register with the template.</param>
        public void RegisterType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // TODO: Use reflection to automatically register the types referenced by the top level type.
            this.registeredTypes.Add(type);
        }

        /// <summary>
        /// Renders the template.
        /// </summary>
        /// <param name="context">The values and parameters to render the template with.</param>
        /// <returns>The rendered template.</returns>
        public string Render(TemplateContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (this.fluidTemplate == null)
            {
                throw new InvalidOperationException("The template has not been parsed.");
            }

            Fluid.TemplateContext fluidContext = new Fluid.TemplateContext();
            foreach (Type type in this.registeredTypes)
            {
                fluidContext.MemberAccessStrategy.Register(type);
            }

            foreach (KeyValuePair<string, object> value in context.Values)
            {
                fluidContext.SetValue(value.Key, value.Value);
            }

            return this.fluidTemplate.Render(fluidContext);
        }
    }
}
