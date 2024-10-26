using EscolaEximia.HttpService.Handlers;
using HttpService.Dominio.Entidades;
using HttpService.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;

namespace HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaController : ControllerBase
    {
        public record RendimentoModel()
        public record NovaPropostaModel(string CpfAgente, string CpfCliente, string Telefone, string Email, string Cep, string Numero,
            string Logradouro,string Complemento, string Cidade, UfEnum Uf, RendimentoModel RendimentoModel);

        public async Task<IActionResult> CriarProposta([FromBody] NovaPropostaModel input,
        [FromServices] CriarPropostaHandler handler,
        CancellationToken cancellationToken)
        {
            var command = new CriarPropostaCommand(
                input.CpfAgente,
                input.CpfCliente,
                input.Telefone,
                input.Email,
                input.Cep,
                input.Numero,
                input.Logradouro,
                input.Complemento,
                input.Cidade,
                input.Uf,);

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);

        }
    }
}
