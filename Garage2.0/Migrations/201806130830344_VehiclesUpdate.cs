namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehiclesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            RenameColumn(table: "dbo.Vehicles", name: "MemberId", newName: "Member_MemberId");
            AddColumn("dbo.Vehicles", "MembershipNr", c => c.String(nullable: false));
            AlterColumn("dbo.Vehicles", "Member_MemberId", c => c.Int());
            CreateIndex("dbo.Vehicles", "Member_MemberId");
            AddForeignKey("dbo.Vehicles", "Member_MemberId", "dbo.Members", "MemberId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Member_MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "Member_MemberId" });
            AlterColumn("dbo.Vehicles", "Member_MemberId", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "MembershipNr");
            RenameColumn(table: "dbo.Vehicles", name: "Member_MemberId", newName: "MemberId");
            CreateIndex("dbo.Vehicles", "MemberId");
            AddForeignKey("dbo.Vehicles", "MemberId", "dbo.Members", "MemberId", cascadeDelete: true);
        }
    }
}
