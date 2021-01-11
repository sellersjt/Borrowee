namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemImageToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "ItemImage_Id", c => c.Int());
            CreateIndex("dbo.Transaction", "ItemImage_Id");
            AddForeignKey("dbo.Transaction", "ItemImage_Id", "dbo.ItemImage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "ItemImage_Id", "dbo.ItemImage");
            DropIndex("dbo.Transaction", new[] { "ItemImage_Id" });
            DropColumn("dbo.Transaction", "ItemImage_Id");
        }
    }
}
