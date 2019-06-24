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
PRINT N'Creating [dbo].[GISCSector]...';


GO
CREATE TABLE [dbo].[GISCSector] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Company_To_GISC_Sector]...';


GO
ALTER TABLE [dbo].[Company] WITH NOCHECK
    ADD CONSTRAINT [FK_Company_To_GISC_Sector] FOREIGN KEY ([Sector_Id]) REFERENCES [dbo].[GISCSector] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Company] WITH CHECK CHECK CONSTRAINT [FK_Company_To_GISC_Sector];


GO
PRINT N'Update complete.';


GO
