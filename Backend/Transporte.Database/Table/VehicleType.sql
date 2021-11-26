CREATE TABLE [dbo].[VehicleType]
(
	[Id]			TINYINT NOT NULL IDENTITY(1,1),
	[Name]			VARCHAR(40) NOT NULL,
	[CanService]	BIT NOT NULL DEFAULT 1,
	[CanExpense]	BIT NOT NULL DEFAULT 1,
	CONSTRAINT pkVehicleType PRIMARY KEY([Id]),
	CONSTRAINT uqVehicleType UNIQUE([Name])
)
