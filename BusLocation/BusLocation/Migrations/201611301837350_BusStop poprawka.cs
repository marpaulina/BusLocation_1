namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusStoppoprawka : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "Name" });
            AlterColumn("dbo.BusStopModels", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BusStopModels", "City", c => c.String(nullable: false));
            CreateIndex("dbo.BusStopModels", "Name");
            AddForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels", "Name", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "Name" });
            AlterColumn("dbo.BusStopModels", "City", c => c.String());
            AlterColumn("dbo.BusStopModels", "Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.BusStopModels", "Name");
            AddForeignKey("dbo.BusStopModels", "Name", "dbo.TrackModels", "Name");
        }
    }
}
