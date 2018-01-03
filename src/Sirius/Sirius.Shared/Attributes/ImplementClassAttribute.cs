using System;

namespace Sirius.Shared.Attributes
{
    /// <summary>
    ///     Special marker attribute. Can be used to mark classes for automatic registration in DI-container.
    ///     Classes with the attribute will be registered as a service for an interface from property <see cref="Type"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ImplementClassAttribute : Attribute
    {
        /// <summary>
        ///     Type (of class or interface) which represents class as a service.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="type">
        ///      Type (of class or interface) which represents class as a service.
        /// </param>
        public ImplementClassAttribute(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            Type = type;
        }
    }
}
