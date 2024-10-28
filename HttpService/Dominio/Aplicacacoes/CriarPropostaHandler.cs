using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;
using HttpService.Dominio.Entidades;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaHandler
{
    private readonly PropostaRepositorio _propostaRepositorio;
    private readonly ClienteRepositorio _clienteRepositorio;
    private readonly AgenteRepositorio _agenteRepositorio;

    public CriarPropostaHandler(PropostaRepositorio propostaRepositorio,ClienteRepositorio clienteRepositorio,AgenteRepositorio agenteRepositorio)
    {
        _propostaRepositorio = propostaRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _agenteRepositorio = agenteRepositorio;
    }

    public async Task<Result<Proposta>> Handle(CriarPropostaCommand command, CancellationToken cancellationToken)
    {
        if (await _propostaRepositorio.PropostaAbertaExistePorCliente(command.CpfCliente))
            return Result.Failure<Proposta>("Proponente possui propostas abertas");

        if (await _clienteRepositorio.ClienteAtivo(command.CpfCliente))
            return Result.Failure<Proposta>("Cliente está bloqueado");
        
        if (await _agenteRepositorio.AgenteAtivo(command.CpfAgente))
            return Result.Failure<Proposta>("Agente deve estar ativo");

        var propostaResult = Proposta.Criar(command.Cliente,command.Endereco,command.CpfAgente,
            new Operacao { 
                Codigo = command.CodigoOperacao, 
                Valor = command.ValorOperacao
            },
            new Convenio
            {

            });
        
        if (propostaResult.IsFailure)
            return Result.Failure<Proposta>(propostaResult.Error);

        var proposta = propostaResult.Value;
        await _propostaRepositorio.Adicionar(proposta, cancellationToken);
        await _propostaRepositorio.Save();

        return Result.Success(proposta);
    }
}
