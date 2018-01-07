using System;
using System.Runtime.Serialization;
using Sirius.DictionaryApi.Enums;
using Sirius.Shared;

namespace Sirius.DictionaryApi.Exceptions
{
    /// <summary>
    /// Dictionary api exception (throwed while accessing api)
    /// </summary>
    public class SiriusDictApiException: SiriusException
    {
        public DictApiErrorCode? StatusCode { get; set; }

        public SiriusDictApiException()
        {
        }

        public SiriusDictApiException(string message) : base(message)
        {
        }

        public SiriusDictApiException(string message, DictApiErrorCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public SiriusDictApiException(string message, Exception inner, DictApiErrorCode statusCode) : base(message, inner)
        {
            StatusCode = statusCode;
        }

        protected SiriusDictApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
