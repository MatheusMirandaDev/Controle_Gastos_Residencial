using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.src.Models;

// Classe que representa o modelo de pessoa
public class Pessoa
{
    [Key]
    public int Id { get; set; } // Identificador único da pessoa

    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } // Nome da pessoa

    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [Range(1, 122, ErrorMessage = "A idade deve estar entre 1 e 122 anos.")]
    public int Idade { get; set; } // Idade da pessoa

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>(); // PropriedACade de navegação para os dados de Transacao

    // Cálculo do total de receitas da pessoa
    public decimal TotalReceitas =>
        Transacoes
            ? // Verifica se a lista de transações não é nula
            .Where(transacao => transacao.Tipo == TipoTransacao.Receita) // Filtra as transações do tipo Receita
            .Sum(transacao => transacao.Valor) ?? 0; // Soma os valores das transações de Receita

    // Cálculo do total de despesas da pessoa
    public decimal TotalDespesas =>
        Transacoes
            ? // Verifica se a lista de transações não é nula
            .Where(transacao => transacao.Tipo == TipoTransacao.Despesa) // Filtra as transações do tipo Despes
            .Sum(transacao => transacao.Valor) ?? 0; // Soma os valores das transações de Despesa

    // Saldo calculado com base nas receitas e despesas
    public decimal Saldo => TotalReceitas - TotalDespesas;
}
