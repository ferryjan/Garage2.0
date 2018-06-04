namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedParkingSpace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkingSpaces", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkingSpaces", "Number");
        }
    }
}
