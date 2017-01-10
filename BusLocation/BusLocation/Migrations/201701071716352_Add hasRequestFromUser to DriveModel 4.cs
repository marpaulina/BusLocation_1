namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "UserLat", c => c.Double(nullable: false));
            AddColumn("dbo.DriverModels", "UserLon", c => c.Double(nullable: false));
            DropColumn("dbo.DriverModels", "UserCity");
            DropColumn("dbo.DriverModels", "UserBusStopsNames");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverModels", "UserBusStopsNames", c => c.String());
            AddColumn("dbo.DriverModels", "UserCity", c => c.String());
            DropColumn("dbo.DriverModels", "UserLon");
            DropColumn("dbo.DriverModels", "UserLat");
        }
    }
}
