CREATE TABLE [dbo].[DailyStockPrice]
(
    [Ticker] VARCHAR(50) NOT NULL, 
    [Date] DATE NOT NULL, 
    [Price] MONEY NOT NULL, 
    PRIMARY KEY ([Ticker], [Date])
)
