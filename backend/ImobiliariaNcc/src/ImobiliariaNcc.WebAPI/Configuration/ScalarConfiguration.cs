using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace ImobiliariaNcc.WebAPI.Configuration;

public static class ScalarConfiguration
{
    public static IServiceCollection AddScalarConfigurations(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();

                document.Components.SecuritySchemes ??=
                    new Dictionary<string, OpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                var metadata = context.Description.ActionDescriptor.EndpointMetadata;

                var hasAuthorize = metadata.OfType<AuthorizeAttribute>().Any();
                var hasAllowAnonymous = metadata.OfType<AllowAnonymousAttribute>().Any();

                if (!hasAuthorize || hasAllowAnonymous)
                    return Task.CompletedTask;

                operation.Security ??= new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

                return Task.CompletedTask;
            });
        });

        return services;
    }
}
