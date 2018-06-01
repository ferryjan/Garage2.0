namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.Int(nullable: false),
                        RegNum = c.String(nullable: false, maxLength: 6),
                        Color = c.Int(),
                        ParkTime = c.DateTime(nullable: false),
                        NumOfTires = c.Int(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
