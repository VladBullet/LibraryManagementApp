
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
/// <summary>
/// Default options used to configure Swagger during service startup.
/// </summary>
public static class SwaggerConfig
{
    /// <summary>
    /// Security scheme using JWT bearer.
    /// </summary>
    public static readonly OpenApiSecurityScheme JwtBearerSecurityScheme = new()
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    };

    /// <summary>
    /// Security requirement for JWT bearer.
    /// </summary>
    public static readonly OpenApiSecurityRequirement JwtBearerSecurityRequirement = new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme, // The name of the previously defined security scheme.
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    };
}