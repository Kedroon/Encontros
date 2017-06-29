namespace Encontros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Encontroes", "Local_Id", "dbo.Locals");
            DropIndex("dbo.Encontroes", new[] { "Local_Id" });
            DropTable("dbo.Encontroes");
            DropTable("dbo.Locals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Locals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Encontroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 255),
                        DataDoEncontro = c.DateTime(nullable: false),
                        LocalId = c.Byte(nullable: false),
                        Local_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Encontroes", "Local_Id");
            AddForeignKey("dbo.Encontroes", "Local_Id", "dbo.Locals", "Id");
        }
    }
}
