
GO
DROP PROCEDURE IF EXISTS Systems_GetList;
GO
CREATE PROCEDURE Systems_GetList
AS
SELECT *
FROM Systems;






GO
DROP PROCEDURE IF EXISTS Ore_GetList;
GO
CREATE PROCEDURE Ore_GetList
AS
SELECT *
FROM Ores;