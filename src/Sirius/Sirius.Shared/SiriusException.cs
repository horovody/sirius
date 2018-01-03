using System;
using System.Runtime.Serialization;

namespace Sirius.Shared
{
    /// <summary>
    /// Base class for all project exceptions
    /// </summary>
    [Serializable]
    public class SiriusException : Exception
    {
        public SiriusException()
        {
        }

        public SiriusException(string message) : base(message)
        {
        }

        public SiriusException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SiriusException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
