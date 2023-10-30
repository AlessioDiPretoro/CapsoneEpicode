namespace Stones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionsDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idAuction = c.Int(nullable: false),
                        idUser = c.Int(nullable: false),
                        data = c.DateTime(nullable: false),
                        price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AuctionsProducts", t => t.idAuction)
                .ForeignKey("dbo.Users", t => t.idUser)
                .Index(t => t.idAuction)
                .Index(t => t.idUser);
            
            CreateTable(
                "dbo.AuctionsProducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idProduct = c.Int(nullable: false),
                        dataStart = c.DateTime(nullable: false),
                        dataEnd = c.DateTime(nullable: false),
                        startPrice = c.Decimal(nullable: false, storeType: "money"),
                        isActive = c.Boolean(nullable: false),
                        idWinner = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Product", t => t.idProduct)
                .ForeignKey("dbo.Users", t => t.idWinner)
                .Index(t => t.idProduct)
                .Index(t => t.idWinner);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30),
                        price = c.Decimal(nullable: false, storeType: "money"),
                        descripton = c.String(),
                        idCategory = c.Int(),
                        idSubject = c.Int(),
                        photo1 = c.String(),
                        photo2 = c.String(),
                        photo3 = c.String(),
                        photo4 = c.String(),
                        photo5 = c.String(),
                        isAvaiable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ProductCategory", t => t.idCategory)
                .ForeignKey("dbo.ProductSubject", t => t.idSubject)
                .Index(t => t.idCategory)
                .Index(t => t.idSubject);
            
            CreateTable(
                "dbo.DetailOrder",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idOrder = c.Int(nullable: false),
                        idProduct = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        quanty = c.Int(nullable: false),
                        priceCad = c.Decimal(nullable: false, storeType: "money"),
                        state = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Order", t => t.idOrder)
                .ForeignKey("dbo.Product", t => t.idProduct)
                .Index(t => t.idOrder)
                .Index(t => t.idProduct);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idBuyer = c.Int(nullable: false),
                        address = c.String(maxLength: 50),
                        city = c.String(maxLength: 50),
                        prov = c.String(maxLength: 10, fixedLength: true),
                        phone = c.String(maxLength: 50),
                        state = c.String(nullable: false, maxLength: 50),
                        date = c.DateTime(nullable: false),
                        notes = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.idBuyer)
                .Index(t => t.idBuyer);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 50),
                        surname = c.String(nullable: false, maxLength: 50),
                        address = c.String(maxLength: 50),
                        city = c.String(maxLength: 50),
                        prov = c.String(maxLength: 2),
                        phone = c.String(maxLength: 20),
                        imgProfile = c.String(maxLength: 50),
                        role = c.String(maxLength: 50),
                        points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idUser = c.Int(nullable: false),
                        idProduct = c.Int(),
                        isActive = c.Boolean(nullable: false),
                        body = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                        dateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.idUser)
                .ForeignKey("dbo.Product", t => t.idProduct)
                .Index(t => t.idUser)
                .Index(t => t.idProduct);
            
            CreateTable(
                "dbo.PostResponse",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idPost = c.Int(nullable: false),
                        body = c.String(nullable: false),
                        idUser = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        date = c.DateTime(nullable: false),
                        dateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Post", t => t.idPost)
                .ForeignKey("dbo.Users", t => t.idUser)
                .Index(t => t.idPost)
                .Index(t => t.idUser);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductSubject",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "idSubject", "dbo.ProductSubject");
            DropForeignKey("dbo.Product", "idCategory", "dbo.ProductCategory");
            DropForeignKey("dbo.Post", "idProduct", "dbo.Product");
            DropForeignKey("dbo.DetailOrder", "idProduct", "dbo.Product");
            DropForeignKey("dbo.PostResponse", "idUser", "dbo.Users");
            DropForeignKey("dbo.Post", "idUser", "dbo.Users");
            DropForeignKey("dbo.PostResponse", "idPost", "dbo.Post");
            DropForeignKey("dbo.Order", "idBuyer", "dbo.Users");
            DropForeignKey("dbo.AuctionsProducts", "idWinner", "dbo.Users");
            DropForeignKey("dbo.AuctionsDetails", "idUser", "dbo.Users");
            DropForeignKey("dbo.DetailOrder", "idOrder", "dbo.Order");
            DropForeignKey("dbo.AuctionsProducts", "idProduct", "dbo.Product");
            DropForeignKey("dbo.AuctionsDetails", "idAuction", "dbo.AuctionsProducts");
            DropIndex("dbo.PostResponse", new[] { "idUser" });
            DropIndex("dbo.PostResponse", new[] { "idPost" });
            DropIndex("dbo.Post", new[] { "idProduct" });
            DropIndex("dbo.Post", new[] { "idUser" });
            DropIndex("dbo.Order", new[] { "idBuyer" });
            DropIndex("dbo.DetailOrder", new[] { "idProduct" });
            DropIndex("dbo.DetailOrder", new[] { "idOrder" });
            DropIndex("dbo.Product", new[] { "idSubject" });
            DropIndex("dbo.Product", new[] { "idCategory" });
            DropIndex("dbo.AuctionsProducts", new[] { "idWinner" });
            DropIndex("dbo.AuctionsProducts", new[] { "idProduct" });
            DropIndex("dbo.AuctionsDetails", new[] { "idUser" });
            DropIndex("dbo.AuctionsDetails", new[] { "idAuction" });
            DropTable("dbo.ProductSubject");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.PostResponse");
            DropTable("dbo.Post");
            DropTable("dbo.Users");
            DropTable("dbo.Order");
            DropTable("dbo.DetailOrder");
            DropTable("dbo.Product");
            DropTable("dbo.AuctionsProducts");
            DropTable("dbo.AuctionsDetails");
        }
    }
}
