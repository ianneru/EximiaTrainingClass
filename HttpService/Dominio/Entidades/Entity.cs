using CSharpFunctionalExtensions;

namespace HttpService.Dominio.Entidades
{
    public partial class Entity : Entity<Guid>
    {
        public DateTime DataCadastro { get; set; }

        public DateTime DataUltimaAlteracao { get; set; }
    }
}
