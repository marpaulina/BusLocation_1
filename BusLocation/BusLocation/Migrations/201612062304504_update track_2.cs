namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetrack_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusStopModels", "TrackModels_Id", "dbo.TrackModels");
            DropIndex("dbo.BusStopModels", new[] { "TrackModels_Id" });
            CreateTable(
                "dbo.TrackModelsBusStopModels",
                c => new
                    {
                        TrackModels_Id = c.Int(nullable: false),
                        BusStopModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackModels_Id, t.BusStopModels_Id })
                .ForeignKey("dbo.TrackModels", t => t.TrackModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusStopModels", t => t.BusStopModels_Id, cascadeDelete: true)
                .Index(t => t.TrackModels_Id)
                .Index(t => t.BusStopModels_Id);
            
            DropColumn("dbo.BusStopModels", "TrackModels_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BusStopModels", "TrackModels_Id", c => c.Int());
            DropForeignKey("dbo.TrackModelsBusStopModels", "BusStopModels_Id", "dbo.BusStopModels");
            DropForeignKey("dbo.TrackModelsBusStopModels", "TrackModels_Id", "dbo.TrackModels");
            DropIndex("dbo.TrackModelsBusStopModels", new[] { "BusStopModels_Id" });
            DropIndex("dbo.TrackModelsBusStopModels", new[] { "TrackModels_Id" });
            DropTable("dbo.TrackModelsBusStopModels");
            CreateIndex("dbo.BusStopModels", "TrackModels_Id");
            AddForeignKey("dbo.BusStopModels", "TrackModels_Id", "dbo.TrackModels", "Id");
        }
    }
}
