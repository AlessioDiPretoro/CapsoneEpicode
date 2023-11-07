namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionProductADDendPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionsProducts", "endPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionsProducts", "endPrice");
        }
    }
}
