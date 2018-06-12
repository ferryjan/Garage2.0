namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Garage25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNr = c.String(),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
            AddColumn("dbo.Vehicles", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "TypeId");
            CreateIndex("dbo.Vehicles", "MemberId");
            AddForeignKey("dbo.Vehicles", "MemberId", "dbo.Members", "MemberId", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "TypeId", "dbo.VehicleTypes", "TypeId", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "VehicleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "VehicleType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropColumn("dbo.Vehicles", "MemberId");
            DropColumn("dbo.Vehicles", "TypeId");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Members");
        }
    }
}
