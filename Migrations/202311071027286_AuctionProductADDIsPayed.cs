namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionProductADDIsPayed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionsProducts", "isPayed", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionsProducts", "isPayed");
        }
    }
}
