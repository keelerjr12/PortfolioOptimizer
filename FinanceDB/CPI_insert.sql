BULK INSERT dbo.ConsumerPriceIndex
FROM 'C:\Users\Joshua\Downloads\file.csv'
WITH
(
  FIELDTERMINATOR = ',',
  CHECK_CONSTRAINTS
)