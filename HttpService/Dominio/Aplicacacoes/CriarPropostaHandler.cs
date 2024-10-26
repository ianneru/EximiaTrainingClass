using CSharpFunctionalExtensions;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaHandler
{
    private readonly PropostaRepositorio _propostaRepositorio;

    public CriarPropostaHandler(PropostaRepositorio propostaRepositorio)
    {
        _propostaRepositorio = propostaRepositorio;
    }

    public async Task<Result<Proposta>> Handle(CriarPropostaCommand command, CancellationToken cancellationToken)
    {
        if()


        return Result.Success(novaProposta);
    }
}
