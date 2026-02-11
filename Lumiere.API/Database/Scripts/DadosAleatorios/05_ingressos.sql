DECLARE @SalaId INT = 1;
DECLARE @Linhas INT;
DECLARE @Colunas INT;

WHILE @SalaId <= 5
BEGIN
    SELECT
        @Linhas = NumeroLinhas,
        @Colunas = NumeroColunas
    FROM Salas
    WHERE Id = @SalaId;

    IF @Linhas IS NOT NULL AND @Colunas IS NOT NULL
    BEGIN
        -- Apagar assentos antigos da sala (evita duplicação)
        DELETE FROM Assentos WHERE SalaId = @SalaId;

        ;WITH Numeros AS
        (
            SELECT TOP (1000000)
                ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS N
            FROM sys.objects a
            CROSS JOIN sys.objects b
        ),
        Grid AS
        (
            SELECT
                (N / @Colunas) AS LinhaIndex,
                (N % @Colunas) + 1 AS Coluna
            FROM Numeros
            WHERE N < (@Linhas * @Colunas)
        )
        INSERT INTO Assentos (SalaId, Fileira, Coluna, TipoAssento)
        SELECT
            @SalaId,
            CHAR(65 + LinhaIndex) AS Fileira,
            Coluna,
            CASE
                WHEN (LinhaIndex = 0 OR LinhaIndex = @Linhas - 1)
                     AND (Coluna = 1 OR Coluna = @Colunas)
                    THEN 2  -- OBESO
                WHEN LinhaIndex = 0
                    THEN 1  -- CADEIRANTE
                ELSE 0      -- NORMAL
            END
        FROM Grid;
    END

    SET @SalaId = @SalaId + 1;
END

SELECT SalaId, COUNT(*) AS TotalAssentos
FROM Assentos
WHERE SalaId BETWEEN 1 AND 5
GROUP BY SalaId
ORDER BY SalaId;
