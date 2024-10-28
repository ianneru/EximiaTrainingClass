using HttpService.Dominio.Entidades;
using HttpService.Dominio.Enumeradores;

namespace TestesUnidade
{
    public class PropostaTestes
    {
        [Fact]
        public void DeveCriarProposta_QuandoTodosOsCamposPreenchidos()
        {
            // Arrange
            var cliente = new Cliente
            {
                Agencia = "1",
                Banco = "NUBANK",
                Cep = "97544-330",
                Cidade = "Alegrete",
                Bairro = "Vera Cruz",
                Cpf = "998.735.980-90",
                DataNascimento = new DateTime(1981, 3, 1),
                Email = "email@email.com",
                Logradouro = "Rua México",
                Numero = "100",
                NumeroConta = "82737",
                Telefone = "51814888778",
                Uf = UfEnum.RS,
                ValorRendimento = 10000,
            };

            var agente = new Agente
            {
                Cpf = "17604-361"
            };

            var novoIdConvenio = Guid.NewGuid();

            var convenio = new Convenio("SIAPE", 
                TipoOperacaoEnum.Novo, true, 
                [ new ConvenioRestricao(novoIdConvenio,UfEnum.RS,99999999) ]);

            var convenioINSS = new Convenio("INSS",
                TipoOperacaoEnum.Novo, true, []);

            var convenioINSS2 = new Convenio("INSS2",
                TipoOperacaoEnum.Portab, true, []);

            // Act
            var resultado = Proposta.Criar(cliente, agente, TipoOperacaoEnum.Novo, 1000, 32, convenio,
            [
                convenioINSS,convenioINSS2
            ]);

            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.NotNull(resultado.Value);
            Assert.Equal(cliente.Cpf, resultado.Value.Cliente.Cpf);
            Assert.Equal(agente.Cpf, resultado.Value.Agente.Cpf);
            Assert.True(resultado.Value.Ativo);
        }
    }
}