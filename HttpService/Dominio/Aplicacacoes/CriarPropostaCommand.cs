using HttpService.Dominio.Entidades;
using HttpService.Dominio.Enumeradores;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaCommand
{
    public string CpfAgente { get; set; }
    public string CpfCliente { get; set; }
    public Rendimento Rendimento { get; set; }

    public Endereco Endereco { get; set; }

    public Cliente Cliente { get; set; }
    public string CodigoOperacao { get; set; }
    public string CodigoConvenio { get; set; }

    public string PrazoEmMeses { get; set; }
    public decimal ValorOperacao { get; }
    public Proposta Proposta { get; set; }

    public CriarPropostaCommand(string CpfAgente, string CpfCliente, string Telefone, string Email, Endereco EnderecoModel,
         Rendimento RendimentoModel,string CodigoOperacao, string CodigoConvenio,string PrazoEmMeses,decimal ValorOperacao,DateTime DataNascimento)
    {
        this.CpfAgente = CpfAgente;
        this.Cliente = new Cliente
        {
            Cpf = CpfCliente ,
            Telefone = Telefone,
            Email = Email,
            DataNascimento = DataNascimento
        };
        this.CpfCliente = CpfCliente;
        Endereco = EnderecoModel;
        this.CodigoOperacao = CodigoOperacao;
        this.CodigoConvenio = CodigoConvenio;
        Rendimento = RendimentoModel;
        this.PrazoEmMeses = PrazoEmMeses;
        this.ValorOperacao = ValorOperacao;
    }
}