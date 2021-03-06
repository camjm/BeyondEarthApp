﻿CREATE TABLE [dbo].[Faction]
(
	[FactionId]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[Name]			NVARCHAR (100)	NOT NULL,
	[Leader]		NVARCHAR (100)	NOT NULL,
	[Capital]		NVARCHAR (100)	NOT NULL,
	[Ability]		NVARCHAR (200)	NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([FactionId] ASC)
)
