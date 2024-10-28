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
        public record RendimentoModel(string Banco,string NumeroConta,string Agencia, decimal Valor,TipoContaEnum TipoConta);

        public record EnderecoModel(string Cep, string Numero,string Logradouro, string Complemento,string Cidade, UfEnum Uf);
        public record NovaPropostaModel(string CpfAgente, string CpfCliente, string Telefone, string Email, EnderecoModel Endereco, RendimentoModel Rendimento);

        public async Task<IActionResult> CriarProposta([FromBody] NovaPropostaModel input,
        [FromServices] CriarPropostaHandler handler,
        CancellationToken cancellationToken)
        {
            var command = new CriarPropostaCommand(
                input.CpfAgente,
                input.CpfCliente,
                input.Telefone,
                input.Email,
                new Endereco {
                    Cep = input.Endereco.Cep,
                    Numero = input.Endereco.Numero,
                    Logradouro = input.Endereco.Logradouro,
                    Complemento = input.Endereco.Complemento,
                    Cidade = input.Endereco.Cidade,
                    Uf = input.Endereco.Uf,
                },
                new Rendimento
                {
                    Agencia = input.Rendimento.Agencia,
                    Banco = input.Rendimento.Banco,
                    NumeroConta = input.Rendimento.NumeroConta,
                    Valor = input.Rendimento.Valor
                });

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);

        }
    }
}
