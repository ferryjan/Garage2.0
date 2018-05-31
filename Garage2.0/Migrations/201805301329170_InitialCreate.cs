namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up() {
            CreateTable(
                "dbo.Vehicles",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    TypeID = c.Int(nullable: false),
                    VehicleType = c.Int(nullable: false),
                    RegNum = c.String(nullable: false, maxLength: 6),
                    Color = c.String(nullable: false),
                    ParkTime = c.DateTime(nullable: false),
                    NumOfTires = c.Int(nullable: false),
                    Model = c.String(nullable: false, maxLength: 30),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down() {
            DropTable("dbo.Vehicles");
        }
    }
}
