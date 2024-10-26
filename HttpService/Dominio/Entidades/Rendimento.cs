namespace HttpService.Dominio.Entidades
{
    public class Rendimento
    {
        public int IdRendimento { get; set; } 
        
        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string NumeroConta { get; set; }

        public decimal Valor { get; set; }
    }
}
