using HttpService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace HttpService.Dominio
{
    public class PropostaDBContext : DbContext
    {
        public PropostaDBContext(DbContextOptions<PropostaDBContext> options) : base(options) { }

        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    if ((item.State == EntityState.Modified || item.State == EntityState.Added)
                        && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
                        item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

                    if (item.State == EntityState.Added)
                        if (item.Properties.Any(c => c.Metadata.Name == "DataCadastro") && item.Property("DataCadastro").CurrentValue.GetType() != typeof(DateTime))
                            item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                }
                var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return result;
            }
            catch (DbUpdateException e)
            {
                throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new InscricoesConfigurations());
            //modelBuilder.ApplyConfiguration(new TurmasConfigurations());
        }
    }
}
