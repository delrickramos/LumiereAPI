;WITH Sess AS (
    SELECT Id AS SessaoId, SalaId, PrecoBase
    FROM Sessoes
),
Tipos AS (
    SELECT Id AS TipoIngressoId, DescontoPercentual
    FROM TiposIngresso
),
AssentosSala AS (
    SELECT a.Id AS AssentoId, a.SalaId
    FROM Assentos a
)
INSERT INTO Ingressos (PrecoFinal, Status, SessaoId, AssentoId, TipoIngressoId)
SELECT
    CAST(s.PrecoBase * (1 - (t.DescontoPercentual / 100.0)) AS decimal(18,2)) AS PrecoFinal,
    st.StatusInt AS Status,  -- 0=Reservado, 1=Cancelado, 2=Confirmado
    s.SessaoId,
    x.AssentoId,
    t.TipoIngressoId
FROM Sess s
CROSS APPLY (
    -- Ocupação aleatória por sessão: 20% a 90% dos assentos
    SELECT CAST(0.2 + (ABS(CHECKSUM(NEWID())) % 71) / 100.0 AS decimal(5,2)) AS Taxa
) occ
CROSS APPLY (
    SELECT COUNT(*) AS TotalAssentos
    FROM AssentosSala a
    WHERE a.SalaId = s.SalaId
) cnt
CROSS APPLY (
    SELECT TOP (
        CONVERT(int,
            CASE
                WHEN cnt.TotalAssentos = 0 THEN 0
                ELSE
                    CASE
                        WHEN CEILING(cnt.TotalAssentos * occ.Taxa) < 1 THEN 1
                        ELSE CEILING(cnt.TotalAssentos * occ.Taxa)
                    END
            END
        )
    )
        a.AssentoId
    FROM AssentosSala a
    WHERE a.SalaId = s.SalaId
    ORDER BY NEWID()
) x
CROSS APPLY (SELECT TOP 1 * FROM Tipos ORDER BY NEWID()) t
CROSS APPLY (
    SELECT ABS(CHECKSUM(NEWID())) % 3 AS StatusInt
) st
WHERE cnt.TotalAssentos > 0
AND NOT EXISTS (
    SELECT 1
    FROM Ingressos i
    WHERE i.SessaoId = s.SessaoId
      AND i.AssentoId = x.AssentoId
);