CREATE TABLE [dbo].[Transaction]
(
	[Id]				BIGINT NOT NULL IDENTITY(1,1),
	[Vehicle]			TINYINT NOT NULL,
	[Service]			SMALLINT NULL,
	[Expense]			SMALLINT NULL,
	[RegisterDate]		DATETIME NOT NULL,
	[TransactionDate]	DATE NOT NULL,
	[User]				TINYINT NOT NULL,
	[Total]				DECIMAL(18,2)
	CONSTRAINT pkTransation PRIMARY KEY([Id]),
	CONSTRAINT fkTransactionVehicle FOREIGN KEY([Vehicle]) REFERENCES [Vehicle]([Id]),
	CONSTRAINT fkTransactionService FOREIGN KEY([Service]) REFERENCES [Service]([Id]),
	CONSTRAINT fkTransactionExpense FOREIGN KEY([Expense]) REFERENCES [Expense]([Id]),
	CONSTRAINT fkTransactionUser FOREIGN KEY([User]) REFERENCES [User]([Id])
)
