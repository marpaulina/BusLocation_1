namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatetrack_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrackModels", "TimeToNext", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrackModels", "TimeToNext", c => c.Int(nullable: false));
        }
    }
}
