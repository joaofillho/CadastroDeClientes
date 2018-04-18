using CadastroDeClientes.Domain.Entities;
using CadastroDeClientes.Persistence.EntityTypeConfigurations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CadastroDeClientes.Persistence.Context
{
    public class DbCLientesContexto:DbContext
    {
        public DbCLientesContexto()
            : base("dbClientes")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Cliente> Clientes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(50));

            modelBuilder.Configurations.Add(new ClienteConfigurations());


        }

    }
}
