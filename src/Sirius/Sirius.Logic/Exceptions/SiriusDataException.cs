using System;
using System.Runtime.Serialization;
using Sirius.Shared;

namespace Sirius.Logic.Exceptions
{
    /// <summary>
    ///     Exception which is thrown due to data errors. Main error types:
    ///     <list type="bullet">
    ///         <item><description>
    ///            attempt to extract and to work with missing or deleted data;  </description></item>
    ///         <item><description>
    ///             attempt to communicate with missing or deleted data; </description></item>
    ///         <item><description>
    ///             attempt that may lead to violation of data restrictions or data breach. </description></item>
    ///     </list>
    /// </summary>
    [Serializable]
    public class SiriusDataException : SiriusException
    {
        private string _entityName;

        /// <summary>
        ///     Entity type.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        ///     Name of an entity or a constraint.
        /// </summary>
        public string EntityName
        {
            get => _entityName ?? EntityType?.Name;
            set => _entityName = value;
        }

        /// <summary>
        ///     Entity identifier.
        /// </summary>
        public long? EntityId { get; set; }

        /// <summary>
        ///     Create an instance of the exception with exception mesaage.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public SiriusDataException(string message)
            : base(message)
        { }

        /// <summary>
        ///     Create an instance of the exception with exception message and 
        ///     entity type which led to the exception
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="entityType">Entity type.</param>
        public SiriusDataException(string message, Type entityType)
            : base(MakeMessage(message, entityType, null, null))
        {
            this.EntityType = entityType;
        }

        /// <summary>
        ///     Create an instance of the exception with exception message, 
        ///     entity type which led to the exception and entity identifier.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="entityType">Entity type.</param>
        /// <param name="entityId">Entity identifier.</param>
        public SiriusDataException(string message, Type entityType, long? entityId)
            : base(MakeMessage(message, entityType, null, entityId))
        {
            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        /// <summary>
        ///     Create an instance of the exception with exception message and 
        ///     name of the entity or DB object which led to the exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="entityName">Name of an entity or a constraint.</param>
        public SiriusDataException(string message, string entityName)
            : base(MakeMessage(message, null, entityName, null))
        {
            this.EntityName = entityName;
        }

        /// <summary>
        ///     Create an instance of the exception with exception message, 
        ///     name of the entity or DB object which led to the exception
        ///     and entity name.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="entityName">Name of an entity or a constraint.</param>
        /// <param name="entityId">Entity identifier.</param>
        public SiriusDataException(string message, string entityName, long? entityId)
            : base(MakeMessage(message, null, entityName, entityId))
        {
            this.EntityName = entityName;
            this.EntityId = entityId;
        }

        /// <summary>
        ///     Create an instance of the exception with exception message and
        ///     inner exception that contains detailed information about the exception.
        /// </summary>
        /// <param name="message">
        ///     Exception message. </param>
        /// <param name="inner">
        ///     Inner exception - source of the error. </param>
        public SiriusDataException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        ///     Created automatically.
        /// </summary>
        protected SiriusDataException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        ///     Create detailed exception message.
        /// </summary>
        private static string MakeMessage(string message, Type entityType, string entityName, long? entityId)
        {
            message = message.Trim();
            var endPointedMessage = string.IsNullOrEmpty(message)
                ? ""
                : message.EndsWith(".") ? message + " " : message + ". ";
            var extraInfo = string.IsNullOrEmpty(entityName)
                ? entityType == null
                    ? null
                    : entityId == null
                        ? $"Entity type {entityType.Name}."
                        : $"Entity type {entityType.Name}, Id #{entityId}."
                : entityId == null
                    ? $"Entity \"{entityType.Name}\"."
                    : $"Entity \"{entityType.Name}\", Id #{entityId}.";
            return endPointedMessage + extraInfo;
        }
    }
}
