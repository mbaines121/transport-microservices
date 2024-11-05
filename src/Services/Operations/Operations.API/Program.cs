using HealthChecks.UI.Client;
using BuildingBlocks.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

var databaseConnectionString = builder.Configuration.GetConnectionString("Database");

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
    config.Schema.For<Company>().Identity(m => m.CompanyId);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CompaniesInitialData>();
}

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(assembly);

//builder.Services.AddMessageBroker(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString!);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => "Operations API");

app.Run();
