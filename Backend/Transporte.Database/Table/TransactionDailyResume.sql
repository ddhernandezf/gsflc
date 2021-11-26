CREATE TABLE [dbo].[TransactionDailyResume]
(
	[Day]		TINYINT NOT NULL,
	[Month]		TINYINT NOT NULL,
	[Year]		SMALLINT NOT NULL,
	[Vehicle]	TINYINT NOT NULL,
	[Total]		DECIMAL(18,2) NOT NULL,
	CONSTRAINT pkTransactionDailyResume PRIMARY KEY([Day],[Month],[Year],[Vehicle]),
	CONSTRAINT fkTransactiondailyresumeVehicle FOREIGN KEY([Vehicle]) REFERENCES [Vehicle]([Id])
)
