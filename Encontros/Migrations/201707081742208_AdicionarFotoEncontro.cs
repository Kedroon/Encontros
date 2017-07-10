namespace Encontros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarFotoEncontro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FotoEncontroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Length = c.Int(nullable: false),
                        Type = c.String(),
                        ImageUrl = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Encontro_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Encontroes", t => t.Encontro_Id)
                .Index(t => t.Encontro_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FotoEncontroes", "Encontro_Id", "dbo.Encontroes");
            DropIndex("dbo.FotoEncontroes", new[] { "Encontro_Id" });
            DropTable("dbo.FotoEncontroes");
        }
    }
}
