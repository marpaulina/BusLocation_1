namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddlatlontoDriveModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "lat", c => c.Double(nullable: false));
            AddColumn("dbo.DriverModels", "lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverModels", "lon");
            DropColumn("dbo.DriverModels", "lat");
        }
    }
}
