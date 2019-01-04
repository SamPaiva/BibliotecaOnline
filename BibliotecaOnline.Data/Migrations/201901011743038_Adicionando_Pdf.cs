namespace BibliotecaOnline.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionando_Pdf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livro", "ConteudoAnexo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livro", "ConteudoAnexo");
        }
    }
}
