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
            if (Cliente.HasNoValue ) return Result.Failure<Cliente>("Cliente inválido");

            var novoCliente = new Cliente
            {
                Cpf = Cliente.Value.Cpf,
                Email = Cliente.Value.Email,
                Telefone = Cliente.Value.Telefone,
                DataNascimento = Cliente.Value.DataNascimento,
                Cep = Cliente.Value.Cep,
                Numero = Cliente.Value.Numero,
                Logradouro = Cliente.Value.Logradouro,
                Complemento = Cliente.Value.Complemento,
                Uf = Cliente.Value.Uf,
                Cidade = Cliente.Value.Cidade,
                Banco = Cliente.Value.Banco,
                Agencia = Cliente.Value.Agencia,
                NumeroConta = Cliente.Value.NumeroConta,
                ValorRendimento = Cliente.Value.ValorRendimento,
                Ativo = true,
                Id = Guid.NewGuid()
            };

            return Result.Success(novoCliente);
        }
    }
}
