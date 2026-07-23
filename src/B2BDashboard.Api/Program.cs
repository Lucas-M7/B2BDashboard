using System.Text;
using B2BDashboard.Api.ErrorHandling;
using B2BDashboard.Application.Common;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Application.Services;
using B2BDashboard.Infrastructure.Persistence;
using B2BDashboard.Infrastructure.Repositories;
using B2BDashboard.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region Banco de dados
builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections")));
#endregion

#region Repositórios
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Segurança
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
#endregion

#region Casos de uso
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // "quem é você???" -> lê e valida o token
app.UseAuthorization(); // "rapaaaz, você pode fazer isso?" -> ([Authorize], Roles)

app.MapControllers();

app.Run();

public partial class Program { }