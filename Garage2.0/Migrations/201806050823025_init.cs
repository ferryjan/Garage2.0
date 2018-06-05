namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingSpaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.Int(nullable: false),
                        RegNum = c.String(nullable: false, maxLength: 6),
                        Color = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        NumOfTires = c.Int(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleParkingSpaces", "ParkingSpace_Id", "dbo.ParkingSpaces");
            DropForeignKey("dbo.VehicleParkingSpaces", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleParkingSpaces", new[] { "ParkingSpace_Id" });
            DropIndex("dbo.VehicleParkingSpaces", new[] { "Vehicle_Id" });
            DropTable("dbo.VehicleParkingSpaces");
            DropTable("dbo.Vehicles");
            DropTable("dbo.ParkingSpaces");
        }
    }
}
