namespace SocialNetwork.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendShips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        Message = c.String(),
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1Id)
                .ForeignKey("dbo.Users", t => t.User2Id)
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
                        IsBlocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        MsgText = c.String(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        User1Id = c.Int(nullable: false),
                        User2Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1Id)
                .ForeignKey("dbo.Users", t => t.User2Id)
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

            Sql(@"
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

            Sql(@"
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

            Sql(@"
IF NOT EXISTS (SELECT * FROM [dbo].[Roles])
BEGIN
    SET IDENTITY_INSERT [dbo].[Roles] ON
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (1, N'admin', N'This is admin.')
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (2, N'user', N'This is user.')
    SET IDENTITY_INSERT [dbo].[Roles] OFF
END");

            Sql(@"
IF NOT EXISTS (SELECT * FROM [dbo].[Users])
BEGIN
    insert into [dbo].[Users] ([Email], [Password], [FirstName], [LastName], [Birthday], [Sex], [IsBlocked])
    values ('admin', '21232f297a57a5a743894a0e4a801fc3', 'admin', 'admin', '1900/01/01', 0, 0)

    insert into [dbo].[UserRole] ([UserId], [RoleId])
    values ((select [Id] from [dbo].[Users] where email='admin'), (select [Id] from [dbo].[Roles] where RoleName='admin'))
END");

            Sql(@"
CREATE TABLE [dbo].[ELMAH_Error]
(
    [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
    [Application] NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Host]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Type]        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Source]      NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Message]     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [User]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [StatusCode]  INT NOT NULL,
    [TimeUtc]     DATETIME NOT NULL,
    [Sequence]    INT IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) 
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

            Sql(@"
ALTER TABLE [dbo].[ELMAH_Error] ADD 
    CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED ([ErrorId]) ON [PRIMARY]");

            Sql(@"
ALTER TABLE [dbo].[ELMAH_Error] ADD 
    CONSTRAINT [DF_ELMAH_Error_ErrorId] DEFAULT (NEWID()) FOR [ErrorId]");

            Sql(@"
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error] 
(
    [Application]   ASC,
    [TimeUtc]       DESC,
    [Sequence]      DESC
) 
ON [PRIMARY]");

            Sql(@"
CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS
    SET QUOTED_IDENTIFIER ON 
    SET ANSI_NULLS ON 
    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application

SET QUOTED_IDENTIFIER OFF 
SET ANSI_NULLS ON 

SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON");

            Sql(@"
CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO

SET QUOTED_IDENTIFIER OFF 
SET ANSI_NULLS ON 

SET QUOTED_IDENTIFIER ON 
SET ANSI_NULLS ON");

            Sql(@"

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )

SET QUOTED_IDENTIFIER OFF 
SET ANSI_NULLS ON");            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.Messages", new[] { "User2Id" });
            DropIndex("dbo.Messages", new[] { "User1Id" });
            DropIndex("dbo.FriendShips", new[] { "User2Id" });
            DropIndex("dbo.FriendShips", new[] { "User1Id" });
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Messages", "User2Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "User1Id", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "User2Id", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "User1Id", "dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRole");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.FriendShips");

            /* TODO: Drop all procedures, triggers and elmah tables*/
        }
    }
}
