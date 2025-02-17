using System;

namespace YOGBIS.Common.Exceptions
{
    public class YogbisException : Exception
    {
        public string ErrorCode { get; }
        public object[] Parameters { get; }

        public YogbisException(string message) : base(message)
        {
        }

        public YogbisException(string message, string errorCode, params object[] parameters) 
            : base(message)
        {
            ErrorCode = errorCode;
            Parameters = parameters;
        }
    }

    public class YogbisBusinessException : YogbisException
    {
        public YogbisBusinessException(string message) : base(message)
        {
        }
    }

    public class YogbisValidationException : YogbisException
    {
        public YogbisValidationException(string message) : base(message)
        {
        }
    }

    public class YogbisNotFoundException : YogbisException
    {
        public YogbisNotFoundException(string message) : base(message)
        {
        }
    }
}
