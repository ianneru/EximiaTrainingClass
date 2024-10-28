using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class ConvenioRestricoes : Entity
    {
        public Guid IdConvenio { get; set; }

        public UfEnum Uf { get; set; }

        public decimal Valor { get; set; }
    }
}
