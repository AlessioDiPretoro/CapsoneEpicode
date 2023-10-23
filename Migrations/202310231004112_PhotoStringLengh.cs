namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoStringLengh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "photo1", c => c.String());
            AlterColumn("dbo.Product", "photo2", c => c.String());
            AlterColumn("dbo.Product", "photo3", c => c.String());
            AlterColumn("dbo.Product", "photo4", c => c.String());
            AlterColumn("dbo.Product", "photo5", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "photo5", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "photo4", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "photo3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "photo2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "photo1", c => c.String(maxLength: 50));
        }
    }
}
