using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.src.Models;

// Classe que representa o modelo de pessoa
public class Pessoa
{
    [Key]
    public int Id { get; set; }// Identificador da pessoa

    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }// Nome da pessoa

    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [Range(0,122, ErrorMessage = "A idade deve estar entre 0 e 122 anos.")]
    public int Idade { get; set; }// Idade da pessoa

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>(); // Propriedade de navegação para os dados de Transacao

    // Cálculo do total de receitas
    public decimal TotalReceitas => Transacoes?
        .Where(transacao => transacao.Tipo == TipoTransacao.Receita)
        .Sum(transacao => transacao.Valor) ?? 0;

    // Cálculo do total de despesas
    public decimal TotalDespesas => Transacoes?
        .Where(transacao => transacao.Tipo == TipoTransacao.Despesa)
        .Sum(transacao => transacao.Valor) ?? 0;

    // Saldo calculado com base nas receitas e despesas
    public decimal Saldo => TotalReceitas - TotalDespesas;
}
