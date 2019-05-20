
IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[User] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END;

IF NOT EXISTS ( SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Name')
BEGIN
    ALTER TABLE [User] ADD [Name] [varchar](500) NULL
END;


IF NOT EXISTS ( SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Email')
BEGIN
    ALTER TABLE [User] ADD [Email] [varchar](500) NULL
END;

IF NOT EXISTS ( SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Phone')
BEGIN
    ALTER TABLE [User] ADD [Phone] [varchar](500) NULL
END;

IF NOT EXISTS ( SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Password')
BEGIN
    ALTER TABLE [User] ADD [Password] [varchar](500) NULL
END;

