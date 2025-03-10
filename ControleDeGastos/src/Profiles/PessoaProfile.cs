using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Profiles;

public class PessoaProfile : Profile
{
    public PessoaProfile()
    {
        CreateMap<CreatePessoaDTO, Pessoa>();  
         CreateMap<Pessoa, ReadPessoaDTO>();
    }

}
