using Modelo.Modelo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Data.Mapping
{
    public class LivroMap : EntityTypeConfiguration<Livro>
    {
        public LivroMap()
        {
            ToTable("Livro");

            Property(c => c.LivroId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Autor).HasMaxLength(150);

            Property(c => c.Autor);

            Property(c => c.Imagem).IsMaxLength();

            Property(c => c.Genero).HasMaxLength(150);

            Property(c => c.Sinopse).HasMaxLength(700);

            Property(c => c.Titulo).HasMaxLength(150);
        }
    }
}
