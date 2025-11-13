using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Api.Domain.Helpers;


public static class SwaggerOptionsBuilder
{
    public static void BuildDocInfo(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "1.0.0",
            Title = ".NET REST API",
            Description = "API REST com um CRUD simples para manipulação de autenticação de usuários",
            Contact = new OpenApiContact
            {
                Email = "flpssdocarmo@gmail.com",
                Name = "Felipe S. Carmo",
                Url = new Uri("https://github.com/SousaFelipe")
            }
        });
    }


    public static void BuildSecurityDefinitions(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT neste formato: Bearer eyJhbGci..."
        });
    }


    public static void BuildSecurityRequirement(SwaggerGenOptions options)
    {
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
        });
    } 
}
