namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isPayedNOTnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuctionsProducts", "isPayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuctionsProducts", "isPayed", c => c.Boolean());
        }
    }
}
