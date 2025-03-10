using ControleDeGastos.API.src.Data.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ControleDeGastos.API.src.Controllers;
[ApiController]
[Route("[controller]")]
public class ConsultaTotaisController : ControllerBase
{
    private readonly ConsultaTotaisRepository _consultaTotaisRepository;

    // Injeção de dependência para o repositório
    public ConsultaTotaisController(ConsultaTotaisRepository consultaTotaisRepository)
    {
        _consultaTotaisRepository = consultaTotaisRepository;
    }

    // Método que retorna os totais de cada pessoa (Receitas, Despesas e Saldo)
    [HttpGet]
    public async Task<IActionResult> GetTotais()
    {
        var totais = await _consultaTotaisRepository.GetTotais();

        if (totais == null || !totais.Any())
        {
            return NotFound("Não há pessoas cadastradas para consultar os registros!");
        }

        return Ok(totais);
    }

    // Método que retorna os totais gerais (Receitas, Despesas, Saldo)
    [HttpGet("gerais")]
    public async Task<IActionResult> GetTotaisGerais()
    {
        var totaisGerais = await _consultaTotaisRepository.ObterTotaisGerais();

        return Ok(totaisGerais);
    }
}
