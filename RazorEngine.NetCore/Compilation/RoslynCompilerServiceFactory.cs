﻿using RazorEngine.Compilation;
using RazorEngine.Roslyn.CSharp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace RazorEngine.Roslyn
{
    /// <summary>
    /// Provides a implementation of <see cref="ICompilerServiceFactory"/> for the Roslyn implementation.
    /// </summary>
    [Serializable]
    public class RoslynCompilerServiceFactory : ICompilerServiceFactory
    {
        #region Methods

        /// <summary>
        /// Creates a <see cref="ICompilerService"/> that supports the specified language.
        /// </summary>
        /// <param name="language">The <see cref="Language"/>.</param>
        /// <returns>An instance of <see cref="ICompilerService"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [SecuritySafeCritical]
        public ICompilerService CreateCompilerService(Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    return new CSharpRoslynCompilerService();

                case Language.VisualBasic:
                    throw new NotSupportedException("Razor4 doesn't support VB.net apparently.");
                default:
                    throw new ArgumentException("Unsupported language: " + language);
            }
        }

        #endregion Methods
    }
}