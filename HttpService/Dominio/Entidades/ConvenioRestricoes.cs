using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class ConvenioRestricao : Entity
    {
        public Guid IdConvenio { get; set; }

        public UfEnum Uf { get; set; }

        public decimal Valor { get; set; }

        public ConvenioRestricao(Guid idConvenio, UfEnum uf, decimal valor)
        {
            IdConvenio = idConvenio;
            Uf = uf;
            Valor = valor;
        }
    }
}
