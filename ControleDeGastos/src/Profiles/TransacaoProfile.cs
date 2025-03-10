using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Profiles;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<CreateTransacaoDTO, Transacao>(); 
        CreateMap<Transacao, ReadTransacaoDTO>();
        CreateMap<Pessoa, ReadPessoaDTO>();
    }
}
