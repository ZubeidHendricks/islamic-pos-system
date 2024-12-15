using System;
using System.Runtime.Serialization;

namespace IslamicPOS.Domain.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public required string ErrorCode { get; init; }

    public DomainException() : base()
    {
        ErrorCode = "DOMAIN_ERROR";
    }

    public DomainException(string message) : base(message)
    {
        ErrorCode = "DOMAIN_ERROR";
    }

    public DomainException(string message, Exception innerException) 
        : base(message, innerException)
    {
        ErrorCode = "DOMAIN_ERROR";
    }

    public DomainException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    [Obsolete("This API supports obsolete formatter-based serialization. See https://aka.ms/dotnet-warnings/SYSLIB0051")]
    protected DomainException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
        ErrorCode = info.GetString(nameof(ErrorCode)) ?? "DOMAIN_ERROR";
    }

    [Obsolete("This API supports obsolete formatter-based serialization. See https://aka.ms/dotnet-warnings/SYSLIB0051")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ErrorCode), ErrorCode);
    }
}
