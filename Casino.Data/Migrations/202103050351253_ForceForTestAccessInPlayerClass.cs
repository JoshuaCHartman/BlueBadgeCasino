namespace Casino.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForceForTestAccessInPlayerClass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Player", "HasAccessToHighLevelGame");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "HasAccessToHighLevelGame", c => c.Boolean(nullable: false));
        }
    }
}
