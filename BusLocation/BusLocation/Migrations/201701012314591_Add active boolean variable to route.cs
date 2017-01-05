namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addactivebooleanvariabletoroute : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BusStopModelsTrackModels", newName: "TrackModelsBusStopModels");
            DropForeignKey("dbo.ActiveRouteModels", "TrackId", "dbo.TrackModels");
            DropIndex("dbo.ActiveRouteModels", new[] { "TrackId" });
            DropPrimaryKey("dbo.TrackModelsBusStopModels");
            AddColumn("dbo.RouteModels", "active", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.TrackModelsBusStopModels", new[] { "TrackModels_Id", "BusStopModels_Id" });
            DropTable("dbo.ActiveRouteModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActiveRouteModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackId = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            DropPrimaryKey("dbo.TrackModelsBusStopModels");
            DropColumn("dbo.RouteModels", "active");
            AddPrimaryKey("dbo.TrackModelsBusStopModels", new[] { "BusStopModels_Id", "TrackModels_Id" });
            CreateIndex("dbo.ActiveRouteModels", "TrackId");
            AddForeignKey("dbo.ActiveRouteModels", "TrackId", "dbo.TrackModels", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.TrackModelsBusStopModels", newName: "BusStopModelsTrackModels");
        }
    }
}
