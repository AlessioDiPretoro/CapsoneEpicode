namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStateNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "state", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "state", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
