using CursoBackEnd.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoBackEnd.Infraestrutura.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tbl_Usuario");
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Codigo).ValueGeneratedOnAdd();

        }
    }
}
