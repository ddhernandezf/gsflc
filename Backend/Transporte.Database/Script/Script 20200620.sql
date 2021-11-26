ALTER TABLE [Vehicle] DROP CONSTRAINT [fkVehicleTrailer];
ALTER TABLE [Vehicle] DROP COLUMN [Trailer];
DROP TABLE [Trailer];
DROP TABLE [TrailerType];
ALTER TABLE [Vehicle] ALTER COLUMN [AssignedPilot] TINYINT NULL;
ALTER TABLE [Vehicle] ADD [Name] VARCHAR(50) NULL;
ALTER TABLE [Vehicle] DROP CONSTRAINT [fkVehiclePilot];
ALTER TABLE [Vehicle] DROP COLUMN [AssignedPilot];
GO
UPDATE [Vehicle] SET [Name] = [RegistrationType] + [Registration];
GO
ALTER TABLE [Vehicle] ALTER COLUMN [Name] VARCHAR(50) NOT NULL;
ALTER TABLE [VehicleType] DROP COLUMN [UseTrailer];
GO
DELETE
  FROM	[RoleAction]
 WHERE	[Action] IN (SELECT Id FROM	[Action] WHERE [Name] LIKE '%trailer%' AND [Url] IS NOT NULL);
DELETE
  FROM	[Action]
 WHERE	[Name] LIKE '%trailer%'
   AND	[Url] IS NOT NULL;
DELETE
  FROM	[Action]
 WHERE	[Name] LIKE '%trailer%'
   AND	[Url] IS NULL
GO
--luego correr el implementado del proyecto de base de datos