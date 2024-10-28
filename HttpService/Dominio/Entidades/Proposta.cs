using CSharpFunctionalExtensions;
using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Proposta : Entity
    {
        public Guid IdCliente { get; set; }
        public Guid IdEndereco { get; set; }
        public Guid IdAgente { get; set; }

        public Guid IdOperacao { get; set; }


        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }

        public Agente Agente { get; set; }

        public Operacao Operacao { get; set; }

        public bool Ativo { get; set; }


        public static Result<Proposta> Criar(Cliente Cliente, Endereco Endereco,string CpfAgente, Operacao Operacao,Maybe<Convenio> Convenio,ICollection<Convenio> Convenios)
        {
            if(Cliente is null) return Result.Failure<Proposta>("Cliente inválido");
            if (Endereco is null) return Result.Failure<Proposta>("Endereço inválido");
            if (CpfAgente is null) return Result.Failure<Proposta>("Agente inválido");
            if (Operacao is null) return Result.Failure<Proposta>("Operacao inválido");
            if (!Convenio.HasValue) return Result.Failure<Proposta>("Convenio inválido");

            var validacoes = new List<IValidacaoProposta>
            {
                new ValidacaoConveniadaREFIN(),
                new ValidacaoRestricaoConvenios(),
                new ValidacaoQuantidadeParcelasMaiorQue80AnosCliente()
            };

            foreach (var validacao in validacoes)
            {
                var resultado = validacao.Validar(Cliente, Endereco,CpfAgente, Operacao, Convenio,Convenios);
                if (resultado.IsFailure)
                    return Result.Failure<Proposta>(resultado.Error);
            }

            var proposta = new Proposta();

            return Result.Success(proposta);
        }
    }

    public interface IValidacaoProposta
    {
        Result Validar(Cliente Cliente, Endereco Endereco, string CpfAgente, Operacao Operacao, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios);
    }

    public class ValidacaoConveniadaREFIN : IValidacaoProposta
    {
        public Result Validar(Cliente Cliente, Endereco Endereco, string CpfAgente, Operacao Operacao, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            if (!Convenio.Value.AceitaREFIN && Operacao.Codigo == "REFIN")
                return Result.Failure("Conveniada não aceita REFIN.");
            return Result.Success();
        }
    }

    public class ValidacaoRestricaoConvenios : IValidacaoProposta
    {
        public Result Validar(Cliente Cliente, Endereco Endereco, string CpfAgente, Operacao Operacao, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            if (Convenio.Value.ConvenioRestricoes is null && Convenio.Value.ConvenioRestricoes.Any(restricao => restricao.Uf == Endereco.Uf && Operacao.Valor > restricao.Valor))
                return Result.Failure("Conveniada não aceita REFIN.");
            return Result.Success();
        }
    }

    public class ValidacaoQuantidadeParcelasMaiorQue80AnosCliente : IValidacaoProposta
    {
        public Result Validar(Cliente Cliente, Endereco Endereco, string CpfAgente, Operacao Operacao, Maybe<Convenio> Convenio, ICollection<Convenio> Convenios)
        {
            var dataPrazoEmMeses = DateTime.Now.AddMonths((int)Operacao.PrazoEmMeses);

            var dataDiferencaAnos = dataPrazoEmMeses.Year - Cliente.DataNascimento.Year;

            if(Cliente.DataNascimento.AddYears(dataDiferencaAnos).Year > 79)
                return Result.Failure("Idade do prononente não pode ser superior que 80 anos na última parcela.");

            return Result.Success();
        }
    }
}
