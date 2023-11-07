namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CAP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "cap", c => c.String(maxLength: 5));
            AddColumn("dbo.Users", "cap", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "cap");
            DropColumn("dbo.Order", "cap");
        }
    }
}
