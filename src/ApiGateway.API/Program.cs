using ApiGateway.API.Extensions;
using ApiGateway.Application;
using ApiGateway.Application.Interfaces;
using ApiGateway.Application.Services;
using ApiGateway.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGatewayMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("GatewayCors");
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
