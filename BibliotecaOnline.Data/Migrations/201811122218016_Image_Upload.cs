namespace BibliotecaOnline.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image_Upload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livro", "Imagem", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livro", "Imagem");
        }
    }
}
