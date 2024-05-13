CREATE DATABASE Boardgames

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	StreetName NVARCHAR(100) NOT NULL,
	StreetNumber INT NOT NULL,
	Town NVARCHAR(30) NOT NULL,
	Country NVARCHAR(50) NOT NULL,
	ZIP INT NOT NULL
)

CREATE TABLE Publishers(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
	Website NVARCHAR(40),
	Phone NVARCHAR(20),
)

CREATE TABLE PlayersRanges(
	Id INT PRIMARY KEY IDENTITY,
	PlayersMin INT NOT NULL,
	PlayersMax INT NOT NULL,
)

CREATE TABLE Boardgames(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	YearPublished INT NOT NULL,
	Rating DECIMAL(18,2) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	PublisherId INT FOREIGN KEY REFERENCES Publishers(Id) NOT NULL,
	PlayersRangeId INT FOREIGN KEY REFERENCES PlayersRanges(Id) NOT NULL,
)

CREATE TABLE Creators(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL,
)

CREATE TABLE CreatorsBoardgames(
	CreatorId INT FOREIGN KEY REFERENCES Creators(Id) NOT NULL,
	BoardgameId INT FOREIGN KEY REFERENCES Boardgames(Id) NOT NULL,
	PRIMARY KEY(CreatorId, BoardgameId)
)


--2
INSERT INTO Boardgames (Name, YearPublished, Rating, CategoryId, PublisherId, PlayersRangeId)
VALUES('Deep Blue', 2019, 5.67, 1, 15, 7),
	  ('Paris', 2016, 9.78, 7, 1, 5),
	  ('Catan: Starfarers', 2021, 9.87, 7, 13, 6),
	  ('Bleeding Kansas' , 2020, 3.25, 3, 7, 4),
	  ('One Small Step', 2019, 5.75, 5, 9, 2)

INSERT INTO Publishers
VALUES('Agman Games', 5, 'www.agmangames.com', '+16546135542'),
      ('Amethyst Games', 7, 'www.amethystgames.com', '+15558889992'),
	  ('BattleBooks', 13, 'www.battlebooks.com', '+12345678907')


--3
UPDATE PlayersRanges
SET PlayersMax += 1
WHERE PlayersMin = 2 AND PlayersMax = 2

UPDATE Boardgames
SET [Name] = CONCAT([Name],'V2')
WHERE YearPublished >= 2020


--4
DELETE FROM CreatorsBoardgames
WHERE BoardgameId IN (1, 16, 31, 47)

DELETE FROM Boardgames
WHERE PublisherId IN (1, 16)

DELETE FROM Publishers
WHERE AddressId = 5

DELETE FROM Addresses
WHERE LEFT(TOWN, 1) = 'L'


-- 5
SELECT * FROM Addresses
WHERE LEFT(TOWN, 1) = 'L'

-- 1, 16
SELECT * FROM Publishers
WHERE AddressId = 5

-- 1, 16, 31, 47
SELECT * FROM Boardgames
WHERE PublisherId IN (1, 16)


--5 
SELECT [Name], Rating
FROM Boardgames
ORDER BY YearPublished, [Name] DESC

--6
SELECT B.Id, b.[Name], b.YearPublished, c.[Name] AS [CategoryName]
FROM Boardgames AS b
JOIN Categories as c ON c.Id = B.CategoryId
WHERE c.Name IN ('Wargames', 'Strategy Games')
ORDER BY b.YearPublished DESC

--7
SELECT Id, FirstName + ' ' + LastName AS CreatorName, Email
FROM Creators
WHERE Id NOT IN(
	SELECT CreatorId FROM CreatorsBoardgames
)
ORDER BY CreatorName ASC


--8
SELECT TOP(5) b.[Name], b.Rating, c.Name AS CategoryName
FROM Boardgames AS b
JOIN Categories AS c ON c.Id = b.CategoryId 
JOIN PlayersRanges AS pr ON pr.Id = b.PlayersRangeId 
WHERE b.Rating > 7
	  AND (b.Name LIKE '%a%' OR b.Rating > 7.50) 
	  AND pr.PlayersMin >= 2 
	  AND pr.PlayersMax <= 5
ORDER BY b.Name, b.Rating



--9
SELECT FullName, Email, Rating FROM 
(SELECT  CONCAT_WS(' ', FirstName, LastName) AS [FullName],  
		Email, b.Rating,
		RANK() OVER (PARTITION BY Email ORDER BY rating DESC) AS GameRating
	FROM Creators c
	JOIN CreatorsBoardgames cb ON cb.CreatorId = c.Id
	JOIN Boardgames b ON b.Id = cb.BoardgameId
	WHERE Email LIKE '%.com') AS SUBQ
WHERE GameRating = 1


--10
SELECT LastName, CEILING(AVG(b.Rating)) AS [AverageRating], p.Name AS [PublisherName]
FROM Creators AS c
JOIN CreatorsBoardgames cb ON cb.CreatorId = c.Id 
JOIN Boardgames b ON b.Id = cb.BoardgameId
JOIN Publishers p ON p.Id = b.PublisherId
WHERE c.Id IN (SELECT CreatorId FROM CreatorsBoardgames)
	AND P.Name = 'Stonemaier Games'
GROUP BY c.LastName, p.Name
ORDER BY AVG(b.Rating) DESC


--11
CREATE FUNCTION udf_CreatorWithBoardgames(@name NVARCHAR(30))
RETURNS INT AS
BEGIN
	DECLARE @result INT =
	(SELECT COUNT(BoardgameId) 
	FROM CreatorsBoardgames cbg
	JOIN Creators c ON c.Id = cbg.CreatorId
	WHERE c.FirstName = @name)

	RETURN @result
END

SELECT dbo.udf_CreatorWithBoardgames('Bruno')


--12
CREATE PROCEDURE usp_SearchByCategory(@category NVARCHAR(50))
AS

DECLARE @categoryId INT =
	(SELECT Id
	FROM Categories
	WHERE Name = @category
	)

SELECT b.Name, b.YearPublished, b.Rating, @category, p.Name AS [PublisherName],
	CONCAT(pr.PlayersMin,' people' ) AS [MinPlayers], CONCAT(pr.PlayersMax,' people' ) AS [MaxPlayers] 
FROM Boardgames as b
JOIN Publishers AS p ON p.Id = b.PublisherId
JOIN PlayersRanges AS pr ON pr.Id = b.PlayersRangeId
WHERE b.CategoryId = @categoryId
ORDER BY p.Name , b.YearPublished DESC

