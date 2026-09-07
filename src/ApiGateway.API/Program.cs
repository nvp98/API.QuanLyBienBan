using ApiGateway.API.Authentication;
using ApiGateway.API.Extensions;
using ApiGateway.Application;
using ApiGateway.Application.Interfaces;
using ApiGateway.Application.Services;
using ApiGateway.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddScoped<IGatewayService, GatewayService>();

builder.Services
    .AddAuthentication(BasicAuthenticationOptions.DefaultScheme)
    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
        BasicAuthenticationOptions.DefaultScheme,
        options => builder.Configuration
            .GetSection("BasicAuth")
            .Bind(options));

builder.Services.AddAuthorization();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("GatewayCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("gateway", limiter =>
    {
        limiter.PermitLimit = 100;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueLimit = 0;
        limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(BasicAuthenticationOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Nhập username/password của Basic Auth"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = BasicAuthenticationOptions.DefaultScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseGatewayMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("GatewayCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapHealthChecks("/health");

app.MapGet("/", () => Results.Ok(new
{
    service = "API Gateway",
    framework = ".NET 9",
    proxy = "YARP",
    status = "running",
    time = DateTimeOffset.Now
}))
.RequireRateLimiting("gateway");

app.MapControllers();

app.MapReverseProxy(proxyPipeline =>
{
    // Có thể thêm authorization, logging hoặc custom proxy middleware tại đây.
});

app.Run();
