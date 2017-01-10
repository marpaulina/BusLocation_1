namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequestModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteID = c.Int(nullable: false),
                        UserCity = c.String(),
                        UserBusStopName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.DriverModels", "UserCity");
            DropColumn("dbo.DriverModels", "UserBusStopName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DriverModels", "UserBusStopName", c => c.String());
            AddColumn("dbo.DriverModels", "UserCity", c => c.String());
            DropTable("dbo.UserRequestModels");
        }
    }
}
