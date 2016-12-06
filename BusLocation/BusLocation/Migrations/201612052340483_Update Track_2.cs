namespace BusLocation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrack_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrackModels", "TimeToNext", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrackModels", "TimeToNext");
        }
    }
}
