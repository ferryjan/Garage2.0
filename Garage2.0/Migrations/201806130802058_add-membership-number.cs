namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmembershipnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "MembershipNr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "MembershipNr");
        }
    }
}
