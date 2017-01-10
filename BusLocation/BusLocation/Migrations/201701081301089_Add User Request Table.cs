namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRequestTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequestModels", "DriversGetRequest", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRequestModels", "Message", c => c.String());
            AddColumn("dbo.UserRequestModels", "Driver_Id", c => c.Int());
            CreateIndex("dbo.UserRequestModels", "Driver_Id");
            AddForeignKey("dbo.UserRequestModels", "Driver_Id", "dbo.DriverModels", "Id");
            DropColumn("dbo.DriverModels", "HasRequestFromUser");
            DropColumn("dbo.UserRequestModels", "RouteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequestModels", "RouteID", c => c.Int(nullable: false));
            AddColumn("dbo.DriverModels", "HasRequestFromUser", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.UserRequestModels", "Driver_Id", "dbo.DriverModels");
            DropIndex("dbo.UserRequestModels", new[] { "Driver_Id" });
            DropColumn("dbo.UserRequestModels", "Driver_Id");
            DropColumn("dbo.UserRequestModels", "Message");
            DropColumn("dbo.UserRequestModels", "DriversGetRequest");
        }
    }
}
