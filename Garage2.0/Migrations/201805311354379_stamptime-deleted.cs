namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stamptimedeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vehicles", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Timestamp", c => c.DateTime(nullable: false));
        }
    }
}
