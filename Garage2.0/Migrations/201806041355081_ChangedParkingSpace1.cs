namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedParkingSpace1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vehicles", "ParkingSpaceNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ParkingSpaceNum", c => c.Int(nullable: false));
        }
    }
}
