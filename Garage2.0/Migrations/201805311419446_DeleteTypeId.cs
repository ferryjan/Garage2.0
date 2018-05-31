namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTypeId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vehicles", "TypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "TypeID", c => c.Int(nullable: false));
        }
    }
}
