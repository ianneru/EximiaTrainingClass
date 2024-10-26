namespace HttpService.Dominio.Entidades
{
    public class Cliente
    {
        public required string Id { get; set; }
        public required string Cpf { get; set; }
        public required int IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public int IdRendimento { get; set; }
        public Rendimento Rendimento { get; set; }
    }
}
