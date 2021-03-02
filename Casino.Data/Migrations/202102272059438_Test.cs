namespace Casino.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankTransaction",
                c => new
                    {
                        BankTransactionId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Guid(nullable: false),
                        DateTimeOfTransaction = c.DateTimeOffset(nullable: false, precision: 7),
                        BankTransactionAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BankTransactionId)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        PlayerFirstName = c.String(nullable: false),
                        PlayerLastName = c.String(nullable: false),
                        PlayerPhone = c.String(),
                        PlayerEmail = c.String(nullable: false),
                        PlayerAddress = c.String(),
                        PlayerState = c.Int(nullable: false),
                        PlayerDob = c.String(nullable: false),
                        AccountCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        TierStatus = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        HasAccessToHighLevelGame = c.Boolean(nullable: false),
                        CurrentBankBalance = c.Double(nullable: false),
                        AgeVerification = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.Bet",
                c => new
                    {
                        BetId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Guid(nullable: false),
                        GameId = c.Int(nullable: false),
                        BetAmount = c.Double(nullable: false),
                        PayoutAmount = c.Double(nullable: false),
                        PlayerWonGame = c.Boolean(nullable: false),
                        DateTimeOfBet = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.BetId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        TypeOfGame = c.Int(nullable: false),
                        IsHighStakes = c.Boolean(nullable: false),
                        MinBet = c.Double(nullable: false),
                        MaxBet = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.BankTransaction", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Bet", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Bet", "GameId", "dbo.Game");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Bet", new[] { "GameId" });
            DropIndex("dbo.Bet", new[] { "PlayerId" });
            DropIndex("dbo.BankTransaction", new[] { "PlayerId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Game");
            DropTable("dbo.Bet");
            DropTable("dbo.Player");
            DropTable("dbo.BankTransaction");
        }
    }
}
