using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

var databaseConnectionString = builder.Configuration.GetConnectionString("LocalDatabase");

if (!builder.Environment.IsDevelopment())
{
    databaseConnectionString = builder.Configuration.GetConnectionString("AzureDatabase");
}

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(config =>
{
    config.Connection(databaseConnectionString!);
    config.Schema.For<Job>().Identity(m => m.JobId);
}).UseLightweightSessions();

builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString!);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
