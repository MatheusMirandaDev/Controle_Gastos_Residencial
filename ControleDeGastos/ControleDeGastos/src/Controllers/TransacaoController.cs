using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.TransacaoDTO;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Controllers;

/// <summary>
/// Controlador responsável por gerenciar as operações relacionadas à entidade Transação.
/// </summary>
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

    /// <summary>
    /// Cria uma nova transação.
    /// </summary>
    /// <param name="transacaoDto">Os dados da transação a serem criados.</param>
    /// <returns>Retorna a transação criada.</returns>
    /// <response code="200">Retorna a transação criada com sucesso.</response>
    /// <response code="400">Se a pessoa não for encontrada ou se a transação não for permitida (para menores de idade que tentam registrar receita).</response>
    [HttpPost]
    public async Task<IActionResult> CreateTransacao([FromBody] CreateTransacaoDTO transacaoDto)
    {
        // Verifica se a pessoa existe
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Id == transacaoDto.PessoaId);
        if (pessoa == null)
        {
            // Retorna um erro caso a pessoa não seja encontrada
            return BadRequest("Pessoa não encontrada!");
        }

        // Verifica a idade da pessoa
        // Se a pessoa for menor de idade e tentar registrar uma receita, retorna um erro
        // Se a pessoa tem menos de 18 anos, apenas transações do tipo "Despesa" são permitidas (Tipo == 1)
        // Certifique-se de que o tipo da transação seja um número inteiro
        int tipoTransacao = Convert.ToInt32(transacaoDto.Tipo);

        // Se a pessoa tem menos de 18 anos, apenas transações do tipo "Despesa" (Tipo == 1) são permitidas
        if (pessoa.Idade < 18 && tipoTransacao != 1)
        {
            return BadRequest("Pessoas menores de 18 anos só podem registrar despesas.");
        }

        // Mapeia o DTO para o modelo de transação
        Transacao transacao = _mapper.Map<Transacao>(transacaoDto);

        // Adiciona a transação no contexto
        await _context.AddAsync(transacao);

        // Salva as mudanças no banco de dados
        await _context.SaveChangesAsync();

        // Mapeia a transação criada para um DTO de leitura
        var transacaoDTO = _mapper.Map<ReadTransacaoDTO>(transacao);

        // Retorna a transação criada, mapeada para o DTO de leitura
        return Ok(transacaoDTO);
    }

    /// <summary>
    /// Obtém todas as transações cadastradas.
    /// </summary>
    /// <returns>Uma lista de transações.</returns>
    /// <response code="200">Retorna a lista de transações com sucesso.</response>
    [HttpGet]
    public async Task<IEnumerable<ReadTransacaoDTO>> GetTransacao()
    {
        // Obtém todas as transações do banco de dados, incluindo os dados da pessoa
        var transacoes = await _context.Transacoes
                                        .Include(t => t.Pessoa) // Inclui os dados da pessoa relacionada à transação
                                        .AsNoTracking() // Melhora a performance ao não rastrear as entidades
                                        .ToListAsync();

        // Mapeia as transações para o DTO de leitura
        return _mapper.Map<List<ReadTransacaoDTO>>(transacoes);
    }
}
