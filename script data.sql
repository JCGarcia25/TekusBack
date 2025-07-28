-- Desactivar constraints temporalmente en SQL Server
ALTER TABLE [ServiceCountries] NOCHECK CONSTRAINT ALL;
ALTER TABLE [Services] NOCHECK CONSTRAINT ALL;
ALTER TABLE [ProviderAttributes] NOCHECK CONSTRAINT ALL;
ALTER TABLE [Providers] NOCHECK CONSTRAINT ALL;
-- Proveedor 1 al 10
-- Este bloque inserta 10 proveedores con servicios y países (códigos válidos de África)
-- Usamos una lista representativa de códigos: DZ, AO, BJ, BW, BF, BI, CV, CM, CF, TD
DECLARE @CreatedAt DATETIME2 = GETDATE();
DECLARE @Providers TABLE (Id UNIQUEIDENTIFIER, Nit NVARCHAR(20), Name NVARCHAR(100), Email NVARCHAR(200));
INSERT INTO @Providers VALUES
(NEWID(), '100001', 'TechSolutions', 'tech@solutions.com'),
(NEWID(), '100002', 'BizConsulting', 'contact@biz.com'),
(NEWID(), '100003', 'LogistiCorp', 'logistics@corp.com'),
(NEWID(), '100004', 'Financia', 'hello@financia.com'),
(NEWID(), '100005', 'Marketify', 'team@marketify.com'),
(NEWID(), '100006', 'CodeMaster', 'support@codemaster.com'),
(NEWID(), '100007', 'TalentWay', 'hire@talentway.com'),
(NEWID(), '100008', 'LegalTrust', 'info@legaltrust.com'),
(NEWID(), '100009', 'DesignPlus', 'contact@designplus.com'),
(NEWID(), '100010', 'LearnPro', 'courses@learnpro.com');
DECLARE @i INT = 1;
DECLARE @ProviderId UNIQUEIDENTIFIER;
DECLARE @ServiceId UNIQUEIDENTIFIER;
-- Variables para capturar los datos del proveedor
DECLARE @Nit NVARCHAR(20), @Name NVARCHAR(100), @Email NVARCHAR(200);

WHILE @i <= 10
BEGIN
    SELECT TOP 1 
        @ProviderId = Id,
        @Nit = Nit,
        @Name = Name,
        @Email = Email
    FROM @Providers;

    DELETE FROM @Providers WHERE Id = @ProviderId;

    INSERT INTO [Providers] ([Id], [Nit], [Name], [Email], [CreatedAt], [IsDeleted], [DeletedAt])
    VALUES (@ProviderId, @Nit, @Name, @Email, @CreatedAt, 0, NULL);

    INSERT INTO [ProviderAttributes] ([Id], [ProviderId], [Key], [Value], [CreatedAt], [IsDeleted], [DeletedAt])
    VALUES (NEWID(), @ProviderId, 'PhoneNumber', '+123-456789', @CreatedAt, 0, NULL);

    SET @ServiceId = NEWID();
    INSERT INTO [Services] ([Id], [ProviderId], [Name], [HourlyRate], [CreatedAt], [IsDeleted], [DeletedAt])
    VALUES (@ServiceId, @ProviderId, CONCAT('Service-', @i), 100 + (@i * 10), @CreatedAt, 0, NULL);

    INSERT INTO [ServiceCountries] ([Id], [ServiceId], [CountryCode])
    VALUES 
        (NEWID(), @ServiceId, 'DZ'),
        (NEWID(), @ServiceId, 'AO'),
        (NEWID(), @ServiceId, 'BJ');

    SET @i += 1;
END


-- Reactivar constraints
ALTER TABLE [Providers] CHECK CONSTRAINT ALL;
ALTER TABLE [ProviderAttributes] CHECK CONSTRAINT ALL;
ALTER TABLE [Services] CHECK CONSTRAINT ALL;
ALTER TABLE [ServiceCountries] CHECK CONSTRAINT ALL;