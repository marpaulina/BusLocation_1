namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusStoppoprawka5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels");
            DropForeignKey("dbo.RouteModels", "Track_Name", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "Name" });
            RenameColumn(table: "dbo.RouteModels", name: "Track_Name", newName: "Track_NameTrack");
            RenameIndex(table: "dbo.RouteModels", name: "IX_Track_Name", newName: "IX_Track_NameTrack");
            DropPrimaryKey("dbo.TrackModels");
            AddColumn("dbo.BusStopModels", "TrackModels_NameTrack", c => c.String(maxLength: 128));
            AddColumn("dbo.TrackModels", "NameTrack", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BusStopModels", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.TrackModels", "NameTrack");
            CreateIndex("dbo.BusStopModels", "TrackModels_NameTrack");
            AddForeignKey("dbo.BusStopModels", "TrackModels_NameTrack", "dbo.TrackModels", "NameTrack");
            AddForeignKey("dbo.RouteModels", "Track_NameTrack", "dbo.TrackModels", "NameTrack");
            DropColumn("dbo.TrackModels", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackModels", "Name", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.RouteModels", "Track_NameTrack", "dbo.TrackModels");
            DropForeignKey("dbo.BusStopModels", "TrackModels_NameTrack", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "TrackModels_NameTrack" });
            DropPrimaryKey("dbo.TrackModels");
            AlterColumn("dbo.BusStopModels", "Name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.TrackModels", "NameTrack");
            DropColumn("dbo.BusStopModels", "TrackModels_NameTrack");
            AddPrimaryKey("dbo.TrackModels", "Name");
            RenameIndex(table: "dbo.RouteModels", name: "IX_Track_NameTrack", newName: "IX_Track_Name");
            RenameColumn(table: "dbo.RouteModels", name: "Track_NameTrack", newName: "Track_Name");
            CreateIndex("dbo.BusStopModels", "Name");
            AddForeignKey("dbo.RouteModels", "Track_Name", "dbo.TrackModels", "Name");
            AddForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels", "Name", cascadeDelete: true);
        }
    }
}
