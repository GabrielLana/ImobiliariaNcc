using FluentValidation.Results;
using ImobiliariaNcc.Domain.Exceptions;
using System.Net;

public class ValidationAppException : AppException
{
    public IEnumerable<string> Errors { get; }

    public ValidationAppException(IEnumerable<ValidationFailure> failures)
        : base("Erro de validação", (int)HttpStatusCode.BadRequest)
    {
        Errors = failures.Select(x => x.ErrorMessage);
    }
}