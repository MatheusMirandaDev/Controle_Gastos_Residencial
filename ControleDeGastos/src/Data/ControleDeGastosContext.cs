using ControleDeGastos.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.src.Data;

public class ControleDeGastosContext : DbContext
{
    //Construtor que recebe as opções de configuração do banco de dados
    public ControleDeGastosContext(DbContextOptions<ControleDeGastosContext> options) : base(options) { }

    //Definição das tabelas do banco de dados
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    //Método que configura o relacionamento entre as tabelas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define que as transações serão deletadas em cascata
        modelBuilder.Entity<Pessoa>()
            .HasMany(pessoa => pessoa.Transacoes)
            .WithOne(transacao => transacao.Pessoa)
            .HasForeignKey(transacao => transacao.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);  
    }
}
