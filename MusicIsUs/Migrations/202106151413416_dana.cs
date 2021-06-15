namespace MusicIsUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dana : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        MadeInCountry = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Instruments_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instruments", t => t.Instruments_Id)
                .Index(t => t.Instruments_Id);
            
            CreateTable(
                "dbo.Vinyls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ArtistName = c.String(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        Genere = c.String(nullable: false),
                        OriginCountry = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VinylsUsers",
                c => new
                    {
                        Vinyls_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vinyls_Id, t.Users_Id })
                .ForeignKey("dbo.Vinyls", t => t.Vinyls_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Vinyls_Id)
                .Index(t => t.Users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Instruments_Id", "dbo.Instruments");
            DropForeignKey("dbo.VinylsUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.VinylsUsers", "Vinyls_Id", "dbo.Vinyls");
            DropIndex("dbo.VinylsUsers", new[] { "Users_Id" });
            DropIndex("dbo.VinylsUsers", new[] { "Vinyls_Id" });
            DropIndex("dbo.Users", new[] { "Instruments_Id" });
            DropTable("dbo.VinylsUsers");
            DropTable("dbo.Vinyls");
            DropTable("dbo.Users");
            DropTable("dbo.Instruments");
        }
    }
}
