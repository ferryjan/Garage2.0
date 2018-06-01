namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingnonnullableforcolor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "Color", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Color", c => c.Int());
        }
    }
}
