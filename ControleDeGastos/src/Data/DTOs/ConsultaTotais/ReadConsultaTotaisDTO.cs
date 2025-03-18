namespace ControleDeGastos.API.src.Data.DTOs.ConsultaTotais;

/// <summary>
/// DTO responsável por representar os totais de uma pessoa, incluindo receitas, despesas e saldo.
/// </summary>
public class ReadConsultaTotaisDTO
{
    /// <summary>
    /// Nome da pessoa.
    /// </summary>
    public string Nome { get; set; }

    /// <summary>
    /// Total de receitas da pessoa.
    /// </summary>
    public decimal TotalReceitas { get; set; }

    /// <summary>
    /// Total de despesas da pessoa.
    /// </summary>
    public decimal TotalDespesas { get; set; }

    /// <summary>
    /// Calcula o saldo da pessoa, que é a diferença entre receitas e despesas.
    /// </summary>
    /// <returns>O saldo é o resultado entre TotalReceitas - TotalDespesas.</returns>
    public decimal Saldo
    {
        get { return TotalReceitas - TotalDespesas; }
    }
}
