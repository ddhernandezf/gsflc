UPDATE [Action] SET IsGroup = 0 WHERE Parent = 38;
INSERT INTO [RoleAction] VALUES (23, 3);
INSERT INTO [RoleAction] SELECT	Id, 2 FROM [Action] WHERE Parent = 38;
INSERT INTO [RoleAction] SELECT	Id, 1 FROM [Action] WHERE Parent = 38;