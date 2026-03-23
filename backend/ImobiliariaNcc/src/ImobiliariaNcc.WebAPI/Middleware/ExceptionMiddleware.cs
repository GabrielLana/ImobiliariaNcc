using ImobiliariaNcc.Domain.Exceptions;
using System;

namespace ImobiliariaNcc.WebAPI.Middleware;

public class ExceptionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await HandleAppException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleUnknownException(context, ex);
        }
    }

    private static async Task HandleAppException(HttpContext context, AppException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;

        if (ex is ValidationAppException validationException)
        {
            var response = new
            {
                message = validationException.Message,
                errors = validationException.Errors
            };

            context.Response.StatusCode = validationException.StatusCode;

            await context.Response.WriteAsJsonAsync(response);

            return;
        } else
        {
            var response = new
            {
                error = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private static async Task HandleUnknownException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var response = new
        {
            error = "Erro interno no servidor"
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}