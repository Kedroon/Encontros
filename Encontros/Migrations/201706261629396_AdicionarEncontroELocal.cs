namespace Encontros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarEncontroELocal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Encontroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 255),
                        DataDoEncontro = c.DateTime(nullable: false),
                        LocalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locals", t => t.LocalId, cascadeDelete: true)
                .Index(t => t.LocalId);
            
            CreateTable(
                "dbo.Locals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Encontroes", "LocalId", "dbo.Locals");
            DropIndex("dbo.Encontroes", new[] { "LocalId" });
            DropTable("dbo.Locals");
            DropTable("dbo.Encontroes");
        }
    }
}
