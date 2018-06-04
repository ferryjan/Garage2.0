namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "ParkingSpaceNum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "ParkingSpaceNum");
        }
    }
}
