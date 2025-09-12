using FlightSystem.Data;
using FlightSystem.Shared.Extensions;
using FlightSystem.Server.Hubs;
using FlightSystem.Server.Services;
using FlightSystem.Server.Middleware;
using FlightSystem.Shared.Interfaces.Services;
using FlightSystem.Shared.Services;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Serilog логийг тохируулах
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/server-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Сервис тохируулга
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// Swagger/OpenAPI тохируулга
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Flight System API", 
        Version = "v1",
        Description = "Нислэгийн систем API",
        Contact = new() { 
            Name = "Flight System Team", 
            Email = "support@flightsystem.mn" 
        }
    });
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CORS тохируулга
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("Development", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000", "http://localhost:5173", "https://localhost:7001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// SignalR тохируулга
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
});

// Өгөгдлийн сервисүүд
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddSharedServices();

// Business сервисүүд
builder.Services.AddScoped<IAircraftService, AircraftService>();
builder.Services.AddScoped<IBoardingPassService, BoardingPassService>();
builder.Services.AddScoped<ICheckinService, CheckinService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ISeatService, SeatService>();

// Server сервисүүд
builder.Services.AddSingleton<ISocketService, SocketService>();
builder.Services.AddScoped<IRealtimeService, FlightSystem.Server.Services.RealtimeService>();
builder.Services.AddScoped<INotificationService, FlightSystem.Server.Services.NotificationService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Өгөгдлийн санг бэлдэх
await app.Services.EnsureDatabaseCreatedAsync();

// HTTP pipeline
// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight System API V1");
    c.RoutePrefix = "";
    c.DisplayRequestDuration();
    c.EnableTryItOutByDefault();
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}
else
{
    app.UseCors("AllowAll");
}

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

// API Routes
app.MapControllers();

// SignalR Hub
app.MapHub<FlightHub>("/flightHub");

// Socket Service эхлүүлэх
var socketService = app.Services.GetRequiredService<ISocketService>();
_ = Task.Run(() => socketService.StartAsync());

app.Run();

