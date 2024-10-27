using CSharpFunctionalExtensions;
using Dapper;
using HttpService.Dominio;
using HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EscolaEximia.HttpService.Dominio.Inscricoes.Infra;

public sealed class PropostaRepositorio(
    PropostaDBContext dbContext)
{
    public async Task<bool> PropostaAbertaExistePorCliente(string cpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>(@"
                SELECT Cliente.Cpf FROM Proposta 
                JOIN Cliente ON Cliente.Id = Proposta.IdCliente    
                WHERE Cliente.Cpf = @cpf 
                AND Proposta.Ativo = 1 ",
        new { cpf });

        return result == cpf;
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