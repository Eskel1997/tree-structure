using System;

namespace TREE.DB.Exceptions
{
    public class TreeException: Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public TreeException(ErrorCode errorCode)
            : this(errorCode, string.Empty) { }

        public TreeException(ErrorCode errorCode, string message) : base(message, null)
        {
            ErrorCode = errorCode;
        }
    }
}
