using HttpService.Dominio.Entidades;
using HttpService.Dominio.Enumeradores;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaCommand
{
    public string CpfAgente { get; set; }
    public string CpfCliente { get; set; }

    public Cliente Cliente { get; set; }
    public string CodigoOperacao { get; set; }
    public string CodigoConvenio { get; set; }

    public string PrazoEmMeses { get; set; }
    public decimal ValorOperacao { get; }
    public Proposta Proposta { get; set; }

    public CriarPropostaCommand(string CpfAgente, string CpfCliente, string Telefone, string Email, string Cep,string Numero,string Logradouro,string Cidade,UfEnum Uf,
        string Complemento,string Agencia,string Banco,string NumeroConta, decimal ValorRendimento, string CodigoOperacao, string CodigoConvenio,string PrazoEmMeses,
        decimal ValorOperacao,DateTime DataNascimento)
    {
        this.CpfAgente = CpfAgente;
        this.Cliente = new Cliente
        {
            Cpf = CpfCliente,
            Telefone = Telefone,
            Email = Email,
            DataNascimento = DataNascimento,
            Cep = Cep,
            Cidade = Cidade,
            Numero = Numero,
            Logradouro = Logradouro,
            Uf  = Uf,
            Agencia = Agencia,
            Banco = Banco,
            NumeroConta = NumeroConta,
            Complemento = Complemento,
            ValorRendimento = ValorRendimento
        };
        this.CpfCliente = CpfCliente;
        this.CodigoOperacao = CodigoOperacao;
        this.CodigoConvenio = CodigoConvenio;
        this.PrazoEmMeses = PrazoEmMeses;
        this.ValorOperacao = ValorOperacao;
    }
}