DECLARE @QtdSessoes INT = 50;

;WITH FilmesBase AS (
    SELECT Id AS FilmeId, DuracaoMinutos FROM Filmes
),
SalasBase AS (
    SELECT Id AS SalaId FROM Salas
),
Nums AS (
    SELECT TOP (@QtdSessoes) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
    FROM sys.all_objects a
    CROSS JOIN sys.all_objects b
)
INSERT INTO Sessoes (DataHoraInicio, DataHoraFim, Idioma, PrecoBase, SalaId, FormatoSessaoId, FilmeId)
SELECT
    dt.Inicio,
    DATEADD(MINUTE, f.DuracaoMinutos, dt.Inicio),
    CASE ABS(CHECKSUM(NEWID())) % 4
        WHEN 0 THEN 'pt-BR'
        WHEN 1 THEN 'en-US'
        WHEN 2 THEN 'es-ES'
        ELSE 'fr-FR'
    END,
    CAST(20 + (ABS(CHECKSUM(NEWID())) % 31) AS decimal(18,2)), -- 20..50
    s.SalaId,
    (SELECT TOP 1 Id FROM FormatosSessao ORDER BY NEWID()),
    f.FilmeId
FROM Nums
CROSS APPLY (SELECT TOP 1 * FROM FilmesBase ORDER BY NEWID()) f
CROSS APPLY (SELECT TOP 1 * FROM SalasBase ORDER BY NEWID()) s
CROSS APPLY (
    SELECT DATEADD(
        HOUR,
        12 + (ABS(CHECKSUM(NEWID())) % 11),
        DATEADD(
            DAY,
            ABS(CHECKSUM(NEWID())) % 30,
            SYSDATETIMEOFFSET()
        )
    ) AS Inicio
) dt;
