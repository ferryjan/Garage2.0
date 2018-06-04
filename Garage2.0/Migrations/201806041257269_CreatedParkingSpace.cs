namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedParkingSpace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "ParkingSpace_Id", "dbo.ParkingSpaces");
            DropIndex("dbo.Vehicles", new[] { "ParkingSpace_Id" });
            CreateTable(
                "dbo.VehicleParkingSpaces",
                c => new
                    {
                        Vehicle_Id = c.Int(nullable: false),
                        ParkingSpace_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_Id, t.ParkingSpace_Id })
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id, cascadeDelete: true)
                .ForeignKey("dbo.ParkingSpaces", t => t.ParkingSpace_Id, cascadeDelete: true)
                .Index(t => t.Vehicle_Id)
                .Index(t => t.ParkingSpace_Id);
            
            AddColumn("dbo.Vehicles", "ParkingSpaceNum", c => c.Int(nullable: false));
            DropColumn("dbo.ParkingSpaces", "Number");
            DropColumn("dbo.Vehicles", "ParkingSpace_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ParkingSpace_Id", c => c.Int());
            AddColumn("dbo.ParkingSpaces", "Number", c => c.Int(nullable: false));
            DropForeignKey("dbo.VehicleParkingSpaces", "ParkingSpace_Id", "dbo.ParkingSpaces");
            DropForeignKey("dbo.VehicleParkingSpaces", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleParkingSpaces", new[] { "ParkingSpace_Id" });
            DropIndex("dbo.VehicleParkingSpaces", new[] { "Vehicle_Id" });
            DropColumn("dbo.Vehicles", "ParkingSpaceNum");
            DropTable("dbo.VehicleParkingSpaces");
            CreateIndex("dbo.Vehicles", "ParkingSpace_Id");
            AddForeignKey("dbo.Vehicles", "ParkingSpace_Id", "dbo.ParkingSpaces", "Id");
        }
    }
}
