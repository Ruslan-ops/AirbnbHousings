using Airbnb.Application;
using Airbnb.Application.Services;
using AirbnbHousings.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Airbnb.Application.Common.Mappings;
using Minio;
using System.Reflection;
using Airbnb.Application.Common.Options;
using Airbnb.Application.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using AirbnbHousings.Middlewares;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

try
{
    var builder = WebApplication.CreateBuilder(args);
    IdentityModelEventSource.ShowPII = true;
    var Configuration = builder.Configuration;

    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IJwtService).Assembly));
    });
    builder.Services.AddApplication();

    builder.Services.AddCors(options =>
    {
       options.AddPolicy("CorsPolicy", builder =>
       {
           builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
       });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddScoped(CreateClient);
    builder.Services.AddScoped<IS3Storage, MinioService>();
    builder.Services.AddDbContext<AirbnbContext>(options =>
    {
        var val = Configuration.GetValue<string>("PostgresConnectionString");
        options.UseNpgsql(val);

    });
    builder.Services.AddScoped<DatabaseService>();

    

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            var jwtOpts = Configuration
                .GetSection("Jwt")
                .Get<JwtOptions>()!;
            Console.WriteLine($"jwtOpts: {JsonSerializer.Serialize(jwtOpts)}");
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOpts.Issuer,
                ValidAudience = jwtOpts.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOpts.SigningSecretKey)),
                TokenDecryptionKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOpts.EncryptionSecretKey)),
                ClockSkew = TimeSpan.FromMinutes(0),
            };
        });

    builder.Services.Configure<MinioOptions>(Configuration.GetSection("Minio"));
    builder.Services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));

    var app = builder.Build();


    app.UseCustomExceptionHandler();
    //app.UseHttpsRedirection();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseRouting();

    app.UseCors("CorsPolicy");


    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

static MinioClient CreateClient(IServiceProvider provider)
{
    var options = provider.GetRequiredService<IOptions<MinioOptions>>().Value;
    var x = new MinioClient().WithEndpoint(options.Endpoint).WithCredentials(options.AccessKey, options.SecretKey).Build();
    return x;
}