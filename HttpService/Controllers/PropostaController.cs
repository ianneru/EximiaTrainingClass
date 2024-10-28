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
        public record RendimentoModel(string Banco,string NumeroConta,string Agencia, decimal Valor);

        public record EnderecoModel(string Cep, string Numero,string Logradouro, string Complemento,string Cidade, UfEnum Uf,string Bairro);
        public record NovaPropostaModel(string CpfAgente, string CpfCliente, string Telefone, string Email, EnderecoModel Endereco,
            RendimentoModel Rendimento, string CodigoConvenio,int PrazoEmMeses,decimal ValorOperacao,TipoOperacaoEnum TipoOperacao, DateTime DataNascimento);

        public async Task<IActionResult> CriarProposta([FromBody] NovaPropostaModel input,
        [FromServices] CriarPropostaHandler handler,
        CancellationToken cancellationToken)
        {
            var command = new CriarPropostaCommand(
                input.CpfAgente,
                input.CpfCliente,
                input.Telefone,
                input.Email,
                input.Endereco.Cep,
                input.Endereco.Numero,
                input.Endereco.Logradouro,
                input.Endereco.Cidade,
                input.Endereco.Bairro,
                input.Endereco.Uf,
                input.Endereco.Complemento,
                input.Rendimento.Agencia,
                input.Rendimento.Banco,
                input.Rendimento.NumeroConta,
                input.Rendimento.Valor,
                input.CodigoConvenio,
                input.PrazoEmMeses,
                input.ValorOperacao,
                input.DataNascimento,
                input.TipoOperacao);

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);

        }
    }
}
