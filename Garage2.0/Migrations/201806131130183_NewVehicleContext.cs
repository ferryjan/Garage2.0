namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewVehicleContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "MembershipNr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "MembershipNr");
        }
    }
}
