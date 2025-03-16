using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.src.Data.Repositories;

// Bibliotecas necessárias para o funcionamento do repositório genérico 
public class Repository<T> where T : class
{
    // Contexto do banco de dados
    protected readonly ControleDeGastosContext _context;

    // Construtor que recebe o contexto do banco de dados
    public Repository(ControleDeGastosContext context)
    {
        this._context = context;
    }

    // Método para listar todos os objetos do banco de dados
    public async Task<IEnumerable<T>> Listar()
    {
        return await _context.Set<T>().ToListAsync();
    }

    // Método para adicionar um objeto no banco de dados
    public async Task Adicionar(T objeto)
    {
        await _context.Set<T>().AddAsync(objeto);
        await _context.SaveChangesAsync();
    }

    // Método para remover um objeto no banco de dados
    public async Task Remover(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        await _context.SaveChangesAsync();
    }

}