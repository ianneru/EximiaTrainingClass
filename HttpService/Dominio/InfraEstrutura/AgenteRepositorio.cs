using CSharpFunctionalExtensions;
using Dapper;
using HttpService.Dominio;
using HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra;

public sealed class AgenteRepositorio(
    PropostaDBContext dbContext)
{
    public async Task<bool> AgenteAtivo(string cpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>(@"
                SELECT Agente.Cpf FROM Agente 
                WHERE Agente.Cpf = @cpf AND Agente.Ativo = 1",
        new { cpf });
        
        return result == cpf;
    }

    public async Task<Maybe<Agente>> ObterPorCpf(string cpf)
    {
        return await dbContext.Agentes.FirstOrDefaultAsync(o => o.Cpf == cpf) ?? Maybe<Agente>.None;
    } 

    public async Task Adicionar(Proposta proposta, CancellationToken cancellationToken)
    {
        await dbContext.Propostas.AddAsync(proposta, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }   
}