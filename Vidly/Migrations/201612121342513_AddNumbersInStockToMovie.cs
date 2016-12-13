namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumbersInStockToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumbersInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumbersInStock");
        }
    }
}
