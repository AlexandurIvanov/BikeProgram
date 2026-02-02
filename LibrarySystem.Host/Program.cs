using FluentValidation;
using LibrarySystem.BL.Interfaces;
using LibrarySystem.BL.Services;
using LibrarySystem.DL.Interfaces;
using LibrarySystem.DL.Repositories;
using LibrarySystem.Host.Validators;
using LibrarySystem.Models.Configurations;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

builder.Services.AddSingleton<IBookRepository, BookMongoRepository>();
builder.Services.AddSingleton<IAuthorRepository, AuthorMongoRepository>();

builder.Services.AddScoped<IBookCrudService, BookCrudService>();
builder.Services.AddScoped<ILibraryBusinessService, LibraryBusinessService>();

var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddValidatorsFromAssemblyContaining<AddBookRequestValidator>();

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