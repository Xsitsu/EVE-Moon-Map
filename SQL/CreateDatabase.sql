DROP TABLE IF EXISTS MoonOres;

DROP TABLE IF EXISTS Moons;
DROP TABLE IF EXISTS Planets;
DROP TABLE IF EXISTS Systems;

DROP TABLE IF EXISTS Ores;
DROP TABLE IF EXISTS OreClass;


CREATE TABLE Systems
(
	SystemId INTEGER IDENTITY(1,1) PRIMARY KEY,
	SystemName varchar(128) NOT NULL,
);

CREATE TABLE Planets
(
	PlanetId INTEGER IDENTITY(1,1) PRIMARY KEY,
	SystemId INTEGER NOT NULL,
	PlanetNumber INTEGER NOT NULL,

	FOREIGN KEY(SystemId) REFERENCES Systems(SystemId)
);

CREATE TABLE Moons
(
	MoonId INTEGER IDENTITY(1,1) PRIMARY KEY,
	PlanetId INTEGER NOT NULL,
	MoonNumber INTEGER NOT NULL,

	FOREIGN KEY(PlanetId) REFERENCES Planets(PlanetId)
);




CREATE TABLE OreClass
(
	OreClassId INTEGER IDENTITY(1,1) PRIMARY KEY,
	OreClassName varchar(64) NOT NULL
);

CREATE TABLE Ores
(
	OreId INTEGER IDENTITY(1,1) PRIMARY KEY,
	OreClassId INTEGER NOT NULL,
	OreName varchar(64),

	FOREIGN KEY(OreClassId) REFERENCES OreClass(OreClassId)
);



CREATE TABLE MoonOres
(
	MoonId INTEGER NOT NULL,
	OreId INTEGER NOT NULL,
	Percentage INTEGER NOT NULL,

	PRIMARY KEY(MoonId, OreId)
);





INSERT INTO OreClass(OreClassName)
VALUES ('Non-Moon'),
('Ubiquitous'),
('Common'),
('Uncommon'),
('Rare'),
('Exceptional');


INSERT INTO Ores(OreClassId, OreName)
VALUES (1, 'Stable Veldspar'),
(1, 'Glossy Scordite'),
(1, 'Opulent Pyroxeres'),
(1, 'Sparkling Plagioclase'),
(1, 'Platinoid Omber'),
(1, 'Resplendant Kernite'),
(1, 'Immaculate Jaspet'),
(1, 'Scintillating Hemorphite'),
(1, 'Lustrous Hedbergite'),
(1, 'Brilliant Gneiss'),
(1, 'Jet Ochre'),
(1, 'Dazzling Spodumain'),
(1, 'Pellucid Crokite'),
(1, 'Flawless Arkonor'),
(1, 'Cubic Bistot'),
(1, 'Mercoxit'),

(2, 'Bitumens'),
(2, 'Coesite'),
(2, 'Sylvite'),
(2, 'Zeolites'),

(3, 'Cobaltite'),
(3, 'Euxenite'),
(3, 'Scheelite'),
(3, 'Titanite'),

(4, 'Chromite'),
(4, 'Otavite'),
(4, 'Sperrylite'),
(4, 'Vanadinite'),

(5, 'Carnotite'),
(5, 'Cinnabar'),
(5, 'Pollucite'),
(5, 'Zircon'),

(6, 'Loparite'),
(6, 'Monazite'),
(6, 'Xenotime'),
(6, 'Ytterbite');
