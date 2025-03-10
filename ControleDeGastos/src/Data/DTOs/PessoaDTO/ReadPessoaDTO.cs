using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.API.src.Data.DTOs.PessoaDTO;

public class ReadPessoaDTO
{
    public string Nome { get; set; }// Nome da pessoa
    public int Idade { get; set; }// Idade da pessoa
}
