namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeModelsTrackModels",
                c => new
                    {
                        TimeModels_Id = c.Int(nullable: false),
                        TrackModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeModels_Id, t.TrackModels_Id })
                .ForeignKey("dbo.TimeModels", t => t.TimeModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.TrackModels", t => t.TrackModels_Id, cascadeDelete: true)
                .Index(t => t.TimeModels_Id)
                .Index(t => t.TrackModels_Id);
            
            AddColumn("dbo.TrackModels", "TimeId", c => c.Int(nullable: false));
            DropColumn("dbo.TrackModels", "TimeToNext");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackModels", "TimeToNext", c => c.Int(nullable: false));
            DropForeignKey("dbo.TimeModelsTrackModels", "TrackModels_Id", "dbo.TrackModels");
            DropForeignKey("dbo.TimeModelsTrackModels", "TimeModels_Id", "dbo.TimeModels");
            DropIndex("dbo.TimeModelsTrackModels", new[] { "TrackModels_Id" });
            DropIndex("dbo.TimeModelsTrackModels", new[] { "TimeModels_Id" });
            DropColumn("dbo.TrackModels", "TimeId");
            DropTable("dbo.TimeModelsTrackModels");
            DropTable("dbo.TimeModels");
        }
    }
}
