using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Data.DTOs.TransacaoDTO
{
    /// <summary>
    /// DTO responsável pela leitura dos dados de uma transação.
    /// Contém as informações de uma transação para ser exibida na API.
    /// </summary>
    public class ReadTransacaoDTO
    {
        /// <summary>
        /// Identificador da transação.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição da transação.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor da transação.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação.
        /// </summary>
        public TipoTransacao Tipo { get; set; }

        /// <summary>
        /// Pessoa associada à transação.
        /// </summary>
        public ReadPessoaDTO Pessoa { get; set; }

        /// <summary>
        /// ID da Pessoa associada à transação.
        /// </summary>
        public int PessoaId { get; set; }
    }
}
