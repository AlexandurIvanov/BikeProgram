using FluentValidation;
using BikeProgram.BL.Interfaces;
using BikeProgram.BL.Services;
using BikeProgram.DL.Interfaces;
using BikeProgram.DL.Repositories;
using BikeProgram.Host.Validators;
using BikeProgram.Models.Configurations;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Configurations
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

// Repositories (Data Layer)
builder.Services.AddSingleton<IBikeRepository, BikeMongoRepository>();
builder.Services.AddSingleton<IManufacturerRepository, ManufacturerMongoRepository>();

// Services (Business Layer)
builder.Services.AddScoped<IBikeCrudProgram, BikeCrudProgram>();
builder.Services.AddScoped<IBikeBusinessProgram, BikeBusinessProgram>();

// Mapster Configuration
var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<AddBikeRequestValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();