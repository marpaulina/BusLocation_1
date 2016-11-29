namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusStopModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        City = c.String(),
                        Latitiude = c.Double(nullable: false),
                        Longitiude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrackModels", t => t.Name)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.DriverModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Nick = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(nullable: false, precision: 7),
                        Track_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrackModels", t => t.Track_Name)
                .Index(t => t.Track_Name);
            
            CreateTable(
                "dbo.TrackModels",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteModels", "Track_Name", "dbo.TrackModels");
            DropForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels");
            DropIndex("dbo.RouteModels", new[] { "Track_Name" });
            DropIndex("dbo.BusStopModels", new[] { "Name" });
            DropTable("dbo.TrackModels");
            DropTable("dbo.RouteModels");
            DropTable("dbo.DriverModels");
            DropTable("dbo.BusStopModels");
        }
    }
}
