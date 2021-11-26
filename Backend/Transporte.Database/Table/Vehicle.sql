CREATE TABLE [dbo].[Vehicle]
(
	[Id]				TINYINT NOT NULL IDENTITY(1,1),
	[Type]				TINYINT NOT NULL,
	[RegistrationType]	VARCHAR(2) NOT NULL,
	[Registration]		VARCHAR(10) NOT NULL,
	[Brand]				TINYINT NOT NULL,
	[Model]				TINYINT NOT NULL,
	[Year]				SMALLINT NOT NULL,
	[Name]				VARCHAR(50) NULL,
	[Active]			BIT NOT NULL DEFAULT 1,
	CONSTRAINT pkVehicle PRIMARY KEY([Id]),
	CONSTRAINT uqVehicle UNIQUE([RegistrationType], [Registration]),
	CONSTRAINT fkVehicleType FOREIGN KEY([Type]) REFERENCES [VehicleType]([Id]),
	CONSTRAINT fkVehicleRegistrsationtype FOREIGN KEY([RegistrationType]) REFERENCES [RegistrationType]([Id]),
	CONSTRAINT fkVehicleBrand FOREIGN KEY([Brand]) REFERENCES [Brand]([Id]),
	CONSTRAINT fkVehicleModel FOREIGN KEY([Model]) REFERENCES [BrandModel]([Id])
)
