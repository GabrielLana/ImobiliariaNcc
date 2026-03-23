using System.Net;

namespace ImobiliariaNcc.Domain.Exceptions;

public sealed class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message, (int)HttpStatusCode.Conflict)
    {
    }
}