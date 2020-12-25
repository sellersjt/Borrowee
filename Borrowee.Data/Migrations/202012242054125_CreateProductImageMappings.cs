namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductImageMappings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemImageMapping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.ItemImage", t => t.ItemImageId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.ItemImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemImageMapping", "ItemImageId", "dbo.ItemImage");
            DropForeignKey("dbo.ItemImageMapping", "ItemId", "dbo.Item");
            DropIndex("dbo.ItemImageMapping", new[] { "ItemImageId" });
            DropIndex("dbo.ItemImageMapping", new[] { "ItemId" });
            DropTable("dbo.ItemImageMapping");
        }
    }
}
