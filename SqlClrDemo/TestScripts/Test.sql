INSERT INTO [dbo].[Authors] ([Name])
SELECT 'Bob'
UNION 
SELECT 'Randy'
UNION 
SELECT 'Ana'
UNION
SELECT 'Susan';
GO

SELECT dbo.MaxVariance([AuthorId]) As [AuthorId]
FROM [dbo].[Authors];