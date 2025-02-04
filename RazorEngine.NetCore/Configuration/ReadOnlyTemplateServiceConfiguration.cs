﻿using Microsoft.AspNetCore.Razor.Language;
using RazorEngine.Compilation;
using RazorEngine.Compilation.ReferenceResolver;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;

namespace RazorEngine.Configuration
{
    /// <summary>
    /// Provides a readonly view of a configuration, and safe-copies all references.
    /// </summary>
    public class ReadOnlyTemplateServiceConfiguration : ITemplateServiceConfiguration
    {
        private readonly IActivator _activator;
        private readonly bool _allowMissingPropertiesOnDynamic;
        private readonly Type _baseTemplateType;
        private readonly ICachingProvider _cachingProvider;
        private readonly ICompilerServiceFactory _compilerServiceFactory;
        private readonly bool _debug;
        private readonly bool _disableTempFileLocking;
        private readonly IEncodedStringFactory _encodedStringFactory;
        private readonly Language _language;
        private readonly ISet<string> _namespaces;
        private readonly IReferenceResolver _referenceResolver;
        private readonly ITemplateManager _templateManager;

        /// <summary>
        /// Create a new readonly view (and copy) of the given configuration.
        /// </summary>
        /// <param name="config">the configuration to copy.</param>
        public ReadOnlyTemplateServiceConfiguration(ITemplateServiceConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            _allowMissingPropertiesOnDynamic = config.AllowMissingPropertiesOnDynamic;

            _activator = config.Activator;
            if (_activator == null)
            {
                throw new ArgumentNullException("config", "the configured Activator cannot be null!");
            }

            _baseTemplateType = config.BaseTemplateType;
            // Check if the baseTemplateType is valid.
            if (_baseTemplateType != null && (!typeof(ITemplate).IsAssignableFrom(_baseTemplateType) || typeof(ITemplate) == _baseTemplateType))
            {
                throw new ArgumentOutOfRangeException("config", "the configured BaseTemplateType must implement ITemplate!");
            }

            _cachingProvider = config.CachingProvider;
            if (_cachingProvider == null)
            {
                throw new ArgumentNullException("config", "the configured CachingProvider cannot be null!");
            }

            _compilerServiceFactory = config.CompilerServiceFactory;
            if (_compilerServiceFactory == null)
            {
                throw new ArgumentNullException("config", "the configured CompilerServiceFactory cannot be null!");
            }

            _debug = config.Debug;
            _disableTempFileLocking = config.DisableTempFileLocking;
            _encodedStringFactory = config.EncodedStringFactory;
            if (_encodedStringFactory == null)
            {
                throw new ArgumentNullException("config", "the configured EncodedStringFactory cannot be null!");
            }

            _language = config.Language;
            _namespaces = config.Namespaces;
            if (_namespaces == null)
            {
                throw new ArgumentNullException("config", "the configured Namespaces cannot be null!");
            }
            _namespaces = new HashSet<string>(_namespaces);

            _referenceResolver = config.ReferenceResolver;
            if (_referenceResolver == null)
            {
                throw new ArgumentNullException("config", "the configured ReferenceResolver cannot be null!");
            }

            _templateManager = config.TemplateManager;

#if NO_CONFIGURATION
            if (_templateManager == null)
            {
                throw new ArgumentNullException("config", "the configured TemplateManager cannot be null!");
            }
#else
#pragma warning disable 0618 // Backwards Compat.
            _resolver = config.Resolver;
            if (_templateManager == null)
            {
                if (_resolver != null)
                {
                    _templateManager = new Xml.WrapperTemplateManager(_resolver);
                }
                else
                {
                    throw new ArgumentNullException("config", "the configured TemplateManager and Resolver cannot be null!");
                }
            }
#pragma warning restore 0618 // Backwards Compat.
#endif
            ConfigureCompilerBuilder = config.ConfigureCompilerBuilder;
        }

        /// <summary>
        /// Gets the activator.
        /// </summary>
        public IActivator Activator
        {
            get
            {
                return _activator;
            }
        }

        /// <summary>
        /// Gets or sets whether to allow missing properties on dynamic models.
        /// </summary>
        public bool AllowMissingPropertiesOnDynamic
        {
            get
            {
                return _allowMissingPropertiesOnDynamic;
            }
        }

        /// <summary>
        /// Gets the base template type.
        /// </summary>
        public Type BaseTemplateType
        {
            get
            {
                return _baseTemplateType;
            }
        }

        /// <summary>
        /// Gets the caching provider.
        /// </summary>
        public ICachingProvider CachingProvider
        {
            get
            {
                return _cachingProvider;
            }
        }

        /// <summary>
        /// Gets the compiler service factory.
        /// </summary>
        public ICompilerServiceFactory CompilerServiceFactory
        {
            get
            {
                return _compilerServiceFactory;
            }
        }

        /// <summary>
        /// Gets whether the template service is operating in debug mode.
        /// </summary>
        public bool Debug
        {
            get
            {
                return _debug;
            }
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
            get
            {
                return _disableTempFileLocking;
            }
        }

        /// <summary>
        /// Gets the encoded string factory.
        /// </summary>
        public IEncodedStringFactory EncodedStringFactory
        {
            get
            {
                return _encodedStringFactory;
            }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        public Language Language
        {
            get
            {
                return _language;
            }
        }

        /// <summary>
        /// Gets the namespaces.
        /// </summary>
        public ISet<string> Namespaces
        {
            get
            {
                return _namespaces;
            }
        }

        /// <summary>
        /// Gets the reference resolver.
        /// </summary>
        public IReferenceResolver ReferenceResolver
        {
            get
            {
                return _referenceResolver;
            }
        }

        /// <summary>
        /// Gets the template resolver.
        /// </summary>
        public ITemplateManager TemplateManager
        {
            get
            {
                return _templateManager;
            }
        }

#pragma warning disable CS0618 // Type or member is obsolete

        /// <summary>
        /// Callback to register custom Model directives or configure the razor engine builder in another form.
        /// </summary>
        /// <value>
        /// An callback that receives the builder
        /// </value>
        public Action<IRazorEngineBuilder> ConfigureCompilerBuilder { get; }

#pragma warning restore CS0618 // Type or member is obsolete
    }
}