using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Profiles;

// Classe que define o mapeamento entre os modelos de Transacao e os DTOs de Transacao
public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        // Mapeia CreateTransacaoDTO para Transacao(usado na criação de uma nova transação)
        CreateMap<CreateTransacaoDTO, Transacao>();

        // Mapeia Transacao para ReadTransacaoDTO (usado na leitura dos dados de uma transação)
        CreateMap<Transacao, ReadTransacaoDTO>();

        // Mapeia UpdateTransacaoDTO para Transacao (usado na atualização de uma transacao)
        CreateMap<UpdateTransacaoDTO, Transacao>();

        // Mapeia Pessoa para ReadPessoaDTO (para incluir os dados da pessoa na transação)
        CreateMap<Pessoa, ReadPessoaDTO>();
    }
}
