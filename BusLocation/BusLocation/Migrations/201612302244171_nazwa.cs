namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nazwa : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrackModelsBusStopModels", newName: "BusStopModelsTrackModels");
            DropPrimaryKey("dbo.BusStopModelsTrackModels");
            CreateTable(
                "dbo.ActiveRouteModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackId = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrackModels", t => t.TrackId, cascadeDelete: true)
                .Index(t => t.TrackId);
            
            AddPrimaryKey("dbo.BusStopModelsTrackModels", new[] { "BusStopModels_Id", "TrackModels_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiveRouteModels", "TrackId", "dbo.TrackModels");
            DropIndex("dbo.ActiveRouteModels", new[] { "TrackId" });
            DropPrimaryKey("dbo.BusStopModelsTrackModels");
            DropTable("dbo.ActiveRouteModels");
            AddPrimaryKey("dbo.BusStopModelsTrackModels", new[] { "TrackModels_Id", "BusStopModels_Id" });
            RenameTable(name: "dbo.BusStopModelsTrackModels", newName: "TrackModelsBusStopModels");
        }
    }
}
