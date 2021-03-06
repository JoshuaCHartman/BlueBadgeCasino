namespace Casino.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NewDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Player", "AgeVerification");
        }

        public override void Down()
        {
            AddColumn("dbo.Player", "AgeVerification", c => c.Boolean(nullable: false));
        }
    }
}
