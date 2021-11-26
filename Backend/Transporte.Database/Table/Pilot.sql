CREATE TABLE [dbo].[Pilot]
(
	[Id]			TINYINT NOT NULL IDENTITY(1,1),
	[IsMale]		BIT NOT NULL DEFAULT 1,
	[HiringDate]	DATE NULL,
	[BornDate]		DATE NOT NULL,
	[CompleteName]	VARCHAR(120) NOT NULL,
	CONSTRAINT pkPilot	PRIMARY KEY([Id])
)
