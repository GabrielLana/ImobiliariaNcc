using FluentValidation;
using ImobiliariaNcc.Application.Behaviors;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Auth.Validator;
using ImobiliariaNcc.Application.Modules.Clientes.Commands;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Auth;
using ImobiliariaNcc.Infrastructure.Context;
using ImobiliariaNcc.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ImobiliariaNcc.Infrastructure.Injection;

public static class DependencyInjection
{
    public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddAuthServices(configuration);
        services.AddDbRelated();

        return services;
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var provider = configuration["Provider"];
            var connectionString = configuration.GetConnectionString("ImobiliariaNcc");

            switch (provider)
            {
                case "SqlServer":
                    options.UseSqlServer(connectionString);
                    break;
                default:
                    throw new BadRequestException("Provedor de Banco de Dados não suportado.");
            }
        });
    }

    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateClienteHandler).Assembly));

        services.AddValidatorsFromAssembly(
            typeof(LoginValidator).Assembly);

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
    }

    public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
        });
        services.AddScoped<IPasswordHasher<VendedorModel>, PasswordHasher<VendedorModel>>();
    }

    public static void AddDbRelated(this IServiceCollection services)
    {
        services.AddScoped<IApartamentosRepository, ApartamentosRepository>();
        services.AddScoped<IClientesRepository, ClientesRepository>();
        services.AddScoped<IReservasRepository, ReservasRepository>();
        services.AddScoped<IVendasRepository, VendasRepository>();
        services.AddScoped<IVendedoresRepository, VendedoresRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
