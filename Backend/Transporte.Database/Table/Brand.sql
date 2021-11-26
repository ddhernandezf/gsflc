CREATE TABLE [dbo].[Brand]
(
	[Id]	TINYINT NOT NULL IDENTITY(1,1),
	[Type]	TINYINT NOT NULL,
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkBrand PRIMARY KEY([Id]),
	CONSTRAINT uqBrand UNIQUE([Type], [Name]),
	CONSTRAINT fkBrandVehicletype FOREIGN KEY([Type]) REFERENCES [VehicleType]([Id])
)
