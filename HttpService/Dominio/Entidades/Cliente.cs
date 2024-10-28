using CSharpFunctionalExtensions;
using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Cliente : Entity
    {
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Cep { get; set; }
        public required string Numero { get; set; }
        public required string Logradouro { get; set; }
        public string? Complemento { get; set; }
        public required UfEnum Uf { get; set; }
        public required string Cidade { get; set; }
        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string NumeroConta { get; set; }

        public decimal ValorRendimento { get; set; }

        public bool Ativo { get; set; }


        public static Result<Cliente> Criar(Maybe<Cliente> Cliente)
        {
            if (Cliente.HasNoValue ) return Result.Failure<Proposta>("Cliente inválido");

            var novoCliente = new Cliente
            {

            };

            return Result.Success(novoCliente);
        }
    }
}
