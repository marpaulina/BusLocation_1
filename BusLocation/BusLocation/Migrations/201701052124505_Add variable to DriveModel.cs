namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddvariabletoDriveModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "BusStopID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverModels", "BusStopID");
        }
    }
}
