using System.Net;

namespace ImobiliariaNcc.Domain.Exceptions;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message, (int)HttpStatusCode.BadRequest)
    {
    }
}