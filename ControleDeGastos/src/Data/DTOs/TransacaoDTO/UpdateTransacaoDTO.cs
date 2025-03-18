using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;

/// <summary>
/// DTO responsável pela atualização de uma transacao.
/// Contém as validações e os dados necessários para atualizar uma transacao já existente no sistema.
/// </summary>
public class UpdateTransacaoDTO
{
    /// <summary>
    /// Descrição da transação.
    /// A descrição é obrigatória e deve ter no máximo 200 caracteres.
    /// </summary>
    [Required(ErrorMessage = "A Descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    /// <summary>
    /// Valor da transação.
    /// O valor é obrigatório e deve ser um número decimal com até 2 casas decimais.
    /// </summary>
    [Required(ErrorMessage = "O Valor é obrigatória.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; } // decimal é usado para valores monetários e opreções matemáticas

    /// <summary>
    /// Tipo da transação.
    /// Pode ser "Receita" ou "Despesa" sendo um campo obrigatório.
    /// </summary>
    [Required(ErrorMessage = "O Tipo é obrigatória.")]
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// ID da pessoa associada à transação.
    /// Representa a chave estrangeira da pessoa que realiza a transação.
    /// </summary>
    [Required]
    public int PessoaId { get; set; }
}
