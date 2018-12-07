
GO
DROP PROCEDURE IF EXISTS Systems_GetList;
GO
CREATE PROCEDURE Systems_GetList
AS
SELECT *
FROM Systems;


GO
DROP PROCEDURE IF EXISTS Systems_GetByName;
GO
CREATE PROCEDURE Systems_GetByName
(
	@SystemName varchar(64)
)
AS
SELECT *
FROM Systems
WHERE SystemName = @SystemName;


GO
DROP PROCEDURE IF EXISTS Systems_Insert;
GO
CREATE PROCEDURE Systems_Insert
(
	@SystemName varchar(64)
)
AS
INSERT INTO Systems(SystemName)
VALUES (@SystemName);







GO
DROP PROCEDURE IF EXISTS Planets_GetListInSystem;
GO
CREATE PROCEDURE Planets_GetListInSystem
(
	@SystemId int
)
AS
SELECT *
FROM Planets
WHERE SystemId = @SystemId;


GO
DROP PROCEDURE IF EXISTS Planets_GetByNumber;
GO
CREATE PROCEDURE Planets_GetByNumber
(
	@SystemId int,
	@PlanetNumber int
)
AS
SELECT *
FROM Planets
WHERE SystemId = @SystemId AND PlanetNumber = @PlanetNumber;


GO
DROP PROCEDURE IF EXISTS Planets_Insert;
GO
CREATE PROCEDURE Planets_Insert
(
	@SystemId int,
	@PlanetNumber int
)
AS
INSERT INTO Planets(SystemId, PlanetNumber)
VALUES (@SystemId, @PlanetNumber);







GO
DROP PROCEDURE IF EXISTS Moons_GetListInPlanet;
GO
CREATE PROCEDURE Moons_GetListInPlanet
(
	@PlanetId int
)
AS
SELECT *
FROM Moons
WHERE PlanetId = @PlanetId;

GO
DROP PROCEDURE IF EXISTS Moons_GetByNumber;
GO
CREATE PROCEDURE Moons_GetByNumber
(
	@PlanetId int,
	@MoonNumber int
)
AS
SELECT *
FROM Moons
WHERE PlanetId = @PlanetId AND MoonNumber = @MoonNumber;

GO
DROP PROCEDURE IF EXISTS Moons_Insert;
GO
CREATE PROCEDURE Moons_Insert
(
	@PlanetId int,
	@MoonNumber int
)
AS
INSERT INTO Moons(PlanetId, MoonNumber)
VALUES (@PlanetId, @MoonNumber);







GO
DROP PROCEDURE IF EXISTS MoonOres_GetListInMoon;
GO
CREATE PROCEDURE MoonOres_GetListInMoon
(
	@MoonId int
)
AS
SELECT *
FROM MoonOres
WHERE MoonId = @MoonId;

GO
DROP PROCEDURE IF EXISTS MoonOres_DeleteAllInMoon
GO
CREATE PROCEDURE MoonOres_DeleteAllInMoon
(
	@MoonId int
)
AS
DELETE FROM MoonOres
WHERE MoonId = @MoonId;

GO
DROP PROCEDURE IF EXISTS MoonOres_Insert
GO
CREATE PROCEDURE MoonOres_Insert
(
	@MoonId int,
	@OreId int,
	@Percentage int
)
AS
INSERT INTO MoonOres(MoonId, OreId, Percentage)
VALUES (@MoonId, @OreId, @Percentage);








GO
DROP PROCEDURE IF EXISTS Ore_GetList;
GO
CREATE PROCEDURE Ore_GetList
AS
SELECT *
FROM Ores;

GO
DROP PROCEDURE IF EXISTS Ore_GetByName;
GO
CREATE PROCEDURE Ore_GetByName
(
	@OreName varchar(64)
)
AS
SELECT *
FROM Ores
WHERE OreName = @OreName;

GO
DROP PROCEDURE IF EXISTS OreClass_GetList;
GO
CREATE PROCEDURE OreClass_GetList
AS
SELECT *
FROM OreClass;

GO
DROP PROCEDURE IF EXISTS OreClass_GetByName
GO
CREATE PROCEDURE OreClass_GetByName
(
	@OreClassName varchar(64)
)
AS
SELECT *
FROM OreClass
WHERE OreClassName = @OreClassName;








GO
DROP PROCEDURE IF EXISTS Search;
GO
CREATE PROCEDURE Search
(
	@Switch0 int,
	@Switch1 int,
	@Switch2 int,
	@Switch3 int,

	@System varchar(64),
	@Ore varchar(64),
	@OreClassId varchar(64),
	@Percentage int
)
AS
SELECT Moons.MoonId, SystemName AS System, PlanetNumber AS Planet, MoonNumber AS Moon, OreName AS Ore, Percentage, OreClassName AS OreClass
FROM Systems
JOIN Planets ON Systems.SystemId = Planets.SystemId
JOIN Moons ON Planets.PlanetId = Moons.PlanetId
JOIN MoonOres ON Moons.MoonId = MoonOres.MoonId
JOIN Ores ON MoonOres.OreId = Ores.OreId
JOIN OreClass ON Ores.OreClassId = OreClass.OreClassId
WHERE Moons.MoonId IN (
SELECT MoonId
FROM MoonOres
JOIN Ores ON MoonOres.OreId = Ores.OreId
WHERE ((1 = @Switch1 OR Ores.OreName = @Ore) AND (1 = @Switch2 OR Ores.OreClassId >= @OreClassId) AND (1 = @Switch3 OR MoonOres.Percentage >= @Percentage))
GROUP BY MoonId
HAVING Count(*) >= 1
)
AND ((1 = @Switch0 OR SystemName = @System))
ORDER BY Systems.SystemName, Planets.PlanetNumber ASC, Moons.MoonNumber ASC, OreClass.OreClassId DESC, Percentage DESC, Ore ASC;

