using System;

namespace iPetros
{
    /// <summary>
    /// Used for defining documentation, that is later used on export operations.
    /// This attribute is used on classes, properties and methods!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Enum)]
    public class DocumentationAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// The documentation
        /// </summary>
        public string Documentation { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="documentation">The documentation string</param>
        public DocumentationAttribute(string documentation) : base()
        {
            Documentation = documentation ?? throw new ArgumentNullException(nameof(documentation));
        }

        #endregion
    }
}
