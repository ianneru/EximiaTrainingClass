using CSharpFunctionalExtensions;
using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Proposta : Entity
    {
        public Guid IdCliente { get; set; }
        public Guid IdAgente { get; set; }

        public Cliente Cliente { get; set; }

        public Agente Agente { get; set; }

        public TipoOperacaoEnum TipoOperacao { get; set; }

        public decimal Valor { get; set; }

        public int PrazoEmMeses { get; set; }

        public bool Ativo { get; set; }


        public static Result<Proposta> Criar(Maybe<Cliente> cliente,Maybe<Agente> agente, TipoOperacaoEnum tipoOperacao,decimal valorOperacao,
            int prazoEmMeses,Maybe<Convenio> Convenio,ICollection<Convenio> Convenios)
        {
            if (cliente.HasNoValue) return Result.Failure<Proposta>("Cliente inválido");
            if (agente.HasNoValue) return Result.Failure<Proposta>("Agente inválido");
            if (Convenio.HasNoValue) return Result.Failure<Proposta>("Convenio inválido");

            var validacoes = new List<IValidacaoProposta>
            {
                new ValidacaoConveniadaREFIN(),
                new ValidacaoRestricaoConvenios(),
                new ValidacaoQuantidadeParcelasMaiorQue80AnosCliente()
            };

            foreach (var validacao in validacoes)
            {
                var resultado = validacao.Validar(cliente, agente,tipoOperacao,valorOperacao,prazoEmMeses,Convenio,Convenios);
                if (resultado.IsFailure)
                    return Result.Failure<Proposta>(resultado.Error);
            }

            var proposta = new Proposta
            {
                IdAgente = agente.Value.Id,
                IdCliente = cliente.Value.Id,
                TipoOperacao = tipoOperacao,
                DataCadastro = DateTime.Now,
                Valor = valorOperacao,
                PrazoEmMeses = prazoEmMeses,
                Ativo = true,
                Id = Guid.NewGuid()
            };

            return Result.Success(proposta);
        }
    }

    public interface IValidacaoProposta
    {
        Result Validar(Maybe<Cliente> Cliente, Maybe<Agente> Agente, TipoOperacaoEnum tipoOperacao, decimal valorOperacao,
            int prazoEmMeses, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios);
    }

    public class ValidacaoConveniadaREFIN : IValidacaoProposta
    {
        public Result Validar(Maybe<Cliente> Cliente, Maybe<Agente> Agente, TipoOperacaoEnum tipoOperacao, decimal valorOperacao,
            int prazoEmMeses, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            if (!Convenio.Value.AceitaREFIN && tipoOperacao == TipoOperacaoEnum.REFIN)
                return Result.Failure("Conveniada não aceita REFIN.");
            return Result.Success();
        }
    }

    public class ValidacaoRestricaoConvenios : IValidacaoProposta
    {
        public Result Validar(Maybe<Cliente> Cliente, Maybe<Agente> Agente, TipoOperacaoEnum tipoOperacao, decimal valorOperacao, 
            int prazoEmMeses, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            if (Convenio.Value.ConvenioRestricoes is null && 
                Convenio.Value.ConvenioRestricoes.Any(restricao => restricao.Uf == Cliente.Value.Uf && valorOperacao > restricao.Valor))
                return Result.Failure("Conveniada não aceita REFIN.");
            return Result.Success();
        }
    }

    public class ValidacaoQuantidadeParcelasMaiorQue80AnosCliente : IValidacaoProposta
    {
        public Result Validar(Maybe<Cliente> Cliente, Maybe<Agente> Agente, TipoOperacaoEnum tipoOperacao, decimal valorOperacao,
            int prazoEmMeses, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            var dataPrazoEmMeses = DateTime.Now.AddMonths(prazoEmMeses);

            var dataDiferencaAnos = dataPrazoEmMeses.Year - Cliente.Value.DataNascimento.Year;

            if(Cliente.Value.DataNascimento.AddYears(dataDiferencaAnos).Year > 79)
                return Result.Failure("Idade do prononente não pode ser superior que 80 anos na última parcela.");

            return Result.Success();
        }
    }
}
