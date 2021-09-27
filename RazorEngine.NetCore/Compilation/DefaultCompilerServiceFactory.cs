namespace RazorEngine.Compilation
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using System.Security;

    /// <summary>
    /// Provides a default implementation of a <see cref="ICompilerServiceFactory"/>.
    /// </summary>
    [Serializable]
    public class DefaultCompilerServiceFactory : ICompilerServiceFactory
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
            return new Roslyn.RoslynCompilerServiceFactory().CreateCompilerService(language);
        }

        #endregion Methods
    }
}