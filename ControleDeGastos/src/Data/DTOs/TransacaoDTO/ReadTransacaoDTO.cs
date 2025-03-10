using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;

public class ReadTransacaoDTO
{
    public string Descricao { get; set; } // Descrição da transação
    public decimal Valor { get; set; } // Valor da transação
    public TipoTransacao Tipo { get; set; } // Receita ou Despesa
    public ReadPessoaDTO Pessoa { get; set; } // Pessoa da transação
}
