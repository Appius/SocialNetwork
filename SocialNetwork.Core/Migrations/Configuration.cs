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
            context.Database.ExecuteSqlCommand(@"
IF NOT EXISTS (SELECT * FROM [dbo].[Roles])
BEGIN
    SET IDENTITY_INSERT [dbo].[Roles] ON
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (1, N'admin', N'This is admin.')
    INSERT INTO [dbo].[Roles] ([Id], [RoleName], [Description]) VALUES (2, N'user', N'This is user.')
    SET IDENTITY_INSERT [dbo].[Roles] OFF
END");

            context.Database.ExecuteSqlCommand(@"
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

            context.Database.ExecuteSqlCommand(@"
ALTER TABLE [dbo].[ELMAH_Error] WITH NOCHECK ADD 
    CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED ([ErrorId]) ON [PRIMARY]");

            context.Database.ExecuteSqlCommand(@"
ALTER TABLE [dbo].[ELMAH_Error] ADD 
    CONSTRAINT [DF_ELMAH_Error_ErrorId] DEFAULT (NEWID()) FOR [ErrorId]");

            context.Database.ExecuteSqlCommand(@"
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error] 
(
    [Application]   ASC,
    [TimeUtc]       DESC,
    [Sequence]      DESC
) 
ON [PRIMARY]");

            context.Database.ExecuteSqlCommand(@"
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

            context.Database.ExecuteSqlCommand(@"
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

            context.Database.ExecuteSqlCommand(@"

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
    }
}
