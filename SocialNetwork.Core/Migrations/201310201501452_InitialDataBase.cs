namespace SocialNetwork.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendShips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User2Id, cascadeDelete: true)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        Avatar = c.Binary(),
                        Mobile = c.String(maxLength: 20),
                        Sex = c.Boolean(nullable: false),
                        Website = c.String(maxLength: 100),
                        Skype = c.String(maxLength: 50),
                        CurrentCity = c.String(maxLength: 50, fixedLength: true),
                        Activies = c.String(maxLength: 1000),
                        Interests = c.String(maxLength: 1000),
                        FavoriteMusic = c.String(maxLength: 1000),
                        FavoriteMovies = c.String(maxLength: 1000),
                        FavoriteBooks = c.String(maxLength: 1000),
                        FavoriteGames = c.String(maxLength: 1000),
                        FavoriteQuotes = c.String(maxLength: 1000),
                        AboutMe = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        MsgText = c.String(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User2Id, cascadeDelete: true)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendShips", "User2Id", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "User1Id", "dbo.Users");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Messages", "User2Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "User1Id", "dbo.Users");
            DropIndex("dbo.FriendShips", new[] { "User2Id" });
            DropIndex("dbo.FriendShips", new[] { "User1Id" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.Messages", new[] { "User2Id" });
            DropIndex("dbo.Messages", new[] { "User1Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRole");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.FriendShips");
        }
    }
}
