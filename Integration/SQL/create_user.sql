
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Email] [varchar](500) NULL,
	[Phone] [varchar](500) NULL,
	[Password] [varchar](500) NULL
) ON [PRIMARY]
GO
