namespace Casino.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NeedToManuallyAddBecauseDataLossWarning : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bet", "PlayerWonGame");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bet", "PlayerWonGame", c => c.Boolean(nullable: false));
        }
    }
}
