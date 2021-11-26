ALTER TABLE [Transaction] DROP CONSTRAINT [fkTransactionService];
ALTER TABLE [Transaction] DROP CONSTRAINT [fkTransactionExpense];
ALTER TABLE [Expense] DROP CONSTRAINT [pkExpense];
ALTER TABLE [Expense] DROP CONSTRAINT [uqExpense];
ALTER TABLE [Service] DROP CONSTRAINT [pkService];
ALTER TABLE [Service] DROP CONSTRAINT [uqService];
GO
ALTER TABLE [Transaction] ALTER COLUMN [Service] SMALLINT NULL;
ALTER TABLE [Transaction] ALTER COLUMN [Expense] SMALLINT NULL;
ALTER TABLE [Service] ALTER COLUMN [Id]	SMALLINT NOT NULL;
ALTER TABLE [Expense] ALTER COLUMN [Id]	SMALLINT NOT NULL;
GO
ALTER TABLE [Expense] ADD CONSTRAINT pkExpense PRIMARY KEY([Id]);
ALTER TABLE [Expense] ADD CONSTRAINT uqExpense UNIQUE([Type], [Name]);
ALTER TABLE [Service] ADD CONSTRAINT pkService PRIMARY KEY([Id]);
ALTER TABLE [Service] ADD CONSTRAINT uqService UNIQUE([Type], [Name]);
ALTER TABLE [Transaction] ADD CONSTRAINT fkTransactionService FOREIGN KEY([Service]) REFERENCES [Service]([Id]);
ALTER TABLE [Transaction] ADD CONSTRAINT fkTransactionExpense FOREIGN KEY([Expense]) REFERENCES [Expense]([Id]);
GO