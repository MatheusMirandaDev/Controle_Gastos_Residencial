using AutoMapper;
using ControleDeGastos.API.src.Data.DTOs.PessoaDTO;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Controllers;

// Define a rota base para o controller
[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase 
{
    private readonly ControleDeGastosContext _context; // Contexto do banco de dados
    private readonly IMapper _mapper; // Mapeador de objetos

    // Construtor que recebe o contexto do banco de dados e o mapeador de objetos
    public PessoaController(ControleDeGastosContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    // Define a rota para a ação AdicionarPessoa
    public async Task<IActionResult> CreatePessoa([FromBody] CreatePessoaDTO pessoaDto)
    {
       Pessoa pessoa = _mapper.Map<Pessoa>(pessoaDto);
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa);
    }

    // Define a rota para a ação ObterPessoa com paginação
    [HttpGet]
    public async Task<IEnumerable<ReadPessoaDTO>> GetPessoa()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        return _mapper.Map<List<ReadPessoaDTO>>(pessoas);
    }

    // Define a rota para a ação DeletarPessoa
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Id == id);
        if (pessoa == null) return NotFound();
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}