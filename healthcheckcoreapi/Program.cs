using healthcheckcoreapi.Repository;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProduct, ProductService>();

builder.Services
    .AddHealthChecks()
    .AddCheck<APIHealthCheck>(nameof(APIHealthCheck))
    .AddCheck<DatabaseHealth>(nameof(DatabaseHealth));

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Healthcheck API", "/health");
    })
    .AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

app.Run();
