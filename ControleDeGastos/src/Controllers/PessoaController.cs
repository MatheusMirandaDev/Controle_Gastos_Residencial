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
    /// <response code="200">Pessoa encontrada.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<ReadPessoaDTO>> GetPessoa()
    {
        var pessoas = await _context.Pessoas.ToListAsync(); // Obtém todas as pessoas do banco de dados
        return _mapper.Map<List<ReadPessoaDTO>>(pessoas); // Mapeia as pessoas para DTOs de leitura
    }

    /// <summary>
    /// Atualiza uma pessoa com base no ID fornecido.
    /// </summary>
    /// <param name="id">O ID da pessoa a ser atualizada.</param>
    /// <param name="pessoaDto">Os dados para atualizar a pessoa.</param>
    /// <returns></returns>
    /// <response code="204">Pessoa atualizada com sucesso.</response>
    /// <response code="400">Dados inválidos no corpo da requisição.</response>
    /// <response code="404">Pessoa não encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePessoa(int id, [FromBody] UpdatePessoaDTO pessoaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna 400 se os dados inseridos forem inválidos
        }

        var pessoa = await _context.Pessoas.FindAsync(id); // Obtém a pessoa do banco de dados
        if (pessoa == null)
        {
            return NotFound(); // Retorna 404 se não encontrar a pessoa
        }

        _mapper.Map(pessoaDto, pessoa); // Mapeia o DTO para a entidade Pessoa
        _context.Pessoas.Update(pessoa); // Atualiza a pessoa no contexto
        await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
        return NoContent(); // Retorna o código HTTP 204 (sem conteúdo)
    }

    /// <summary>
    /// Remove uma pessoa do banco de dados com base no ID fornecido.
    /// </summary>
    /// <param name="id">O ID da pessoa a ser deletada.</param>
    /// <returns>Resultado da operação de exclusão.</returns>
    /// <response code="204">Pessoa deletada com sucesso.</response>
    /// <response code="404">Pessoa não encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id); // Obtém a pessoa do banco de dados
        // Se a pessoa não for encontrada, retorna 404
        if (pessoa == null)
        {
            return NotFound();
        }

        _context.Pessoas.Remove(pessoa); // Remove a pessoa do contexto
        await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados

        return NoContent();
    }

}