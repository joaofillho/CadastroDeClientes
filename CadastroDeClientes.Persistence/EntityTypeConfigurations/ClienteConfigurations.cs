using CadastroDeClientes.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CadastroDeClientes.Persistence.EntityTypeConfigurations
{
    public class ClienteConfigurations:EntityTypeConfiguration<Cliente>
    {
        public ClienteConfigurations()
        {
            Property(x => x.Cpf)
                .HasColumnName("Cpf")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_CPF", 1) { IsUnique = true })).IsRequired();


            Property(x => x.DataNascimento)
                .HasColumnType("DateTime")
                .IsRequired();

            Property(x => x.Nome)
                .HasMaxLength(Cliente.NomeMaxValue).IsRequired();

        }
    }
}
