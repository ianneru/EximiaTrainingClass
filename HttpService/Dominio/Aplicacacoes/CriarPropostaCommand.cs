using HttpService.Dominio.Entidades;
using HttpService.Dominio.Enumeradores;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaCommand
{
    public string CpfAgente { get; set; }
    public string CpfCliente { get; set; }
    private string Telefone { get; set; }
    private string Email { get; set; }
    public Rendimento Rendimento { get; set; }

    public Endereco Endereco { get; set; }
    public string CodigoOperacao { get; set; }
    public string CodigoConvenio { get; set; }

    public CriarPropostaCommand(string CpfAgente, string CpfCliente, string Telefone, string Email, Endereco EnderecoModel,
         Rendimento RendimentoModel,string codigoOperacao, string codigoConvenio)
    {
        this.CpfAgente = CpfAgente;
        this.CpfCliente = CpfCliente;
        this.Telefone = Telefone;
        this.Email = Email;
        Endereco = EnderecoModel;
        this.CodigoOperacao = codigoOperacao;
        this.CodigoConvenio = codigoConvenio;
        Rendimento = RendimentoModel;
    }
}