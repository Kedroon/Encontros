namespace Encontros.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudarDescricaoParaNomeEncontro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Encontroes", "NomeEncontro", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Encontroes", "Descricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Encontroes", "Descricao", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Encontroes", "NomeEncontro");
        }
    }
}
