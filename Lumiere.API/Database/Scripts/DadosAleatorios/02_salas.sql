SET NOCOUNT ON;

DECLARE @QtdSalas INT = 10;

;WITH Nums AS (
    SELECT TOP (@QtdSalas) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
    FROM sys.all_objects
)
INSERT INTO Salas (Nome, NumeroLinhas, NumeroColunas, Capacidade)
SELECT
    'Sala ' + CAST(n AS varchar(10)) + ' - ' +
    CHAR(65 + (ABS(CHECKSUM(NEWID())) % 26)) +
    CHAR(65 + (ABS(CHECKSUM(NEWID())) % 26)) AS Nome,
    Linhas,
    Colunas,
    Linhas * Colunas AS Capacidade
FROM (
    SELECT
        n,
        6 + (ABS(CHECKSUM(NEWID())) % 10)  AS Linhas,   -- 6..15
        8 + (ABS(CHECKSUM(NEWID())) % 13)  AS Colunas   -- 8..20
    FROM Nums
) X;