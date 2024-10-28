using CSharpFunctionalExtensions;
using Dapper;
using HttpService.Dominio;
using HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra;

public sealed class ConvenioRepositorio(
    PropostaDBContext dbContext)
{
    public async Task<Maybe<Convenio>> ObterPorCodigo(string codigo)
    {
        return await dbContext.Convenios.FirstOrDefaultAsync(o => o.Codigo == codigo) ?? Maybe<Convenio>.None;
    }

    public async Task<ICollection<Convenio>> ObterTodos()
    {
        return await dbContext.Convenios.ToListAsync();
    }

    public async Task Adicionar(Convenio convenio, CancellationToken cancellationToken)
    {
        await dbContext.Convenios.AddAsync(convenio, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }   
}