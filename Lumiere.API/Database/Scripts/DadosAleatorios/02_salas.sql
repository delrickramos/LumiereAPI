SET NOCOUNT ON;

DECLARE @QtdFilmes INT = 10;

;WITH Nums AS (
    SELECT TOP (@QtdFilmes) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
    FROM sys.all_objects
)
INSERT INTO Filmes (Titulo, DuracaoMinutos, ClassificacaoIndicativa, Sinopse, Direcao, Distribuidora, GeneroId)
SELECT
    'Filme ' + CAST(n AS varchar(10)) + ' - ' +
    CHAR(65 + (ABS(CHECKSUM(NEWID())) % 26)) +
    CHAR(65 + (ABS(CHECKSUM(NEWID())) % 26)),
    80 + (ABS(CHECKSUM(NEWID())) % 61), -- 80..140
    CASE ABS(CHECKSUM(NEWID())) % 6
        WHEN 0 THEN '0'
        WHEN 1 THEN '10'
        WHEN 2 THEN '12'
        WHEN 3 THEN '14'
        WHEN 4 THEN '16'
        ELSE '18'
    END,
    REPLICATE('Sinopse ', 25),
    'Diretor ' + CAST(ABS(CHECKSUM(NEWID())) % 200 AS varchar(10)),
    'Distribuidora ' + CAST(ABS(CHECKSUM(NEWID())) % 50 AS varchar(10)),
    (SELECT TOP 1 Id FROM Generos ORDER BY NEWID())
FROM Nums;
