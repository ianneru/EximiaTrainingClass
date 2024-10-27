using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Endereco : Entity
    {
        public required string Cep { get; set; }
        public required string Numero { get; set; }
        public required string Logradouro { get; set; }
        public string? Complemento { get; set; }
        public required UfEnum Uf { get; set; }
        public required string Cidade { get; set; }
    }
}
