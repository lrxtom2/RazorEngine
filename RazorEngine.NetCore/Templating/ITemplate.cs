﻿using System.IO;
using System.Threading.Tasks;

namespace RazorEngine.Templating
{
    /// <summary>
    /// Defines the required contract for implementing a template.
    /// </summary>
    public interface ITemplate
    {
        #region Properties

        /// <summary>
        /// Sets the internal template service.
        /// </summary>
        IInternalTemplateService InternalTemplateService { set; }

        /// <summary>
        /// Sets the cached template service.
        /// </summary>
        IRazorEngineService Razor { set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Set the model of the template (if applicable).
        /// </summary>
        /// <param name="model"></param>
        /// <param name="viewbag"></param>
        void SetData(object model, DynamicViewBag viewbag);

        /// <summary>
        /// Executes the compiled template.
        /// </summary>
        Task ExecuteAsync();

        /// <summary>
        /// Runs the template and returns the result.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <param name="writer"></param>
        /// <returns>The merged result of the template.</returns>
        Task Run(ExecuteContext context, TextWriter writer);

        /// <summary>
        /// Writes the specified object to the result.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(object value);

        /// <summary>
        /// Writes the specified string to the result.
        /// </summary>
        /// <param name="literal">The literal to write.</param>
        void WriteLiteral(string literal);

        #endregion Methods
    }
}