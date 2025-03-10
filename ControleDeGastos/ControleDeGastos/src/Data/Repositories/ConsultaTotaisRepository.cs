using ControleDeGastos.API.src.Data.DTOs.ConsultaTotais;
using ControleDeGastos.src.Data;
using ControleDeGastos.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.API.src.Data.Repositories;

// Repositório para consulta de totais de receitas, despesas e saldo
public class ConsultaTotaisRepository
{
    private readonly ControleDeGastosContext _context;

    // Construtor que inicializa o contexto do banco de dados
    public ConsultaTotaisRepository(ControleDeGastosContext context)
    {
        _context = context;
    }

    // Método para obter os totais de receitas, despesas e saldo por pessoa
    public async Task<IEnumerable<ReadConsultaTotaisDTO>> GetTotais()
    {
        // Consulta para obter os totais de receitas, despesas e saldo por pessoa
        var totais = await _context.Pessoas
            .Select(pessoa => new ReadConsultaTotaisDTO
            {
                Nome = pessoa.Nome, // Nome da pessoa

                // Soma dos valores das receitas da pessoa
                TotalReceitas = pessoa.Transacoes 
                    .Where(t => t.Tipo == TipoTransacao.Receita) 
                    .Sum(t => t.Valor),

                // Soma dos valores das despesas da pessoa
                TotalDespesas = pessoa.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
               
            }).ToListAsync(); // Converte a consulta em uma lista

        return totais;
    }



    // Método para obter os totais gerais de receitas, despesas e saldo de todas as pessoas
    public async Task<ReadConsultaTotaisDTO> ObterTotaisGerais()
    {
        // Soma das receitas de todas as pessoas
        var totalReceitas = await _context.Pessoas
            .SelectMany(pessoa => pessoa.Transacoes
                .Where(transacao => transacao.Tipo == TipoTransacao.Receita)
                .Select(transacao => transacao.Valor))
            .SumAsync();

        // Soma das despesas de todas as pessoas
        var totalDespesas = await _context.Pessoas
            .SelectMany(pessoa => pessoa.Transacoes
                .Where(transacao => transacao.Tipo == TipoTransacao.Despesa)
                .Select(transacao => transacao.Valor))
            .SumAsync();

        // Retornando o DTO com os totais gerais
        return new ReadConsultaTotaisDTO
        {
            Nome = "Totais Gerais",  // Nome fixo para totais gerais
            TotalReceitas = totalReceitas,  // Total de receitas
            TotalDespesas = totalDespesas   // Total de despesas
        };
    }
}
