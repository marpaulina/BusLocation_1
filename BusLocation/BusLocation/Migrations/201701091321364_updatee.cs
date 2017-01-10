namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequestModels", "TimeAddRequest", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequestModels", "TimeAddRequest");
        }
    }
}
