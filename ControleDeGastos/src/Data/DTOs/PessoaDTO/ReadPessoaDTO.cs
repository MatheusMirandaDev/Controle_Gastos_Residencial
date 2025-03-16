using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.API.src.Data.DTOs.PessoaDTO;

/// <summary>
/// DTO responsável por representar os dados de uma pessoa para leitura.
/// Utilizado para retornar as informações de uma pessoa já cadastrada no sistema.
/// </summary>
public class ReadPessoaDTO
{
    /// <summary>
    /// Nome da pessoa.
    /// </summary>
    public int Id { get; set; }

    public string Nome { get; set; }

    /// <summary>
    /// Idade da pessoa.
    /// </summary>
    public int Idade { get; set; }
}