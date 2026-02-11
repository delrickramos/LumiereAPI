SET NOCOUNT ON;

DECLARE @SalaId INT = 1;
DECLARE @Linhas INT;
DECLARE @Colunas INT;

WHILE @SalaId <= 10
BEGIN
    -- Lê dimensões da sala
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
        INSERT INTO Assentos (SalaId, Nome, Fileira, Coluna, TipoAssento)
        SELECT
            @SalaId,
            CHAR(65 + LinhaIndex) + RIGHT('00' + CAST(Coluna AS varchar(2)), 2) AS Nome, -- ex: A01
            CHAR(65 + LinhaIndex) AS Fileira,
            Coluna,
            CASE
                WHEN (LinhaIndex = 0 OR LinhaIndex = @Linhas - 1)
                     AND (Coluna = 1 OR Coluna = @Colunas)
                    THEN 2  -- OBESO (cantos: primeira/última fileira e primeira/última coluna)
                WHEN LinhaIndex = 0
                    THEN 1  -- CADEIRANTE (primeira fileira)
                ELSE 0      -- NORMAL
            END
        FROM Grid;
    END

    -- próxima sala
    SET @SalaId = @SalaId + 1;
END

SELECT SalaId, COUNT(*) AS TotalAssentos
FROM Assentos
WHERE SalaId BETWEEN 1 AND 10
GROUP BY SalaId
ORDER BY SalaId;