using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.API.src.Data.DTOs.PessoaDTO;

public class CreatePessoaDTO
{
    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }// Nome da pessoa

    [Required(ErrorMessage = "Esse campo é obrigatorio")]
    [Range(0, 122, ErrorMessage = "A idade deve estar entre 0 e 122 anos.")]
    public int Idade { get; set; }// Idade da pessoa

}
