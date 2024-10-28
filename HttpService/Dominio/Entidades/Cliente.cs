using CSharpFunctionalExtensions;
using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Cliente : Entity
    {
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required DateTime DataNascimento { get; set; }
        public required string Cep { get; set; }
        public required string Numero { get; set; }
        public required string Logradouro { get; set; }

        public required string Bairro { get; set; }
        public string? Complemento { get; set; }
        public required UfEnum Uf { get; set; }
        public required string Cidade { get; set; }
        public required string Banco { get; set; }

        public required string Agencia { get; set; }

        public required string NumeroConta { get; set; }

        public required decimal ValorRendimento { get; set; }

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
                Bairro = Cliente.Value.Bairro,
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
