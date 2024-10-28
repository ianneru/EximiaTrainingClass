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


        public static Result<Proposta> Criar(Cliente Cliente, Endereco Endereco,Agente Agente, Operacao Operacao)
        {
            if(Cliente is null) return Result.Failure<Proposta>("Cliente inválido");
            if (Endereco is null) return Result.Failure<Proposta>("Endereço inválido");
            if (Agente is null) return Result.Failure<Agente>("Cliente inválido");
            if (Operacao is null) return Result.Failure<Operacao>("Cliente inválido");

        }
    }
}
