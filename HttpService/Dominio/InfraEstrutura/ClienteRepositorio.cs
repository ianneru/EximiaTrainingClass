using CSharpFunctionalExtensions;
using Dapper;
using HttpService.Dominio;
using HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra;

public sealed class ClienteRepositorio(
    PropostaDBContext dbContext)
{
    public async Task<bool> ClienteAtivo(string cpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>(@"
                SELECT Cliente.Ativo FROM Cliente 
                WHERE Cliente.Cpf = @cpf AND Cliente.Ativo = 1",
        new { cpf });
        
        return result == cpf;
    }

    public async Task<Maybe<Cliente>> ObterPorCpf(string cpf)
    {
        return await dbContext.Clientes.FirstOrDefaultAsync(o => o.Cpf == cpf) ?? Maybe<Cliente>.None;
    }

    public async Task Adicionar(Cliente Cliente, CancellationToken cancellationToken)
    {
        await dbContext.Clientes.AddAsync(Cliente, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }   
}