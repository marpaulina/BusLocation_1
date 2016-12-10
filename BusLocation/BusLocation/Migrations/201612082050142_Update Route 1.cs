namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoute1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteModels", "Track_Id", "dbo.TrackModels");
            DropIndex("dbo.RouteModels", new[] { "Track_Id" });
            RenameColumn(table: "dbo.RouteModels", name: "Track_Id", newName: "TrackId");
            AlterColumn("dbo.RouteModels", "TrackId", c => c.Int(nullable: false));
            CreateIndex("dbo.RouteModels", "TrackId");
            AddForeignKey("dbo.RouteModels", "TrackId", "dbo.TrackModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteModels", "TrackId", "dbo.TrackModels");
            DropIndex("dbo.RouteModels", new[] { "TrackId" });
            AlterColumn("dbo.RouteModels", "TrackId", c => c.Int());
            RenameColumn(table: "dbo.RouteModels", name: "TrackId", newName: "Track_Id");
            CreateIndex("dbo.RouteModels", "Track_Id");
            AddForeignKey("dbo.RouteModels", "Track_Id", "dbo.TrackModels", "Id");
        }
    }
}
