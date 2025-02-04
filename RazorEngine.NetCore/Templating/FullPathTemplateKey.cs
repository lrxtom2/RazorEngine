﻿using System;
using System.IO;

namespace RazorEngine.Templating
{
    /// <summary>
    /// A simple <see cref="ITemplateKey"/> implementation inheriting from <see cref="BaseTemplateKey"/>.
    /// This implementation assumes that the template-names are unique and returns the name as unique key.
    /// (So this implementation is used by <see cref="DelegateTemplateManager"/> and <see cref="RazorEngine.Configuration.Xml.WrapperTemplateManager"/>.
    /// </summary>
    [Serializable]
    public class FullPathTemplateKey : BaseTemplateKey
    {
        private readonly string fullPath;

        private static readonly bool isUnix =
            Environment.OSVersion.Platform == PlatformID.Unix ||
            Environment.OSVersion.Platform == PlatformID.MacOSX;

        private static string NormalizePath(string p)
        {
            var full = Path.GetFullPath(p).TrimEnd(Path.AltDirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar);
            if (isUnix)
            {
                return full;
            }
            else
            {
                return full.ToLowerInvariant();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameOnlyTemplateKey"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullPath"></param>
        /// <param name="resolveType"></param>
        /// <param name="context"></param>
        public FullPathTemplateKey(string name, string fullPath, ResolveType resolveType, ITemplateKey context)
            : base(name, resolveType, context)
        {
            this.fullPath = NormalizePath(fullPath);
        }

        /// <summary>
        ///
        /// </summary>
        public string FullPath
        {
            get { return fullPath; }
        }

        /// <summary>
        /// Returns the name.
        /// </summary>
        /// <returns></returns>
        public override string GetUniqueKeyString()
        {
            return this.FullPath;
        }

        /// <summary>
        /// Checks if the names are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as FullPathTemplateKey;
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            return other.FullPath == FullPath;
        }

        /// <summary>
        /// Returns a hashcode for the current instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.FullPath.GetHashCode();
        }
    }
}