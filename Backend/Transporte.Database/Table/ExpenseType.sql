CREATE TABLE [dbo].[ExpenseType]
(
	[Id]	TINYINT NOT NULL IDENTITY(1,1),
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkExpenseType PRIMARY KEY([Id]),
	CONSTRAINT uqExpenseType UNIQUE([Name])
)
