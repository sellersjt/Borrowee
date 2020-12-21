namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueFileName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemImage", "FileName", c => c.String(maxLength: 100));
            CreateIndex("dbo.ItemImage", "FileName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ItemImage", new[] { "FileName" });
            AlterColumn("dbo.ItemImage", "FileName", c => c.String());
        }
    }
}
