using HttpService.Dominio.Enumeradores;

namespace EscolaEximia.HttpService.Handlers;

public class CriarPropostaCommand
{
    private string CpfAgente { get; set; }
    private string CpfCliente { get; set; }
    private string Telefone { get; set; }
    private string Email { get; set; }
    private string Cep { get; set; }
    private string Numero { get; set; }
    private string Logradouro { get; set; }
    private string? Complemento { get; set; }
    private UfEnum Uf { get; set; }
    private string Cidade { get; set; }

    public CriarPropostaCommand(string CpfAgente, string CpfCliente, string Telefone, string Email, string Cep, string Numero,
            string Logradouro, string? Complemento, string Cidade, UfEnum Uf)
    {
        this.CpfAgente = CpfAgente;
        this.CpfCliente = CpfCliente;
        this.Telefone = Telefone;
        this.Email = Email;
        this.Cep = Cep;
        this.Telefone = Telefone;
        this.Numero = Numero;
        this.Logradouro = Logradouro;
        this.Complemento = Complemento;
        this.Uf = Uf;
        this.Cidade = Cidade;
    }
}