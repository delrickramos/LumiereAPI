using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.API.Repository;
using Lumiere.API.Services.Interfaces;
using Lumiere.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<LumiereContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Lumiere"))
);

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<ISessaoRepository, SessaoRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFormatoSessaoRepository, FormatoSessaoRepository>();
builder.Services.AddScoped<ITipoIngressoRepository, TipoIngressoRepository>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IFormatoSessaoService, FormatoSessaoService>();
builder.Services.AddScoped<ITipoIngressoService, TipoIngressoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
