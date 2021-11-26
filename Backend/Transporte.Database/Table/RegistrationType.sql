CREATE TABLE [dbo].[RegistrationType]
(
	[Id]	VARCHAR(2) NOT NULL,
	[Name]	VARCHAR(15) NOT NULL,
	CONSTRAINT pkRegistrationType PRIMARY KEY([Id]),
	CONSTRAINT uqRegistrationType UNIQUE([Name])
)
