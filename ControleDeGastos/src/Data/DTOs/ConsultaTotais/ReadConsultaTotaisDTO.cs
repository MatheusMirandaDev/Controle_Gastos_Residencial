namespace ControleDeGastos.API.src.Data.DTOs.ConsultaTotais;

public class ReadConsultaTotaisDTO
{
    public string Nome { get; set; }
    public decimal TotalReceitas { get; set; }

    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get { return TotalReceitas - TotalDespesas; } }
}
