namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advanced : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ParkingSpaces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParkingSpaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
