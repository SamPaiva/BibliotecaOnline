using BibliotecaOnline.Data.Mapping;
using BibliotecaOnline.Data.Migrations;
using Modelo.Modelo;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BibliotecaOnline.Data.Context
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new LivroMap());

            base.OnModelCreating(modelBuilder);
        }

        public BibliotecaContext() : base("Biblioteca")
        {
            Database.SetInitializer<BibliotecaContext>(new MigrateDatabaseToLatestVersion<BibliotecaContext, Configuration>());
        }

        public DbSet<Livro> Livros { get; set; }
    }
}
