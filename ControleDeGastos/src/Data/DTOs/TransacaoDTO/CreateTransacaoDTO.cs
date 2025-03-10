using ControleDeGastos.src.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;

public class CreateTransacaoDTO
{

    [Required(ErrorMessage = "A Descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
    public string Descricao { get; set; } // Descrição da transação

    [Required(ErrorMessage = "O Valor é obrigatória.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; } // Valor da transação

    [Required(ErrorMessage = "O Tipo é obrigatória.")]
    public TipoTransacao Tipo { get; set; } // Receita ou Despesa

    [Required]
    public int PessoaId { get; set; } // Chave estrangeira de Pessoa
}
