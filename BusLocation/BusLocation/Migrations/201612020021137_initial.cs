namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteModels", "Track_NameTrack", "dbo.TrackModels");
            DropForeignKey("dbo.BusStopModels", "TrackModels_NameTrack", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "TrackModels_NameTrack" });
            DropIndex("dbo.RouteModels", new[] { "Track_NameTrack" });
            RenameColumn(table: "dbo.BusStopModels", name: "TrackModels_NameTrack", newName: "TrackModels_Id");
            DropPrimaryKey("dbo.TrackModels");
            AddColumn("dbo.TrackModels", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BusStopModels", "TrackModels_Id", c => c.Int());
            AlterColumn("dbo.TrackModels", "NameTrack", c => c.String(nullable: false));
            AddPrimaryKey("dbo.TrackModels", "Id");
            CreateIndex("dbo.BusStopModels", "TrackModels_Id");
            AddForeignKey("dbo.BusStopModels", "TrackModels_Id", "dbo.TrackModels", "Id");
            DropColumn("dbo.RouteModels", "Track_NameTrack");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RouteModels", "Track_NameTrack", c => c.String(maxLength: 128));
            DropForeignKey("dbo.BusStopModels", "TrackModels_Id", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "TrackModels_Id" });
            DropPrimaryKey("dbo.TrackModels");
            AlterColumn("dbo.TrackModels", "NameTrack", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BusStopModels", "TrackModels_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.TrackModels", "Id");
            AddPrimaryKey("dbo.TrackModels", "NameTrack");
            RenameColumn(table: "dbo.BusStopModels", name: "TrackModels_Id", newName: "TrackModels_NameTrack");
            CreateIndex("dbo.RouteModels", "Track_NameTrack");
            CreateIndex("dbo.BusStopModels", "TrackModels_NameTrack");
            AddForeignKey("dbo.BusStopModels", "TrackModels_NameTrack", "dbo.TrackModels", "NameTrack");
            AddForeignKey("dbo.RouteModels", "Track_NameTrack", "dbo.TrackModels", "NameTrack");
        }
    }
}
