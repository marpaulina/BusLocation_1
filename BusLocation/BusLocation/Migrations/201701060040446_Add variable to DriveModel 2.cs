namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddvariabletoDriveModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "timeFromBusStop", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverModels", "timeFromBusStop");
        }
    }
}
