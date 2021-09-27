﻿namespace RazorEngine.Configuration
{
    using Compilation;
    using Microsoft.AspNetCore.Razor.Language;
    using RazorEngine.Compilation.ReferenceResolver;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Templating;
    using Text;

    /// <summary>
    /// Defines a fluent template service configuration
    /// </summary>
    public class FluentTemplateServiceConfiguration : ITemplateServiceConfiguration
    {
        #region Fields

        private readonly TemplateServiceConfiguration _innerConfig = new TemplateServiceConfiguration();

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initialises a new instance of <see cref="FluentTemplateServiceConfiguration"/>.
        /// </summary>
        /// <param name="config">The delegate used to create the configuration.</param>
        public FluentTemplateServiceConfiguration(Action<IConfigurationBuilder> config)
        {
            Contract.Requires(config != null);

            config(new FluentConfigurationBuilder(_innerConfig));
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the activator.
        /// </summary>
        public IActivator Activator
        {
            get { return _innerConfig.Activator; }
        }

        /// <summary>
        /// Gets or sets whether to allow missing properties on dynamic models.
        /// </summary>
        public bool AllowMissingPropertiesOnDynamic
        {
            get { return _innerConfig.AllowMissingPropertiesOnDynamic; }
        }

        /// <summary>
        /// Loads all dynamic assemblies with Assembly.Load(byte[]).
        /// This prevents temp files from being locked (which makes it impossible for RazorEngine to delete them).
        /// At the same time this completely shuts down any sandboxing/security.
        /// Use this only if you have a limited amount of static templates (no modifications on rumtime),
        /// which you fully trust and when a seperate AppDomain is no solution for you!.
        /// This option will also hurt debugging.
        ///
        /// OK, YOU HAVE BEEN WARNED.
        /// </summary>
        public bool DisableTempFileLocking
        {
            get { return _innerConfig.DisableTempFileLocking; }
        }

        /// <summary>
        /// Gets the base template type.
        /// </summary>
        public Type BaseTemplateType
        {
            get { return _innerConfig.BaseTemplateType; }
        }

        /// <summary>
        /// Gets the reference resolver.
        /// </summary>
        public IReferenceResolver ReferenceResolver
        {
            get { return _innerConfig.ReferenceResolver; }
        }

        /// <summary>
        /// Gets the caching provider.
        /// </summary>
        public ICachingProvider CachingProvider
        {
            get { return _innerConfig.CachingProvider; }
        }

        /// <summary>
        /// Gets or sets the compiler service factory.
        /// </summary>
        public ICompilerServiceFactory CompilerServiceFactory
        {
            get { return _innerConfig.CompilerServiceFactory; }
        }

        /// <summary>
        /// Gets whether the template service is operating in debug mode.
        /// </summary>
        public bool Debug
        {
            get { return _innerConfig.Debug; }
        }

        /// <summary>
        /// Gets or sets the encoded string factory.
        /// </summary>
        public IEncodedStringFactory EncodedStringFactory
        {
            get { return _innerConfig.EncodedStringFactory; }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public Language Language
        {
            get { return _innerConfig.Language; }
        }

        /// <summary>
        /// Gets or sets the collection of namespaces.
        /// </summary>
        public ISet<string> Namespaces
        {
            get { return _innerConfig.Namespaces; }
        }

        /// <summary>
        /// Gets the template manager.
        /// </summary>
        public ITemplateManager TemplateManager
        {
            get { return _innerConfig.TemplateManager; }
        }

#pragma warning disable CS0618 // Type or member is obsolete

        /// <summary>
        /// Callback to register custom Model directives or configure the razor engine builder in another form.
        /// </summary>
        /// <value>
        /// An callback that receives the builder
        /// </value>
        public Action<IRazorEngineBuilder> ConfigureCompilerBuilder
#pragma warning restore CS0618 // Type or member is obsolete
        {
            get { return _innerConfig.ConfigureCompilerBuilder; }
        }

        #endregion Properties
    }
}