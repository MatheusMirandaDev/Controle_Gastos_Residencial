using ControleDeGastos.API.src.Data.DTOs.ConsultaTotais;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Data.Repositories;

public class ConsultaTotaisRepository
{
    private readonly ControleDeGastosContext _context;

    public ConsultaTotaisRepository(ControleDeGastosContext context)
    {
        _context = context;
    }

    // Método para obter totais por pessoa
    public async Task<IEnumerable<ReadConsultaTotaisDTO>> GetTotais()
    {
        var totais = await _context.Pessoas
            .Select(pessoa => new ReadConsultaTotaisDTO
            {
                Nome = pessoa.Nome,
                TotalReceitas = pessoa.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesas = pessoa.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            }).ToListAsync();

        return totais;
    }


    // Método para obter totais gerais (somando todas as receitas e despesas)
    public async Task<ReadConsultaTotaisDTO> ObterTotaisGerais()
    {
        var totalReceitas = await _context.Pessoas
            .SelectMany(pessoa => pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Select(t => t.Valor))
            .SumAsync();  // Soma dos totais das receitas de todas as pessoas

        var totalDespesas = await _context.Pessoas
            .SelectMany(pessoa => pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Select(t => t.Valor))
            .SumAsync();  // Soma dos totais das despesas de todas as pessoas

        return new ReadConsultaTotaisDTO
        {
            Nome = "Totais Gerais",
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas
        };
    }

}