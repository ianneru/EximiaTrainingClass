using HttpService.Dominio.Enumeradores;

namespace HttpService.Dominio.Entidades
{
    public class Convenio : Entity
    {
        public string Codigo { get; set; }

        public TipoOperacaoEnum TipoOperacao { get; set; }

        public bool AceitaREFIN { get; set; }

        public ICollection<ConvenioRestricao> ConvenioRestricoes { get; set; }

        public Convenio(string codigo, TipoOperacaoEnum tipoOperacao, bool aceitaREFIN, ICollection<ConvenioRestricao> convenioRestricoes )
        {
            Codigo = codigo;
            TipoOperacao = tipoOperacao;
            AceitaREFIN = aceitaREFIN;
            ConvenioRestricoes = convenioRestricoes;
        }
    }
}
