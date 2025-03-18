using System.Reflection;
using ControleDeGastos.API.src.Data.Repositories;
using ControleDeGastos.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "PermitirRequisicoes",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // Permite apenas o frontend React
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
});

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ControleDeGastosContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<ConsultaTotaisRepository>();

//
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Controle de Gastos Residenciais API",
            Description =
                "Esta API permite o controle e gerenciamento de transações financeiras residenciais, como despesas e receitas. A API oferece funcionalidades para cadastrar, listar, atualizar e excluir transações, além de fornecer informações detalhadas sobre as pessoas associadas a cada transação.",
            Contact = new OpenApiContact
            {
                Name = "Matheus Miranda Batista",
                Email = "matheusmiranda.batista@gmail.com",
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT"),
            },
        }
    );

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseCors("PermitirRequisicoes");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
