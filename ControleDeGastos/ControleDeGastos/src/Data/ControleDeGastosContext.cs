using ControleDeGastos.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.src.Data;

// Classe que representa o contexto do banco de dados
public class ControleDeGastosContext : DbContext
{
    // Construtor que recebe as opções de configuração do contexto
    public ControleDeGastosContext(DbContextOptions<ControleDeGastosContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas { get; set; } // Define a tabela de Pessoas
    public DbSet<Transacao> Transacoes { get; set; } // Define a tabela de Transações

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
