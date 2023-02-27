using AirbnbHousings.Models;
using AirbnbHousings.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minio;

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

    //Add services to the container.

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
        options.UseNpgsql("Host=localhost;Port=5466;Database=airbnb;Username=postgres;Password=postgres"));
    builder.Services.AddScoped<PostgresService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");

    //app.UseHttpsRedirection();

    //app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
