namespace BibliotecaOnline.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        LivroId = c.Long(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 150),
                        Sinopse = c.String(maxLength: 700),
                        Autor = c.String(maxLength: 150),
                        Genero = c.String(maxLength: 150),
                        DataLancamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LivroId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Livro");
        }
    }
}
