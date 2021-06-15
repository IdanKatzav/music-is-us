namespace MusicIsUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dana1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vinyls", "Genere", c => c.String(nullable: false));
            DropColumn("dbo.Vinyls", "Genere");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vinyls", "Genere", c => c.String(nullable: false));
            DropColumn("dbo.Vinyls", "Genere");
        }
    }
}
