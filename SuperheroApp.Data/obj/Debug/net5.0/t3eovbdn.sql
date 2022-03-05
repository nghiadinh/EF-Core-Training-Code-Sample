IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Battles] (
    [Id] int NOT NULL IDENTITY,
    [Location] nvarchar(max) NULL,
    CONSTRAINT [PK_Battles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Teams] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Heroes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [RealName] nvarchar(max) NULL,
    [Race] int NOT NULL,
    [TeamId] int NULL,
    CONSTRAINT [PK_Heroes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Heroes_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [BattleHero] (
    [BattlesId] int NOT NULL,
    [HeroesId] int NOT NULL,
    CONSTRAINT [PK_BattleHero] PRIMARY KEY ([BattlesId], [HeroesId]),
    CONSTRAINT [FK_BattleHero_Battles_BattlesId] FOREIGN KEY ([BattlesId]) REFERENCES [Battles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BattleHero_Heroes_HeroesId] FOREIGN KEY ([HeroesId]) REFERENCES [Heroes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Equipments] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Type] int NOT NULL,
    [HeroId] int NULL,
    CONSTRAINT [PK_Equipments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Equipments_Heroes_HeroId] FOREIGN KEY ([HeroId]) REFERENCES [Heroes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_BattleHero_HeroesId] ON [BattleHero] ([HeroesId]);
GO

CREATE INDEX [IX_Equipments_HeroId] ON [Equipments] ([HeroId]);
GO

CREATE INDEX [IX_Heroes_TeamId] ON [Heroes] ([TeamId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210628161646_init', N'5.0.7');
GO

COMMIT;
GO

