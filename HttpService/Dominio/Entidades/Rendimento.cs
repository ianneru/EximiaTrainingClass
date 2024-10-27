namespace HttpService.Dominio.Entidades
{
    public class Rendimento : Entity
    {
        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string NumeroConta { get; set; }

        public decimal Valor { get; set; }
    }
}
