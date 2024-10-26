namespace HttpService.Dominio.Entidades
{
    public class Proposta
    {
        public int IdProposta { get; set; }
        public int IdCliente { get; set; }
        public int IdEndereco { get; set; }
        public int IdAgente { get; set; }
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }

        public Agente Agente { get; set; }
    }
}
