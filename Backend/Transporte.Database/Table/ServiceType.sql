CREATE TABLE [dbo].[ServiceType]
(
	[Id]	TINYINT NOT NULL IDENTITY(1,1),
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkServiceType PRIMARY KEY([Id]),
	CONSTRAINT uqServiceType UNIQUE([Name])
)
