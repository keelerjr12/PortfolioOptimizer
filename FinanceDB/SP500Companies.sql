CREATE TABLE [dbo].[SP500Companies]
(
    [CompanyId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    CONSTRAINT [FK_SP500_Companies_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id])
)
