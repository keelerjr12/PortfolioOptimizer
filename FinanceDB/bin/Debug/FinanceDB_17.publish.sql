﻿/*
Deployment script for FinanceDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "FinanceDB"
:setvar DefaultFilePrefix "FinanceDB"
:setvar DefaultDataPath "C:\Users\Joshua\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\Joshua\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

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
PRINT N'Rename refactoring operation with key 9e4b828c-3d32-4ab0-b785-5ff4c56b13e5 is skipped, element [dbo].[Company].[Sector_Id] (SqlSimpleColumn) will not be renamed to SectorId';


GO
PRINT N'Rename refactoring operation with key 28d580ce-27d9-4f73-90f3-b3477db528cb is skipped, element [dbo].[SP500Companies].[Company_Id] (SqlSimpleColumn) will not be renamed to CompanyId';


GO
PRINT N'Creating [dbo].[Company]...';


GO
CREATE TABLE [dbo].[Company] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Ticker]   VARCHAR (50)     NOT NULL,
    [Name]     VARCHAR (128)    NOT NULL,
    [SectorId] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[SP500Companies]...';


GO
CREATE TABLE [dbo].[SP500Companies] (
    [CompanyId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Company_To_GISC_Sector]...';


GO
ALTER TABLE [dbo].[Company] WITH NOCHECK
    ADD CONSTRAINT [FK_Company_To_GISC_Sector] FOREIGN KEY ([SectorId]) REFERENCES [dbo].[GISCSector] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SP500_Companies_ToCompany]...';


GO
ALTER TABLE [dbo].[SP500Companies] WITH NOCHECK
    ADD CONSTRAINT [FK_SP500_Companies_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9e4b828c-3d32-4ab0-b785-5ff4c56b13e5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9e4b828c-3d32-4ab0-b785-5ff4c56b13e5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '28d580ce-27d9-4f73-90f3-b3477db528cb')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('28d580ce-27d9-4f73-90f3-b3477db528cb')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Company] WITH CHECK CHECK CONSTRAINT [FK_Company_To_GISC_Sector];

ALTER TABLE [dbo].[SP500Companies] WITH CHECK CHECK CONSTRAINT [FK_SP500_Companies_ToCompany];


GO
PRINT N'Update complete.';


GO