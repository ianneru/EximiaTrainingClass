namespace HttpService.Dominio.Entidades
{
    public class Cliente : Entity
    {
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public Guid IdRendimento { get; set; }
        public Rendimento Rendimento { get; set; }

        public bool Ativo { get; set; }
    }
}
