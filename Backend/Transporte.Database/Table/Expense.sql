CREATE TABLE [dbo].[Expense]
(
	[Id]	SMALLINT NOT NULL IDENTITY(1,1),
	[Type]	TINYINT NOT NULL,
	[Name]	VARCHAR(40) NOT NULL,
	CONSTRAINT pkExpense PRIMARY KEY([Id]),
	CONSTRAINT uqExpense UNIQUE([Type], [Name]),
	CONSTRAINT fkExpenseExpensetype FOREIGN KEY([Type]) REFERENCES [ExpenseType]([Id])
)
