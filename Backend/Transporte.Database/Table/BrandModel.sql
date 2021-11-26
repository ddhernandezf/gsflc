CREATE TABLE [dbo].[BrandModel]
(
	[Id]	TINYINT NOT NULL IDENTITY(1,1),
	[Brand]	TINYINT NOT NULL,
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkBrandModel PRIMARY KEY([Id]),
	CONSTRAINT uqBrandModel UNIQUE([Brand], [Name]),
	CONSTRAINT fkBrandmodelBrand FOREIGN KEY([Brand]) REFERENCES [Brand]([Id])
)
