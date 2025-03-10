using ControleDeGastos.API.src.Data.Repositories;
using ControleDeGastos.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configura a política de CORS para permitir requisições do frontend React.
builder.Services.AddCors(options =>
{
    // Define uma política chamada PermitirRequisicoes que permite requisições do frontend React.
    options.AddPolicy("PermitirRequisicoes",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ControleDeGastosContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona o repositório de consulta de totais ao contêiner de serviços.
builder.Services.AddScoped<ConsultaTotaisRepository>();

// Configura o AutoMapper para escanear todos os assemblies do domínio atual.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adiciona serviços de controle de gastos.
builder.Services.AddControllers().AddNewtonsoftJson();

// Configura o Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Gastos API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Aplica a política de CORS configurada
app.UseCors("PermitirRequisicoes");

// Configura as requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
