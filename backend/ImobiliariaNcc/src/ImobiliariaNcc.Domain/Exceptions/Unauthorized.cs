using System.Net;

namespace ImobiliariaNcc.Domain.Exceptions;

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message)
        : base(message, (int)HttpStatusCode.Unauthorized)
    {
    }
}