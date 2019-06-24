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
PRINT N'Rename refactoring operation with key ec1c4148-083b-4bc4-b238-09b83e56e5b0 is skipped, element [dbo].[DailyStockPrice].[Id] (SqlSimpleColumn) will not be renamed to Ticker';


GO
PRINT N'Creating [dbo].[DailyStockPrice]...';


GO
CREATE TABLE [dbo].[DailyStockPrice] (
    [Ticker] VARCHAR (50) NOT NULL,
    [Date]   DATE         NOT NULL,
    [Price]  MONEY        NOT NULL,
    PRIMARY KEY CLUSTERED ([Ticker] ASC, [Date] ASC)
);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ec1c4148-083b-4bc4-b238-09b83e56e5b0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ec1c4148-083b-4bc4-b238-09b83e56e5b0')

GO

GO
PRINT N'Update complete.';


GO
