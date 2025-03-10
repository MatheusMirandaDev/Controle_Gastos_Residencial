using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Controllers;

[ApiController]
[Route("[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly ControleDeGastosContext _context;
    private readonly IMapper _mapper;
    public TransacaoController(ControleDeGastosContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransacao([FromBody] CreateTransacaoDTO transacaoDto)
    {
        // Verifica se a pessoa existe
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Id == transacaoDto.PessoaId);
        if (pessoa == null) return BadRequest("Pessoa não encontrada!");

        // Verifica a idade da pessoa
        if (pessoa.Idade < 18 && transacaoDto.Tipo != TipoTransacao.Despesa)
        {
            return BadRequest("Pessoas menores de idade só podem ter despesas!");
        }

        // Mapeia o DTO para o modelo de transação
        Transacao transacao = _mapper.Map<Transacao>(transacaoDto);

        // Adiciona a transação no contexto
        await _context.AddAsync(transacao);
        await _context.SaveChangesAsync();

        // Retorna a transação criada, mapeada para o DTO de leitura
        var transacaoDTO = _mapper.Map<ReadTransacaoDTO>(transacao);
        return Ok(transacaoDTO); // Retorna apenas os dados necessários da transação
    }

    [HttpGet]
    public async Task<IEnumerable<ReadTransacaoDTO>> GetTransacao()
    {
        var transacoes = await _context.Transacoes
                                    .Include(t => t.Pessoa) // Evite carregar a lista de transações da pessoa aqui
                                    .AsNoTracking() // Melhor performance
                                    .ToListAsync();
        return _mapper.Map<List<ReadTransacaoDTO>>(transacoes);
    }

}