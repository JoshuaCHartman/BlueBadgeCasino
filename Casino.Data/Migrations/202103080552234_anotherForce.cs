namespace Casino.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anotherForce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "PlayerClosedAccount", c => c.Boolean(nullable: false));
            DropColumn("dbo.Player", "AgeVerification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "AgeVerification", c => c.Boolean(nullable: false));
            DropColumn("dbo.Player", "PlayerClosedAccount");
        }
    }
}
