
IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Client]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Client] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Client]') AND name = 'Name')
BEGIN
    ALTER TABLE [Client] ADD [Name] [varchar](500) NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Admin]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Admin] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Admin]') AND name = 'Name')
BEGIN
    ALTER TABLE [Admin] ADD [Name] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Admin]') AND name = 'Email')
BEGIN
    ALTER TABLE [Admin] ADD [Email] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Admin]') AND name = 'Phone')
BEGIN
    ALTER TABLE [Admin] ADD [Phone] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Admin]') AND name = 'Password')
BEGIN
    ALTER TABLE [Admin] ADD [Password] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Admin]') AND name = 'ClientID')
BEGIN
    ALTER TABLE [Admin] ADD [ClientID] bigint NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[User] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Name')
BEGIN
    ALTER TABLE [User] ADD [Name] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Email')
BEGIN
    ALTER TABLE [User] ADD [Email] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Phone')
BEGIN
    ALTER TABLE [User] ADD [Phone] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'Password')
BEGIN
    ALTER TABLE [User] ADD [Password] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[User]') AND name = 'ClientID')
BEGIN
    ALTER TABLE [User] ADD [ClientID] bigint NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[ProductCategory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[ProductCategory] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND name = 'ClientID')
BEGIN
    ALTER TABLE [ProductCategory] ADD [ClientID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND name = 'Name')
BEGIN
    ALTER TABLE [ProductCategory] ADD [Name] [varchar](500) NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[ProductSubCategory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[ProductSubCategory] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductSubCategory]') AND name = 'ClientID')
BEGIN
    ALTER TABLE [ProductSubCategory] ADD [ClientID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductSubCategory]') AND name = 'ProductCategoryID')
BEGIN
    ALTER TABLE [ProductSubCategory] ADD [ProductCategoryID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductSubCategory]') AND name = 'Name')
BEGIN
    ALTER TABLE [ProductSubCategory] ADD [Name] [varchar](500) NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Product]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Product] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ClientID')
BEGIN
    ALTER TABLE [Product] ADD [ClientID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ProductCategoryID')
BEGIN
    ALTER TABLE [Product] ADD [ProductCategoryID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ProductSubCategoryID')
BEGIN
    ALTER TABLE [Product] ADD [ProductSubCategoryID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'Name')
BEGIN
    ALTER TABLE [Product] ADD [Name] [varchar](500) NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'Views')
BEGIN
    ALTER TABLE [Product] ADD [Views] int NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'CreatedByAdminID')
BEGIN
    ALTER TABLE [Product] ADD [CreatedByAdminID] bigint NULL
END;

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[ProductComment]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[ProductComment] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductComment]') AND name = 'ProductID')
BEGIN
    ALTER TABLE [ProductComment] ADD [ProductID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductComment]') AND name = 'UserID')
BEGIN
    ALTER TABLE [ProductComment] ADD [UserID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductComment]') AND name = 'UserID')
BEGIN
    ALTER TABLE [ProductComment] ADD [UserID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductComment]') AND name = 'Comment')
BEGIN
    ALTER TABLE [ProductComment] ADD [Comment] [varchar](500) NULL
END

------------------------------------------------

IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[ProductFavorited]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[ProductFavorited] ( [Id] [bigint] IDENTITY(1,1) NOT NULL );
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductFavorited]') AND name = 'ProductID')
BEGIN
    ALTER TABLE [ProductFavorited] ADD [ProductID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductFavorited]') AND name = 'UserID')
BEGIN
    ALTER TABLE [ProductFavorited] ADD [UserID] bigint NULL
END

IF NOT EXISTS ( SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ProductFavorited]') AND name = 'UserID')
BEGIN
    ALTER TABLE [ProductFavorited] ADD [UserID] bigint NULL
END