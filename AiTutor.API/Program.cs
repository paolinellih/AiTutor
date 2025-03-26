using AiTutor.Application;
using AiTutor.Application.Services;
using AiTutor.Infrastructure.Clients;
using AiTutor.Infrastructure.Seeders;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Configure Redis connection using the connection string from environment variables
var redisConnectionString = builder.Configuration["REDIS_CONNECTION_STRING"] ?? "localhost:6379"; // Fallback to localhost if not set
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services in the DI container
builder.Services.AddControllers();

builder.Services.AddHttpClient<IAiClient, OllamaAiClient>();
builder.Services.AddScoped<AiService>();  


var app = builder.Build();

// Seed data into Redis
using (var scope = app.Services.CreateScope())
{
    var redis = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
    RedisSeeder.SeedData(redis);  // Pre-populate the Redis data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapGet("/", () => "Hello, AiTutor!");

app.MapControllers();

app.Run();