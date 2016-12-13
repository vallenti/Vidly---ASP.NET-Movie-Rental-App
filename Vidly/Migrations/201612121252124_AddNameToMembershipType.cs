namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNameToMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String());
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Pay as You Go' WHERE Id = 1");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Monthly' WHERE Id = 2");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Annualy' WHERE Id = 4");
        }

        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
