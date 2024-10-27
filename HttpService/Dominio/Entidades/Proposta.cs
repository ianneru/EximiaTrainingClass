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


        public static Result<Proposta> Criar(string CpfAgente, string CpfCliente, string Telefone, string Email, string Cep, string Numero,
            string Logradouro, string? Complemento, string Cidade, UfEnum Uf)
        {


        }
    }
}
