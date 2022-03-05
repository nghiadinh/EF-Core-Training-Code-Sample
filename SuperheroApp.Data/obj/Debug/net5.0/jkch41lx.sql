BEGIN TRANSACTION;
GO

ALTER TABLE [Heroes] ADD [Description] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220215042711_add-description-to-hero', N'5.0.7');
GO

COMMIT;
GO

