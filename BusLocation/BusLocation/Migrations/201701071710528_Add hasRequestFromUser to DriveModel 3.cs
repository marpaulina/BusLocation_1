namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "UserCity", c => c.String());
            AddColumn("dbo.DriverModels", "UserBusStopsNames", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverModels", "UserBusStopsNames");
            DropColumn("dbo.DriverModels", "UserCity");
        }
    }
}
