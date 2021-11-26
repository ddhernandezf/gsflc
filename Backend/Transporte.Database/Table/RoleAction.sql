CREATE TABLE [dbo].[RoleAction]
(
	[Action]	TINYINT NOT NULL,
	[Role]	TINYINT NOT NULL,
	CONSTRAINT pkRoleAction PRIMARY KEY([Action], [Role]),
	CONSTRAINT fkRoleactionAction FOREIGN KEY([Action]) REFERENCES [Action]([Id]),
	CONSTRAINT fkRoleactionRole FOREIGN KEY([Role]) REFERENCES [Role]([Id]),
)
