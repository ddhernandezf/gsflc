CREATE TABLE [dbo].[TransactionDetail]
(
	[Id]			BIGINT NOT NULL IDENTITY(1,1),
	[Transaction]	BIGINT NOT NULL,
	[Quantity]		DECIMAL(18,2) NOT NULL,
	[Description]	VARCHAR(300) NOT NULL,
	[UnitPrice]		DECIMAL(18,2) NOT NULL,
	[TotalPrice]	DECIMAL(18,2) NOT NULL,
	CONSTRAINT pkTransactionDetail PRIMARY KEY([Id]),
	CONSTRAINT fkTransactiondetailTransaction FOREIGN KEY([Transaction]) REFERENCES [Transaction]([Id])
)
