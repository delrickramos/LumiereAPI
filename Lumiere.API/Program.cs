using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.API.Repository;
using Lumiere.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços OpenAPI e Swagger para documentação da API
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configuração para evitar referências circulares em JSON
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Configuração do contexto de banco de dados com SQL Server
builder.Services.AddDbContext<LumiereContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Lumiere"))
);

// Registro de dependências: repositórios
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<ISessaoRepository, SessaoRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFormatoSessaoRepository, FormatoSessaoRepository>();
builder.Services.AddScoped<ITipoIngressoRepository, TipoIngressoRepository>();
builder.Services.AddScoped<IAssentoRepository, AssentoRepository>();
builder.Services.AddScoped<IIngressoRepository, IngressoRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

// Registro de dependências: serviços
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IFormatoSessaoService, FormatoSessaoService>();
builder.Services.AddScoped<ITipoIngressoService, TipoIngressoService>();
builder.Services.AddScoped<IFilmeService, FilmeService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<ISessaoService, SessaoService>();
builder.Services.AddScoped<IAssentoService, AssentoService>();
builder.Services.AddScoped<IIngressoService, IngressoService>();


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
