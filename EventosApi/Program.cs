using EventosApi.BL;
using EventosApi.BL.Caching;
using EventosApi.BL.Interfaces;
using EventosApi.BL.Profiles;
using EventosApi.Common;
using EventosApi.DAL;
using EventosApi.DAL.Interfaces;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Redis: mismo patron que la Guia #8 (IConnectionMultiplexer registrado como singleton)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")!, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

// Acceso a datos (DAL) con Dapper
builder.Services.AddTransient<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddTransient<IEventoRepository, EventoRepository>();
builder.Services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
builder.Services.AddTransient<IOrganizadorRepository, OrganizadorRepository>();

// Logica de negocio (BL) con AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<EventoProfile>();
    cfg.AddProfile<ParticipanteProfile>();
    cfg.AddProfile<OrganizadorProfile>();
});
builder.Services.AddTransient<IEventoService, EventoService>();
builder.Services.AddTransient<IParticipanteService, ParticipanteService>();
builder.Services.AddTransient<IOrganizadorService, OrganizadorService>();

var app = builder.Build();

var connectionString = builder.Configuration["AppSettings:ConnectionString"]!;
await DbInitializer.InitializeAsync(connectionString);

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthorization();

app.MapControllers();

app.Run();
