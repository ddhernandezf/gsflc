﻿/*
Deployment script for Transporte

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Transporte"
:setvar DefaultFilePrefix "Transporte"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Brand].[Type] on table [dbo].[Brand] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Brand])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[fkBrandmodelBrand]...';


GO
ALTER TABLE [dbo].[BrandModel] DROP CONSTRAINT [fkBrandmodelBrand];


GO
PRINT N'Dropping [dbo].[fkVehicleBrand]...';


GO
ALTER TABLE [dbo].[Vehicle] DROP CONSTRAINT [fkVehicleBrand];


GO
PRINT N'Starting rebuilding table [dbo].[Brand]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Brand] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Type] TINYINT      NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_pkBrand1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_uqBrand1] UNIQUE NONCLUSTERED ([Name] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Brand])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Brand] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Brand] ([Id], [Name])
        SELECT   [Id],
                 [Name]
        FROM     [dbo].[Brand]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Brand] OFF;
    END

DROP TABLE [dbo].[Brand];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Brand]', N'Brand';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_pkBrand1]', N'pkBrand', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_uqBrand1]', N'uqBrand', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[fkBrandmodelBrand]...';


GO
ALTER TABLE [dbo].[BrandModel] WITH NOCHECK
    ADD CONSTRAINT [fkBrandmodelBrand] FOREIGN KEY ([Brand]) REFERENCES [dbo].[Brand] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehicleBrand]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehicleBrand] FOREIGN KEY ([Brand]) REFERENCES [dbo].[Brand] ([Id]);


GO
PRINT N'Creating [dbo].[fkBrandVehicletype]...';


GO
ALTER TABLE [dbo].[Brand] WITH NOCHECK
    ADD CONSTRAINT [fkBrandVehicletype] FOREIGN KEY ([Type]) REFERENCES [dbo].[VehicleType] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[BrandModel] WITH CHECK CHECK CONSTRAINT [fkBrandmodelBrand];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehicleBrand];

ALTER TABLE [dbo].[Brand] WITH CHECK CHECK CONSTRAINT [fkBrandVehicletype];


GO
PRINT N'Update complete.';


GO