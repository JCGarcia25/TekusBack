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
CREATE TABLE [Providers] (
    [Id] uniqueidentifier NOT NULL,
    [Nit] nvarchar(20) NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [Email] nvarchar(320) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);

CREATE TABLE [ProviderAttributes] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Key] nvarchar(150) NOT NULL,
    [Value] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_ProviderAttributes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProviderAttributes_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Services] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [HourlyRate] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Services_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ServiceCountries] (
    [Id] uniqueidentifier NOT NULL,
    [ServiceId] uniqueidentifier NOT NULL,
    [CountryCode] nchar(3) NOT NULL,
    CONSTRAINT [PK_ServiceCountries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ServiceCountries_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_ProviderAttributes_ProviderId] ON [ProviderAttributes] ([ProviderId]);

CREATE UNIQUE INDEX [IX_Providers_Nit] ON [Providers] ([Nit]);

CREATE UNIQUE INDEX [IX_ServiceCountries_ServiceId_CountryCode] ON [ServiceCountries] ([ServiceId], [CountryCode]);

CREATE INDEX [IX_Services_ProviderId] ON [Services] ([ProviderId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250510195916_InitialCreate', N'9.0.4');

ALTER TABLE [Services] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [Services] ADD [DeletedAt] datetime2 NULL;

ALTER TABLE [Services] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512010014_UpdateServiceEntity', N'9.0.4');

DROP INDEX [IX_Providers_Nit] ON [Providers];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512120902_RemoveUniqueConstraintFromNit', N'9.0.4');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512131516_AddQueryFilter', N'9.0.4');

ALTER TABLE [ProviderAttributes] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [ProviderAttributes] ADD [DeletedAt] datetime2 NULL;

ALTER TABLE [ProviderAttributes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512132852_AddAuditColumnsToProviderAttributes', N'9.0.4');

COMMIT;
GO
