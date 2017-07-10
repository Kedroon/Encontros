namespace Encontros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FotoEncontroes", "Encontro_Id", "dbo.Encontroes");
            DropIndex("dbo.FotoEncontroes", new[] { "Encontro_Id" });
            RenameColumn(table: "dbo.FotoEncontroes", name: "Encontro_Id", newName: "EncontroId");
            AlterColumn("dbo.FotoEncontroes", "EncontroId", c => c.Int(nullable: false));
            CreateIndex("dbo.FotoEncontroes", "EncontroId");
            AddForeignKey("dbo.FotoEncontroes", "EncontroId", "dbo.Encontroes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FotoEncontroes", "EncontroId", "dbo.Encontroes");
            DropIndex("dbo.FotoEncontroes", new[] { "EncontroId" });
            AlterColumn("dbo.FotoEncontroes", "EncontroId", c => c.Int());
            RenameColumn(table: "dbo.FotoEncontroes", name: "EncontroId", newName: "Encontro_Id");
            CreateIndex("dbo.FotoEncontroes", "Encontro_Id");
            AddForeignKey("dbo.FotoEncontroes", "Encontro_Id", "dbo.Encontroes", "Id");
        }
    }
}
