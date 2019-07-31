using System;
using System.Runtime.Serialization;

namespace Aafp.Payments.Api.Filters
{
    public class RegistrationProcessingException : ApplicationException
    {
        public RegistrationProcessingException()
        {
        }

        public RegistrationProcessingException(string message) : base(message)
        {
        }

        public RegistrationProcessingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RegistrationProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}