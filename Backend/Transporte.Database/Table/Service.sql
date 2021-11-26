CREATE TABLE [dbo].[Service]
(
	[Id]	SMALLINT NOT NULL IDENTITY(1,1),
	[Type]	TINYINT NOT NULL,
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkService PRIMARY KEY([Id]),
	CONSTRAINT uqService UNIQUE([Type], [Name]),
	CONSTRAINT fkServiceServicetype FOREIGN KEY([Type]) REFERENCES [ServiceType]([Id])
)
