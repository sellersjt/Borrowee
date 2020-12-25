namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemImageIdToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "ItemImageId", c => c.Int());
            CreateIndex("dbo.Item", "ItemImageId");
            AddForeignKey("dbo.Item", "ItemImageId", "dbo.ItemImage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "ItemImageId", "dbo.ItemImage");
            DropIndex("dbo.Item", new[] { "ItemImageId" });
            DropColumn("dbo.Item", "ItemImageId");
        }
    }
}
