using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControleDeGastos.API.src.Data.DTOs.PessoaDTO;

/// <summary>
/// DTO responsável pela atualização de uma pessoa.
/// Contém as validações e os dados necessários para atualizar uma pessoa já existente no sistema.
/// </summary>
public class UpdatePessoaDTO
{
    /// <summary>
    /// Nome da pessoa.
    /// O nome deve ser fornecido e ter no máximo 100 caracteres.
    /// </summary>
    [Required(ErrorMessage = "Esse campo é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    /// <summary>
    /// Idade da pessoa.
    /// A idade deve ser fornecida e estar no intervalo de 1 a 122 anos.
    /// </summary>
    [Required(ErrorMessage = "Esse campo é obrigatório")]
    [Range(1, 122, ErrorMessage = "A idade deve estar entre 1 e 122 anos.")]
    public int Idade { get; set; }
}
