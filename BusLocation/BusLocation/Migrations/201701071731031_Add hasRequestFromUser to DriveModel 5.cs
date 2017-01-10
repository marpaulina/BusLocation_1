namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "UserCity", c => c.String());
            AddColumn("dbo.DriverModels", "UserBusStopName", c => c.String());
            DropColumn("dbo.DriverModels", "UserLat");
            DropColumn("dbo.DriverModels", "UserLon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverModels", "UserLon", c => c.Double(nullable: false));
            AddColumn("dbo.DriverModels", "UserLat", c => c.Double(nullable: false));
            DropColumn("dbo.DriverModels", "UserBusStopName");
            DropColumn("dbo.DriverModels", "UserCity");
        }
    }
}
