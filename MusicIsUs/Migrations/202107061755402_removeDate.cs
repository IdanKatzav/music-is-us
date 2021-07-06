namespace MusicIsUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vinyls", "PublishDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vinyls", "PublishDate", c => c.DateTime(nullable: false));
        }
    }
}
