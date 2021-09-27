﻿using System;

namespace RazorEngine.Templating
{
    /// <summary>
    /// A base implementation for <see cref="T:RazorEngine.Templating.ITemplateKey"/>.
    /// You only need to provide the <see cref="ITemplateKey.GetUniqueKeyString()"/>
    /// implementation which depends on the <see cref="ITemplateManager"/> implementation.
    /// </summary>
    [Serializable]
    public abstract class BaseTemplateKey : ITemplateKey
    {
        /// <summary>
        /// See <see cref="ITemplateKey.Name"/>.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// See <see cref="ITemplateKey.TemplateType"/>.
        /// </summary>
        private readonly ResolveType _resolveType;

        /// <summary>
        /// See <see cref="ITemplateKey.Context"/>.
        /// </summary>
        private readonly ITemplateKey _context;

        /// <summary>
        /// Create a new <see cref="BaseTemplateKey"/> instance.
        /// </summary>
        /// <param name="name">See <see cref="ITemplateKey.Name"/></param>
        /// <param name="resolveType">See <see cref="ITemplateKey.TemplateType"/></param>
        /// <param name="context">See <see cref="ITemplateKey.Context"/></param>
        public BaseTemplateKey(string name, ResolveType resolveType, ITemplateKey context)
        {
            _name = name;
            _resolveType = resolveType;
            _context = context;
        }

        /// <summary>
        /// See <see cref="ITemplateKey.Name"/>.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// See <see cref="ITemplateKey.TemplateType"/>.
        /// </summary>
        public ResolveType TemplateType
        {
            get { return _resolveType; }
        }

        /// <summary>
        /// See <see cref="ITemplateKey.Context"/>.
        /// </summary>
        public ITemplateKey Context
        {
            get { return _context; }
        }

        /// <summary>
        /// See <see cref="ITemplateKey.GetUniqueKeyString()"/>.
        /// </summary>
        public abstract string GetUniqueKeyString();
    }
}