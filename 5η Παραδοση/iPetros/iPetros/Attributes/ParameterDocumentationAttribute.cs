using System;

namespace iPetros
{
    /// <summary>
    /// Used for defining documentation, that is later used on export operations.
    /// This attribute is used on parameters!
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterDocumentationAttribute : DocumentationAttribute
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ParameterDocumentationAttribute(string documentation) : base(documentation)
        {
        }

        #endregion
    }
}
