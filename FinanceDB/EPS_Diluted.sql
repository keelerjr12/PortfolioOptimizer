CREATE TABLE [dbo].[EPS_Diluted]
(
    [Ticker] VARCHAR(32) NOT NULL,
    [QuarterEnd] DATE NOT NULL , 
    [EPS] MONEY NOT NULL, 
    PRIMARY KEY ([Ticker], [QuarterEnd])
)
