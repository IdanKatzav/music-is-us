namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Street", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Street", c => c.Int(nullable: false));
        }
    }
}
