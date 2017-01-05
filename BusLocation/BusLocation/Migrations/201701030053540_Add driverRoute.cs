namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddriverRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "RouteID", c => c.Int(nullable: false));
            AddColumn("dbo.RouteModels", "DriverID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteModels", "DriverID");
            DropColumn("dbo.DriverModels", "RouteID");
        }
    }
}
