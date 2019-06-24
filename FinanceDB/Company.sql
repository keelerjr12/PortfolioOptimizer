CREATE TABLE [dbo].[Company]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Ticker] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(128) NOT NULL,
    [SectorId] Int NOT NULL, 
    [CIK] INT NOT NULL, 
    CONSTRAINT [FK_Company_To_GISC_Sector] FOREIGN KEY ([SectorId]) REFERENCES [GISCSector]([Id])
)
