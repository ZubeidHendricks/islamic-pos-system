using System.Runtime.Serialization;

namespace IslamicPOS.Domain.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public string ErrorCode { get; } = string.Empty;

    public DomainException() : base() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException) 
        : base(message, innerException) { }

    public DomainException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    [Obsolete("This constructor is obsolete due to formatter-based serialization being deprecated.")]
    protected DomainException(SerializationInfo info, StreamingContext context) 
        : base(info, context) 
    {
        ErrorCode = info.GetString(nameof(ErrorCode)) ?? string.Empty;
    }

    [Obsolete("This method is obsolete due to formatter-based serialization being deprecated.")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ErrorCode), ErrorCode);
    }
}