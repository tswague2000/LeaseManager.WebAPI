using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.WebAPI.Application;
using LeaseManager.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

// Add logging
builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.AddDebug();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "LeaseManager API",
  Version = "v1",
        Description = "API REST complète pour la gestion des contrats de location",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support",
          Email = "support@leasemanager.com"
        }
    });

  var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
      options.IncludeXmlComments(xmlPath);
    }
});

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
      .AllowAnyMethod()
   .AllowAnyHeader();
    });

    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200", "https://localhost:3000", "https://localhost:4200")
       .AllowAnyMethod()
   .AllowAnyHeader()
       .AllowCredentials();
    });

    options.AddPolicy("AllowProduction", policy =>
    {
    policy.WithOrigins("https://yourdomain.com")
            .AllowAnyMethod()
   .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Add Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
options.ReportApiVersions = true;
});

// Add Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaseManager API V1");
        options.RoutePrefix = string.Empty;
        options.DefaultModelsExpandDepth(1);
      options.DefaultModelExpandDepth(1);
    });
}

// Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Use CORS based on environment
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowLocalhost");
}
else
{
    app.UseCors("AllowProduction");
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Health Check Endpoint
app.MapHealthChecks("/api/health");

app.Run();

