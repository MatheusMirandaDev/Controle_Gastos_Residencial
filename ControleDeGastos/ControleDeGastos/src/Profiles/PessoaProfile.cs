using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Models;

namespace ControleDeGastos.API.src.Profiles;

// Classe que define o mapeamento entre os modelos de Pessoa e os DTOs de Pessoa
public class PessoaProfile : Profile
{
    public PessoaProfile()
    {
        // Mapeia CreatePessoaDTO para Pessoa (usado na criação de uma nova pessoa)
        CreateMap<CreatePessoaDTO, Pessoa>();

        // Mapeia Pessoa para ReadPessoaDTO (usado na leitura dos dados de uma pessoa)
        CreateMap<Pessoa, ReadPessoaDTO>();
    }

}
