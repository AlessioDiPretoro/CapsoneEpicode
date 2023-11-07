namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionProductADDIdAuctionWinner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionsProducts", "idAuctionWinner", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionsProducts", "idAuctionWinner");
        }
    }
}
