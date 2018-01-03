using System;
using System.Runtime.Serialization;
using Sirius.Shared;

namespace Sirius.Logic.Exceptions
{
    [Serializable]
    public class SiriusLogicException: SiriusException
    {
        /// <summary>
        ///     Operation is not allowed for a user. The value is true, if the 
        ///     exception is not application failure, but requested BL operation 
        ///     which could not be processed.
        /// </summary>
        public bool OperationNotAllowed { get; set; }

        public SiriusLogicException(bool operationNotAllowed = false)
        {
            OperationNotAllowed = operationNotAllowed;
        }

        public SiriusLogicException(string message, bool operationNotAllowed = false)
            : base(message)
        {
            OperationNotAllowed = operationNotAllowed;
        }

        public SiriusLogicException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SiriusLogicException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
