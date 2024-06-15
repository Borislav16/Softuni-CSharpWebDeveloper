CREATE DATABASE RailwaysDb

CREATE TABLE Passengers(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(80) NOT NULL,
)

CREATE TABLE Towns(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
)

CREATE TABLE RailwayStations(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL,
)

CREATE TABLE Trains(
	Id INT PRIMARY KEY IDENTITY,
	HourOfDeparture NVARCHAR(5) NOT NULL,
	HourOfArrival NVARCHAR(5) NOT NULL,
	DepartureTownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL,
	ArrivalTownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL,
)


CREATE TABLE TrainsRailwayStations(
	TrainId INT FOREIGN KEY REFERENCES Trains(Id) NOT NULL,
	RailwayStationId INT FOREIGN KEY REFERENCES RailwayStations(Id) NOT NULL,
	PRIMARY KEY(TrainId, RailwayStationId)
)

CREATE TABLE MaintenanceRecords(
	Id INT PRIMARY KEY IDENTITY,
	DateOfMaintenance DATE NOT NULL,
	Details NVARCHAR(2000) NOT NULL,
	TrainId INT FOREIGN KEY REFERENCES Trains(Id),
)

CREATE TABLE Tickets(
	Id INT PRIMARY KEY IDENTITY,
	Price DECIMAL(18,2) NOT NULL,
	DateOfDeparture DATE NOT NULL,
	DateOfArrival DATE NOT NULL,
	TrainId INT FOREIGN KEY REFERENCES Trains(Id),
	PassengerId INT FOREIGN KEY REFERENCES Passengers(Id)
)

--2
INSERT INTO Trains
VALUES ('07:00','19:00', 1, 3),
	   ('08:30','20:30', 5, 6),
	   ('09:00','21:00', 4, 8),
	   ('06:45','03:55', 27, 7),
	   ('10:15','12:15', 15, 5)

INSERT INTO TrainsRailwayStations
VALUES (36, 1), (36, 4), (36, 31), (36, 57), (36, 7),
	(37, 13), (37, 54), (37, 60), (37, 16),
	(38, 10), (38, 50), (38, 52), (38, 22),
	(39, 68), (39, 3), (39, 31), (39, 19),
	(40, 41), (40, 7), (40, 52), (40, 13)

INSERT INTO Tickets
VALUES (90.00, '2023-12-01', '2023-12-01', 36, 1),
	   (115.00, '2023-08-02', '2023-08-02', 37, 2),
	   (160.00, '2023-08-03', '2023-08-03', 38, 3),
	   (255.00, '2023-09-01', '2023-09-02', 39, 21),
	   (95.00, '2023-09-02', '2023-09-03', 40, 22)

--3
UPDATE Tickets
SET DateOfDeparture = DATEADD(DAY, 7, DateOfDeparture) 
WHERE DATEPART(MONTH, DateOfDeparture) > 10 

UPDATE Tickets
SET DateOfArrival = DATEADD(DAY, 7, DateOfArrival) 
WHERE DATEPART(MONTH, DateOfDeparture) > 10 


--4
DELETE FROM Tickets
WHERE TrainId = 7

DELETE FROM MaintenanceRecords
WHERE TrainId = 7

DELETE FROM TrainsRailwayStations
WHERE TrainId = 7

DELETE FROM Trains
WHERE DepartureTownId = 3

--5
SELECT DateOfDeparture, Price AS TicketPrice
FROM Tickets
ORDER BY Price, DateOfDeparture DESC

--6
SELECT p.[Name],
	t.Price AS TicketPrice,
	T.DateOfDeparture AS DateOfDeparture,
	t.TrainId AS TrainID
FROM Tickets AS t
JOIN Passengers AS p ON p.Id = t.PassengerId
ORDER BY Price DESC, p.[Name] 

--7
SELECT t.[Name], r.[Name]
FROM RailwayStations AS r
LEFT JOIN TrainsRailwayStations AS tr ON tr.RailwayStationId = r.Id
JOIN Towns AS t ON t.Id = r.TownId
WHERE  tr.TrainId IS NULL
ORDER BY t.[Name], r.[Name]

--8
SELECT TOP (3) tr.Id AS TrainId,
	tr.HourOfDeparture AS HourOfDeparture,
	t.Price AS TicketPrice,
	tn.Name AS Destination 
FROM Trains AS tr
JOIN Tickets AS t ON tr.Id = t.TrainId 
JOIN Towns AS tn ON tn.Id = tr.ArrivalTownId
WHERE (HourOfDeparture >= '08:00' AND HourOfDeparture <= '08:59')
	AND t.Price > 50.00
ORDER BY t.Price

--9
SELECT tn.[Name] AS TownName,
	COUNT(p.Id) AS PassengersCount
FROM Tickets AS t
JOIN Passengers AS p ON p.Id = t.PassengerId
JOIN Trains AS tr ON tr.Id = t.TrainId
JOIN Towns AS tn ON tn.Id = tr.ArrivalTownId
WHERE Price > 76.99
GROUP BY tn.[Name]
ORDER BY tn.[Name]


--10
SELECT t.Id AS TrainID,
	tn.Name AS DepartureTown,
	m.Details AS Details
FROM MaintenanceRecords AS m
JOIN Trains AS t ON t.Id = m.TrainId
JOIN Towns AS tn ON tn.Id = t.DepartureTownId
WHERE m.Details LIKE '%inspection%'
ORDER BY t.Id


--11

CREATE FUNCTION udf_TownsWithTrains(@name NVARCHAR(50))
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(t.Id)
		FROM Trains AS t
		JOIN Towns AS tna ON tna.Id = t.ArrivalTownId 
		JOIN Towns AS tnd ON tnd.Id = t.DepartureTownId 
		WHERE tna.Name = @name OR tnd.Name = @name)
END

--12
CREATE PROCEDURE usp_SearchByTown(@townName NVARCHAR(200))
AS
SELECT p.[Name], t.DateOfDeparture, tr.HourOfDeparture
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Trains AS tr ON tr.Id = t.TrainId
JOIN Towns AS tn ON tn.Id = tr.ArrivalTownId
WHERE tn.Name = @townName
ORDER BY t.DateOfDeparture DESC, p.[Name]
