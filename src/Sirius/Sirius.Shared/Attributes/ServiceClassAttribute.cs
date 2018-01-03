using System;

namespace Sirius.Shared.Attributes
{
    /// <summary>
    ///     Special marker attribute. Can be used to mark classes for automatic registration in DI-container.
    ///     Classes with the attribute will be registered as a service for all implemented interfaces.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServiceClassAttribute : Attribute
    {
    }
}
