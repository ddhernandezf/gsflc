/*
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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating [dbo].[Action]...';


GO
CREATE TABLE [dbo].[Action] (
    [Id]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40)  NOT NULL,
    [Text] VARCHAR (100) NOT NULL,
    CONSTRAINT [pkAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqAction] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[Brand]...';


GO
CREATE TABLE [dbo].[Brand] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkBrand] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqBrand] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[BrandModel]...';


GO
CREATE TABLE [dbo].[BrandModel] (
    [Id]    TINYINT      IDENTITY (1, 1) NOT NULL,
    [Brand] TINYINT      NOT NULL,
    [Name]  VARCHAR (40) NOT NULL,
    CONSTRAINT [pkBrandModel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqBrandModel] UNIQUE NONCLUSTERED ([Brand] ASC, [Name] ASC)
);


GO
PRINT N'Creating [dbo].[Expense]...';


GO
CREATE TABLE [dbo].[Expense] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Type] TINYINT      NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkExpense] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqExpense] UNIQUE NONCLUSTERED ([Type] ASC, [Name] ASC)
);


GO
PRINT N'Creating [dbo].[ExpenseType]...';


GO
CREATE TABLE [dbo].[ExpenseType] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkExpenseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqExpenseType] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[Pilot]...';


GO
CREATE TABLE [dbo].[Pilot] (
    [Id]           TINYINT       IDENTITY (1, 1) NOT NULL,
    [IsMale]       BIT           NOT NULL,
    [BornDate]     DATE          NOT NULL,
    [CompleteName] VARCHAR (120) NOT NULL,
    CONSTRAINT [pkPilot] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RegistrationType]...';


GO
CREATE TABLE [dbo].[RegistrationType] (
    [Id]   VARCHAR (1)  NOT NULL,
    [Name] VARCHAR (15) NOT NULL,
    CONSTRAINT [pkRegistrationType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqRegistrationType] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqRole] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[RoleAction]...';


GO
CREATE TABLE [dbo].[RoleAction] (
    [Action] TINYINT NOT NULL,
    [Role]   TINYINT NOT NULL,
    CONSTRAINT [pkRoleAction] PRIMARY KEY CLUSTERED ([Action] ASC, [Role] ASC)
);


GO
PRINT N'Creating [dbo].[RoleUser]...';


GO
CREATE TABLE [dbo].[RoleUser] (
    [User] TINYINT NOT NULL,
    [Role] TINYINT NOT NULL,
    CONSTRAINT [pkRoleUser] PRIMARY KEY CLUSTERED ([User] ASC, [Role] ASC)
);


GO
PRINT N'Creating [dbo].[Service]...';


GO
CREATE TABLE [dbo].[Service] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Type] TINYINT      NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkService] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqService] UNIQUE NONCLUSTERED ([Type] ASC, [Name] ASC)
);


GO
PRINT N'Creating [dbo].[ServiceType]...';


GO
CREATE TABLE [dbo].[ServiceType] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    CONSTRAINT [pkServiceType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqServiceType] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[Transaction]...';


GO
CREATE TABLE [dbo].[Transaction] (
    [Id]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [Vehicle]         TINYINT         NOT NULL,
    [Service]         TINYINT         NULL,
    [Expense]         TINYINT         NULL,
    [RegisterDate]    DATETIME        NOT NULL,
    [TransactionDate] DATE            NOT NULL,
    [User]            TINYINT         NOT NULL,
    [Total]           DECIMAL (18, 2) NULL,
    CONSTRAINT [pkTransation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[TransactionDailyResume]...';


GO
CREATE TABLE [dbo].[TransactionDailyResume] (
    [Day]     TINYINT         NOT NULL,
    [Month]   TINYINT         NOT NULL,
    [Year]    SMALLINT        NOT NULL,
    [Vehicle] TINYINT         NOT NULL,
    [Total]   DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [pkTransactionDailyResume] PRIMARY KEY CLUSTERED ([Day] ASC, [Month] ASC, [Year] ASC, [Vehicle] ASC)
);


GO
PRINT N'Creating [dbo].[TransactionDetail]...';


GO
CREATE TABLE [dbo].[TransactionDetail] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [Transaction] BIGINT          NOT NULL,
    [Quantity]    TINYINT         NOT NULL,
    [Description] VARCHAR (300)   NOT NULL,
    [UnitPrice]   DECIMAL (18, 2) NOT NULL,
    [TotalPrice]  DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [pkTransactionDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [Id]           TINYINT       IDENTITY (1, 1) NOT NULL,
    [Email]        VARCHAR (120) NOT NULL,
    [Password]     VARCHAR (300) NOT NULL,
    [CompleteName] VARCHAR (120) NOT NULL,
    CONSTRAINT [pkUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqUser] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Creating [dbo].[Vehicle]...';


GO
CREATE TABLE [dbo].[Vehicle] (
    [Id]               TINYINT      IDENTITY (1, 1) NOT NULL,
    [Type]             TINYINT      NOT NULL,
    [RegistrationType] VARCHAR (1)  NOT NULL,
    [Registration]     VARCHAR (10) NOT NULL,
    [Brand]            TINYINT      NOT NULL,
    [Model]            TINYINT      NOT NULL,
    [Year]             SMALLINT     NOT NULL,
    [AssignedPilot]    TINYINT      NOT NULL,
    CONSTRAINT [pkVehicle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqVehicle] UNIQUE NONCLUSTERED ([RegistrationType] ASC, [Registration] ASC)
);


GO
PRINT N'Creating [dbo].[VehicleType]...';


GO
CREATE TABLE [dbo].[VehicleType] (
    [Id]   TINYINT      IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (15) NOT NULL,
    CONSTRAINT [pkVehicleType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [uqVehicleType] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Pilot]...';


GO
ALTER TABLE [dbo].[Pilot]
    ADD DEFAULT 1 FOR [IsMale];


GO
PRINT N'Creating [dbo].[fkBrandmodelBrand]...';


GO
ALTER TABLE [dbo].[BrandModel] WITH NOCHECK
    ADD CONSTRAINT [fkBrandmodelBrand] FOREIGN KEY ([Brand]) REFERENCES [dbo].[Brand] ([Id]);


GO
PRINT N'Creating [dbo].[fkExpenseExpensetype]...';


GO
ALTER TABLE [dbo].[Expense] WITH NOCHECK
    ADD CONSTRAINT [fkExpenseExpensetype] FOREIGN KEY ([Type]) REFERENCES [dbo].[ExpenseType] ([Id]);


GO
PRINT N'Creating [dbo].[fkRoleactionAction]...';


GO
ALTER TABLE [dbo].[RoleAction] WITH NOCHECK
    ADD CONSTRAINT [fkRoleactionAction] FOREIGN KEY ([Action]) REFERENCES [dbo].[Action] ([Id]);


GO
PRINT N'Creating [dbo].[fkRoleactionRole]...';


GO
ALTER TABLE [dbo].[RoleAction] WITH NOCHECK
    ADD CONSTRAINT [fkRoleactionRole] FOREIGN KEY ([Role]) REFERENCES [dbo].[Role] ([Id]);


GO
PRINT N'Creating [dbo].[fkRoleuserUser]...';


GO
ALTER TABLE [dbo].[RoleUser] WITH NOCHECK
    ADD CONSTRAINT [fkRoleuserUser] FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Id]);


GO
PRINT N'Creating [dbo].[fkRoleuserRole]...';


GO
ALTER TABLE [dbo].[RoleUser] WITH NOCHECK
    ADD CONSTRAINT [fkRoleuserRole] FOREIGN KEY ([Role]) REFERENCES [dbo].[Role] ([Id]);


GO
PRINT N'Creating [dbo].[fkServiceServicetype]...';


GO
ALTER TABLE [dbo].[Service] WITH NOCHECK
    ADD CONSTRAINT [fkServiceServicetype] FOREIGN KEY ([Type]) REFERENCES [dbo].[ServiceType] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactionVehicle]...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [fkTransactionVehicle] FOREIGN KEY ([Vehicle]) REFERENCES [dbo].[Vehicle] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactionService]...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [fkTransactionService] FOREIGN KEY ([Service]) REFERENCES [dbo].[Service] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactionExpense]...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [fkTransactionExpense] FOREIGN KEY ([Expense]) REFERENCES [dbo].[Expense] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactionUser]...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [fkTransactionUser] FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactiondailyresumeVehicle]...';


GO
ALTER TABLE [dbo].[TransactionDailyResume] WITH NOCHECK
    ADD CONSTRAINT [fkTransactiondailyresumeVehicle] FOREIGN KEY ([Vehicle]) REFERENCES [dbo].[Vehicle] ([Id]);


GO
PRINT N'Creating [dbo].[fkTransactiondetailTransaction]...';


GO
ALTER TABLE [dbo].[TransactionDetail] WITH NOCHECK
    ADD CONSTRAINT [fkTransactiondetailTransaction] FOREIGN KEY ([Transaction]) REFERENCES [dbo].[Transaction] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehicleType]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehicleType] FOREIGN KEY ([Type]) REFERENCES [dbo].[VehicleType] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehicleRegistrsationtype]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehicleRegistrsationtype] FOREIGN KEY ([RegistrationType]) REFERENCES [dbo].[RegistrationType] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehicleBrand]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehicleBrand] FOREIGN KEY ([Brand]) REFERENCES [dbo].[Brand] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehicleModel]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehicleModel] FOREIGN KEY ([Model]) REFERENCES [dbo].[BrandModel] ([Id]);


GO
PRINT N'Creating [dbo].[fkVehiclePilot]...';


GO
ALTER TABLE [dbo].[Vehicle] WITH NOCHECK
    ADD CONSTRAINT [fkVehiclePilot] FOREIGN KEY ([AssignedPilot]) REFERENCES [dbo].[Pilot] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[BrandModel] WITH CHECK CHECK CONSTRAINT [fkBrandmodelBrand];

ALTER TABLE [dbo].[Expense] WITH CHECK CHECK CONSTRAINT [fkExpenseExpensetype];

ALTER TABLE [dbo].[RoleAction] WITH CHECK CHECK CONSTRAINT [fkRoleactionAction];

ALTER TABLE [dbo].[RoleAction] WITH CHECK CHECK CONSTRAINT [fkRoleactionRole];

ALTER TABLE [dbo].[RoleUser] WITH CHECK CHECK CONSTRAINT [fkRoleuserUser];

ALTER TABLE [dbo].[RoleUser] WITH CHECK CHECK CONSTRAINT [fkRoleuserRole];

ALTER TABLE [dbo].[Service] WITH CHECK CHECK CONSTRAINT [fkServiceServicetype];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [fkTransactionVehicle];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [fkTransactionService];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [fkTransactionExpense];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [fkTransactionUser];

ALTER TABLE [dbo].[TransactionDailyResume] WITH CHECK CHECK CONSTRAINT [fkTransactiondailyresumeVehicle];

ALTER TABLE [dbo].[TransactionDetail] WITH CHECK CHECK CONSTRAINT [fkTransactiondetailTransaction];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehicleType];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehicleRegistrsationtype];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehicleBrand];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehicleModel];

ALTER TABLE [dbo].[Vehicle] WITH CHECK CHECK CONSTRAINT [fkVehiclePilot];


GO
PRINT N'Update complete.';


GO
