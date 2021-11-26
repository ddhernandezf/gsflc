CREATE TABLE [dbo].[RoleUser]
(
	[User]	TINYINT NOT NULL,
	[Role]	TINYINT NOT NULL,
	CONSTRAINT pkRoleUser PRIMARY KEY([User], [Role]),
	CONSTRAINT fkRoleuserUser FOREIGN KEY([User]) REFERENCES [User]([Id]),
	CONSTRAINT fkRoleuserRole FOREIGN KEY([Role]) REFERENCES [Role]([Id]),
)
