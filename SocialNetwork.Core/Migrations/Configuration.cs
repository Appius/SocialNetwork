namespace SocialNetwork.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialNetwork.Core.Models.SocialNetworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SocialNetwork.Core.Models.SocialNetworkContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Database.ExecuteSqlCommand(@"
CREATE TRIGGER [DELETE_Users]
    ON [dbo].[Users]
    INSTEAD OF DELETE
AS 
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [Messages] WHERE User1Id IN (SELECT Id FROM DELETED)
    DELETE FROM [Messages] WHERE User2Id IN (SELECT Id FROM DELETED)
    DELETE FROM [FriendShips] WHERE User1Id IN (SELECT Id FROM DELETED)
    DELETE FROM [FriendShips] WHERE User2Id IN (SELECT Id FROM DELETED)

    DELETE FROM [Users] WHERE Id IN (SELECT Id FROM DELETED)
END");

            // продедура удаления всех таблиц и связей между ними
            context.Database.ExecuteSqlCommand(@"
CREATE PROCEDURE [dbo].[DropAllTables]
AS
BEGIN
	IF object_id('[dbo].[UserRole]','U') IS NOT NULL
	BEGIN
		ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_ToRole]
		ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_ToUser]
		DROP TABLE [dbo].[UserRole]
	END

	IF object_id('[dbo].[Roles]','U') IS NOT NULL
	BEGIN
		DROP TABLE [dbo].[Roles]
	END

	IF object_id('[dbo].[Messages]','U') IS NOT NULL
	BEGIN
		ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_ToUsers1]
		ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_ToUsers2]
		DROP TABLE [dbo].[Messages]
	END

	IF object_id('[dbo].[FriendShips]','U') IS NOT NULL
	BEGIN
		ALTER TABLE [dbo].[FriendShips] DROP CONSTRAINT [FK_FriendShips_ToUsers1]
		ALTER TABLE [dbo].[FriendShips] DROP CONSTRAINT [FK_FriendShips_ToUsers2]
		DROP TABLE [dbo].[FriendShips]
	END

	IF object_id('[dbo].[Users]','U') IS NOT NULL
	BEGIN
		DROP TABLE [dbo].[Users]
	END
END");
            // начальные роли
            context.Database.ExecuteSqlCommand(@"
IF NOT EXISTS (SELECT * FROM [dbo].[Roles])
BEGIN
    SET IDENTITY_INSERT [dbo].[Roles] ON
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (1, N'admin', N'This is admin.')
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (2, N'user', N'This is user.')
    SET IDENTITY_INSERT [dbo].[Roles] OFF
END");
        }
    }
}
