using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Controllers;

/// <summary>
/// Controlador responsável por gerenciar as operações relacionadas à entidade Pessoa.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private readonly ControleDeGastosContext _context; // Contexto do banco de dados
    private readonly IMapper _mapper; // Mapeador de objetos

    /// <summary>
    /// Construtor do controlador.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    /// <param name="mapper">Mapeador de objetos para DTOs.</param>
    public PessoaController(ControleDeGastosContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    /// <param name="pessoaDto">Os dados da pessoa a ser criada.</param>
    /// <returns>Retorna a pessoa criada.</returns>
    /// <response code="201">Pessoa criada com sucesso.</response>
    /// <response code="400">Erro de solicitação, dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePessoa([FromBody] CreatePessoaDTO pessoaDto)
    {
        Pessoa pessoa = _mapper.Map<Pessoa>(pessoaDto); // Mapeia o DTO para a entidade Pessoa
        await _context.Pessoas.AddAsync(pessoa); // Adiciona a pessoa ao contexto
        await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
        return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa); // Retorna a pessoa criada com o código HTTP 201
    }

    /// <summary>
    /// Obtém uma lista de todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Uma lista de pessoas.</returns>
    [HttpGet]
    public async Task<IEnumerable<ReadPessoaDTO>> GetPessoa()
    {
        var pessoas = await _context.Pessoas.ToListAsync(); // Obtém todas as pessoas do banco de dados
        return _mapper.Map<List<ReadPessoaDTO>>(pessoas); // Mapeia as pessoas para DTOs de leitura
    }

    /// <summary>
    /// Remove uma pessoa do banco de dados com base no ID fornecido.
    /// </summary>
    /// <param name="id">O ID da pessoa a ser deletada.</param>
    /// <returns>Resultado da operação de exclusão.</returns>
    /// <response code="204">Pessoa deletada com sucesso.</response>
    /// <response code="404">Pessoa não encontrada.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
