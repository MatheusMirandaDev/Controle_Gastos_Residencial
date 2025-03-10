using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControleDeGastos.src.Models;

// Enumerador que define os tipos de transação
public enum TipoTransacao
{
    Receita,
    Despesa
}

// Classe que representa o modelo de transação
public class Transacao
{
    [Key]
    public int Id { get; set; } // Identificador da transação

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
    public string Descricao { get; set; } // Descrição da transação

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; } // Valor da transação

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public TipoTransacao Tipo { get; set; } // Receita ou Despesa

    //Campos necessários para o relacionamento com a tabela de Pessoa
    [Required]
    public int PessoaId { get; set; } // Chave estrangeira de Pessoa

    [JsonIgnore]
    [Required]
    public Pessoa Pessoa { get; set; } // Propriedade de navegação para os dados de Pessoa
}