namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.DriverModels", "Longitude", c => c.Double(nullable: false));
            DropColumn("dbo.DriverModels", "lat");
            DropColumn("dbo.DriverModels", "lon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverModels", "lon", c => c.Double(nullable: false));
            AddColumn("dbo.DriverModels", "lat", c => c.Double(nullable: false));
            DropColumn("dbo.DriverModels", "Longitude");
            DropColumn("dbo.DriverModels", "Latitude");
        }
    }
}
