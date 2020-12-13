namespace Borrowee.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionTweek : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transaction", "ReturnDateUtc", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "ReturnDateUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
