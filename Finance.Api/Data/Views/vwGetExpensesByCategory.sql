-- get expenses by category
create or alter VIEW [vwGetExpensesByCategory] as 
    select 
    [Transaction].UserId,
    Category.Title as [Category],
    YEAR([Transaction].PaidOrReceivedAt) as YEAR,
    sum([Transaction].[Amount]) as [Expenses]
    FROM
    [TRANSACTION]
    INNER JOIN [Category] on [Transaction].[CategoryId] = [Category].[Id]
    where 
    [Transaction].[PaidOrReceivedAt]  >=  DATEADD(MONTH,-11,cast( GETDATE() as date))
    and [Transaction].[PaidOrReceivedAt] <DATEADD(MONTH,1,cast( GETDATE() as date))
    and [Transaction].[Type] = 2
    GROUP by [Transaction].[UserId],
    Category.Title,
    YEAR([Transaction].PaidOrReceivedAt)