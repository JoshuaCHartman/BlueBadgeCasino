namespace Casino.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class After1237push : DbMigration
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
