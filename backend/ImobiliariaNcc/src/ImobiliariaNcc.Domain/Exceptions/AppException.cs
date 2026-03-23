namespace ImobiliariaNcc.Domain.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; }

    protected AppException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}