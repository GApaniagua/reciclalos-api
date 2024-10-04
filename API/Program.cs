using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Models;
using Application.ProfileMapping;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Interface;
using Application.CollectorCore;
using Domain.Collector;
using Application.CollectionCenterCore;
using Domain.CollectionCenter;
using Application.CatalogCore;
using Domain.Catalog;
using DotNetEnv;
using Application.AuthCore;
using Application.GameCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1.0.0",
            Title = "WWF API",
            Description = "API for WWF competition or similar.",
            Contact = new OpenApiContact
            {
                Name = "InSoft Guatemala",
                Email = "info@insoft.com.gt",
                Url = new Uri("https://www.insoft.com.gt")
            }
        }
    );
    // NOTE: Enable to add behavior to all operations show by Swagger (IOperationFilter). Ex:
    // c.OperationFilter<T>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {JWT}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    c.EnableAnnotations();
});
builder.Services.AddDbContext<ReciclalosDbContext>(option =>
{
    option.UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"), ServerVersion.Parse("5.7.37-mysql"));
});

builder.Services.AddAutoMapper(setup =>
{
    setup.AddProfile(new MapperProfile());
});
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null; // No redirigir a HTTPS
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});


// Configuración de servicios
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Acceder a la variable de entorno cargada por DotNetEnv
    var tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY");
    if (string.IsNullOrEmpty(tokenKey))
    {
        throw new InvalidOperationException("La variable de entorno TOKEN_KEY no está configurada.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidIssuer = tokenKey,
        ValidAudience = tokenKey
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("A"));
    // Política para el rol "U" (Collector)
    options.AddPolicy("UserOnly", policy => policy.RequireRole("U"));
    // Política para el rol "E" (Colletor Managment)
    options.AddPolicy("CollectorOnly", policy => policy.RequireRole("E"));
});


builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkContainer>();
builder.Services.AddScoped<ICollectorService, CollectorManager>();
builder.Services.AddScoped<ICollectionCenterService, CollectionCenterManager>();
builder.Services.AddScoped<ICatalogService, CatalogManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IGameService, GameManager>();

var app = builder.Build();

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
