namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhasRequestFromUsertoDriveModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverModels", "hasRequestFromUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DriverModels", "hasRequestFromUser");
        }
    }
}
