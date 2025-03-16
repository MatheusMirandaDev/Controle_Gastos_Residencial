using ControleDeGastos.API.src.Data.Repositories;
using ControleDeGastos.src.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirRequisicoes",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Permite apenas o frontend React
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ControleDeGastosContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ConsultaTotaisRepository>();

//
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("PermitirRequisicoes");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();