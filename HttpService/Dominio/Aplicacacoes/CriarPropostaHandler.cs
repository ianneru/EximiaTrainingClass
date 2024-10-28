using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Inscricoes.Infra;
using HttpService.Dominio.Entidades;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaHandler
{
    private readonly PropostaRepositorio _propostaRepositorio;
    private readonly ClienteRepositorio _clienteRepositorio;
    private readonly AgenteRepositorio _agenteRepositorio;
    private readonly ConvenioRepositorio _convenioRepositorio;

    public CriarPropostaHandler(PropostaRepositorio propostaRepositorio,ClienteRepositorio clienteRepositorio,AgenteRepositorio agenteRepositorio, ConvenioRepositorio convenioRepositorio)
    {
        _propostaRepositorio = propostaRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _agenteRepositorio = agenteRepositorio;
        _convenioRepositorio = convenioRepositorio;
    }

    public async Task<Result<Proposta>> Handle(CriarPropostaCommand command, CancellationToken cancellationToken)
    {
        if (await _propostaRepositorio.PropostaAbertaExistePorCliente(command.CpfCliente))
            return Result.Failure<Proposta>("Proponente possui propostas abertas");

        if (await _clienteRepositorio.ClienteAtivo(command.CpfCliente))
            return Result.Failure<Proposta>("Cliente está bloqueado");
        
        if (await _agenteRepositorio.AgenteAtivo(command.CpfAgente))
            return Result.Failure<Proposta>("Agente deve estar ativo");


        var clienteRecuperado = await _clienteRepositorio.ObterPorCpf(command.CpfCliente);
        if (clienteRecuperado.HasNoValue)
        {
            var novoClienteResult = Cliente.Criar(command.Cliente);
            await _clienteRepositorio.Adicionar(novoClienteResult.Value, cancellationToken);
            await _clienteRepositorio.Save();

            if (novoClienteResult.IsFailure)
                return Result.Failure<Proposta>(novoClienteResult.Error);

            clienteRecuperado = novoClienteResult.Value;
        }

        var agenteRecuperado = await _agenteRepositorio.ObterPorCpf(command.CpfAgente);
        var convenios =  await _convenioRepositorio.ObterTodos();
        var convenioRecuperado = await _convenioRepositorio.ObterPorCodigo(command.CodigoConvenio);

        var propostaResult = Proposta.Criar(clienteRecuperado,
            agenteRecuperado,
            command.TipoOperacaoEnum,
            command.ValorOperacao,
            command.PrazoEmMeses,
            convenioRecuperado,
            convenios
           );
        
        if (propostaResult.IsFailure)
            return Result.Failure<Proposta>(propostaResult.Error);

        var proposta = propostaResult.Value;
        await _propostaRepositorio.Adicionar(proposta, cancellationToken);
        await _propostaRepositorio.Save();

        return Result.Success(proposta);
    }
}
