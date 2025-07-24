using IndustrialCampusAPI.Data;
using IndustrialCampusAPI.Mappings;
using IndustrialCampusAPI.Repositories;
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<FluentValidation.IValidator<IndustrialCampusAPI.DTOs.KullaniciLoginDTO>, IndustrialCampusAPI.Validators.KullaniciLoginDTOValidator>();
builder.Services.AddScoped<FluentValidation.IValidator<IndustrialCampusAPI.DTOs.KullaniciRegisterDTO>, IndustrialCampusAPI.Validators.KullaniciRegisterDTOValidator>();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Repositories
builder.Services.AddScoped<IFirmaRepository, FirmaRepository>();
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IZiyaretRepository, ZiyaretRepository>();
builder.Services.AddScoped<IGelirGiderRepository, GelirGiderRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFirmaService, FirmaService>();
builder.Services.AddScoped<IZiyaretService, ZiyaretService>();
builder.Services.AddScoped<IGelirGiderService, GelirGiderService>();
builder.Services.AddScoped<IRaporService, RaporService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var key = Encoding.ASCII.GetBytes(jwtKey!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sanayi Kampüsü Dijital Yönetim Sistemi API", 
        Version = "v1",
        Description = "ASP.NET Core Web API for Industrial Campus Digital Management System"
    });

    // JWT Authentication için Swagger konfigürasyonu
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", builder =>
        builder.WithOrigins(
            "https://your-frontend-domain.com" // Buraya izin verilen domain(ler) eklenmeli
        )
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger her ortamda aktif
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Industrial Campus API v1");
        c.RoutePrefix = string.Empty; // Swagger UI'ı root'ta açar
    });

// Database Migration
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var licenseKey = builder.Configuration["License:Key"];
if (licenseKey != "1986SEC-2024-ULTRA")
{
    throw new Exception("Lisans anahtarı geçersiz! Uygulama başlatılamaz. Lütfen 1986sec ile iletişime geçin.");
}

Log.Information("Sanayi Kampüsü Dijital Yönetim Sistemi API başlatıldı");

app.Run();
// powered by 1986sec

#if DEBUG
public static class LicenseKeyGenerator
{
    public static string GenerateKey(int length = 20)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return string.Join("-", 
            Enumerable.Range(0, length / 5)
                .Select(_ => new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray()))
        );
    }
}
#endif