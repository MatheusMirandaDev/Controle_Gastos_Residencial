using ControleDeGastos.API.src.Data.Repositories;
using ControleDeGastos.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configura a pol�tica de CORS para permitir requisi��es do frontend React.
builder.Services.AddCors(options =>
{
    // Define uma pol�tica chamada PermitirRequisicoes que permite requisi��es do frontend React.
    options.AddPolicy("PermitirRequisicoes",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configura��o do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ControleDeGastosContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona o reposit�rio de consulta de totais ao cont�iner de servi�os.
builder.Services.AddScoped<ConsultaTotaisRepository>();

// Configura o AutoMapper para escanear todos os assemblies do dom�nio atual.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adiciona servi�os de controle de gastos.
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

// Aplica a pol�tica de CORS configurada
app.UseCors("PermitirRequisicoes");

// Configura as requisi��es HTTP.
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
