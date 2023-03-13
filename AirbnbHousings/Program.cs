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

static MinioClient CreateClient(IServiceProvider provider)
{
    var endpoint = "localhost:9000";
    var accessKey = "ROOTUSER";
    var secretKey = "CHANGEME123";
    var x = new MinioClient().WithEndpoint(endpoint).WithCredentials(accessKey, secretKey).Build();
    return x;
}

try
{


    var builder = WebApplication.CreateBuilder(args);
    IdentityModelEventSource.ShowPII = true;
    //Add services to the container.
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
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddScoped(CreateClient);
    builder.Services.AddScoped<MinioService>();
    builder.Services.AddDbContext<AirbnbContext>(options =>
        options.UseNpgsql("Host=localhost;Port=5477;Database=airbnb;Username=postgres;Password=postgres"));
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
    //builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

    var app = builder.Build();


    app.UseCustomExceptionHandler();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");

    //app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
