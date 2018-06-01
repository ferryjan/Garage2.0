namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedParkTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "CheckInTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "ParkTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ParkTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "CheckInTime");
        }
    }
}
