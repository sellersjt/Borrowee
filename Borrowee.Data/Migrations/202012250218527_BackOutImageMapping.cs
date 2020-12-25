namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackOutImageMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemImageMapping", "ItemId", "dbo.Item");
            DropForeignKey("dbo.ItemImageMapping", "ItemImageId", "dbo.ItemImage");
            DropIndex("dbo.ItemImageMapping", new[] { "ItemId" });
            DropIndex("dbo.ItemImageMapping", new[] { "ItemImageId" });
            DropTable("dbo.ItemImageMapping");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ItemImageMapping", "ItemImageId");
            CreateIndex("dbo.ItemImageMapping", "ItemId");
            AddForeignKey("dbo.ItemImageMapping", "ItemImageId", "dbo.ItemImage", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemImageMapping", "ItemId", "dbo.Item", "Id", cascadeDelete: true);
        }
    }
}
