using ControleDeGastos.API.src.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeGastos.API.src.Controllers;

/// <summary>
/// Controlador responsável por gerenciar as operações relacionadas à consulta de totais de pessoas.
/// Este controlador oferece dois endpoints principais:
/// - Um para obter os totais de receitas, despesas e saldo por pessoa.
/// - Outro para obter os totais gerais (soma de todas as receitas, despesas e saldo).
/// </summary>
[ApiController]
[Route("[controller]")]
public class ConsultaTotaisController : ControllerBase
{
    private readonly ConsultaTotaisRepository _consultaTotaisRepository;

    // Injeção de dependência para o repositório
    /// <summary>
    /// Construtor do controlador.
    /// O repositório é responsável por realizar as operações de consulta dos totais de cada pessoa.
    /// </summary>
    /// <param name="consultaTotaisRepository">Repositório responsável pela consulta de totais.</param>
    public ConsultaTotaisController(ConsultaTotaisRepository consultaTotaisRepository)
    {
        _consultaTotaisRepository = consultaTotaisRepository; // Repositório responsável pela consulta de totais
    }

    /// <summary>
    /// Obtém os totais de cada pessoa (Receitas, Despesas e Saldo).
    /// A consulta retorna uma lista de objetos com os totais para cada pessoa.
    /// </summary>
    /// <returns>Uma lista de totais de cada pessoa.</returns>
    /// <response code="200">Retorna os totais com sucesso.</response>
    /// <response code="404">Se não houver pessoas cadastradas.</response>
    [HttpGet]
    public async Task<IActionResult> GetTotais()
    {
        // A palavra-chave 'await' é usada para esperar pela execução do método assíncrono,
        // garantindo que os totais sejam recuperados antes de retornar a resposta.
        var totais = await _consultaTotaisRepository.GetTotais();

        // Verifica se a lista de totais está vazia ou nula
        if (totais == null || !totais.Any())
        {
            // Se não houver pessoas cadastradas, retorna um código 404 com uma mensagem explicativa
            return NotFound("Não há pessoas cadastradas para consultar os registros!");
        }

        // Retorna os totais encontrados com um código de sucesso (200)
        return Ok(totais);
    }

    /// <summary>
    /// Obtém os totais gerais (Receitas, Despesas, Saldo).
    /// A consulta retorna os totais agregados de todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Os totais gerais.</returns>
    /// <response code="200">Retorna os totais gerais com sucesso.</response>
    [HttpGet("gerais")]
    public async Task<IActionResult> GetTotaisGerais()
    {
        // A palavra-chave 'await' é usada para esperar pela execução do método assíncrono,
        // garantindo que os totais gerais sejam recuperados antes de retornar a resposta.
        var totaisGerais = await _consultaTotaisRepository.ObterTotaisGerais();

        // Retorna os totais gerais encontrados com um código de sucesso (200)
        return Ok(totaisGerais);
    }
}
