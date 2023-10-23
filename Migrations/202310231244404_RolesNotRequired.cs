namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "role", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "role", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
