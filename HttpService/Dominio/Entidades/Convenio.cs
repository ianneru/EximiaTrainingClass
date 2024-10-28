namespace HttpService.Dominio.Entidades
{
    public class Convenio : Entity
    {
        public string Codigo { get; set; }

        public Guid IdOperacao { get; set; }

        public bool AceitaREFIN { get; set; }

        public ICollection<ConvenioRestricoes> ConvenioRestricoes { get; set; }
    }
}
