namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addvalidation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusStopModels", "Latitiude", c => c.Single(nullable: false));
            AlterColumn("dbo.BusStopModels", "Longitiude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusStopModels", "Longitiude", c => c.Double(nullable: false));
            AlterColumn("dbo.BusStopModels", "Latitiude", c => c.Double(nullable: false));
        }
    }
}
