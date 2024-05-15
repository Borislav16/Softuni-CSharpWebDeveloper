CREATE DATABASE Zoo

USE Zoo

CREATE TABLE Owners(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	[Address] VARCHAR(50),
)


CREATE TABLE AnimalTypes(
	Id INT PRIMARY KEY IDENTITY,
	AnimalType VARCHAR(30) NOT NULL,
)

CREATE TABLE Cages(
	Id INT PRIMARY KEY IDENTITY,
	AnimalTypeId INT FOREIGN KEY REFERENCES AnimalTypes(Id) NOT NULL,
)

CREATE TABLE Animals(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(30) NOT NULL,
	BirthDate DATE NOT NULL,
	OwnerId INT FOREIGN KEY REFERENCES Owners(Id),
	AnimalTypeId INT FOREIGN KEY REFERENCES AnimalTypes(Id) NOT NULL,
)

CREATE TABLE AnimalsCages(
	CageId INT FOREIGN KEY REFERENCES Cages(Id) NOT NULL,
	AnimalId INT FOREIGN KEY REFERENCES Animals(Id) NOT NULL,
	PRIMARY KEY(CageId, AnimalId)
)

CREATE TABLE VolunteersDepartments(
	Id INT PRIMARY KEY IDENTITY,
	DepartmentName VARCHAR(30) NOT NULL,
)

CREATE TABLE Volunteers(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	[Address] VARCHAR(50),
	AnimalId INT FOREIGN KEY REFERENCES Animals(Id),
	DepartmentId INT FOREIGN KEY REFERENCES VolunteersDepartments(Id) NOT NULL,
)

--2
INSERT INTO Volunteers
VALUES ('Anita Kostova', '0896365412', 'Sofia, 5 Rosa str.', 15, 1),
	   ('Dimitur Stoev', '0877564223', null, 42, 4),
	   ('Kalina Evtimova', '0896321112', 'Silistra, 21 Breza str.', 9, 7),
	   ('Stoyan Tomov', '0898564100', 'Montana, 1 Bor str.', 18, 8),
	   ('Boryana Mileva', '0888112233', null, 31, 5)


INSERT INTO Animals
VALUES ('Giraffe', '2018-09-21', 21, 1),
	   ('Harpy Eagle', '2015-04-17', 15, 3),
	   ('Hamadryas Baboon', '2017-11-02', NULL, 1),
	   ('Tuatara', '2021-06-30', 2, 4)


--3
UPDATE Animals
SET OwnerId = 4
WHERE OwnerId IS NULL


--4
DELETE FROM Volunteers 
WHERE DepartmentId = 2

DELETE FROM VolunteersDepartments
WHERE Id = 2

--5
SELECT [Name], PhoneNumber, [Address], AnimalId, DepartmentId
FROM Volunteers	
ORDER BY [Name], AnimalId, DepartmentId


--6
SELECT [Name], at.AnimalType, FORMAT(BirthDate, 'dd.MM.yyyy')
FROM Animals AS a
JOIN AnimalTypes AS at ON at.Id = a.AnimalTypeId
ORDER BY [Name]


--7
SELECT TOP(5) o.[Name], COUNT(*)
FROM Owners AS o
JOIN Animals AS a ON a.OwnerId = o.Id
WHERE o.Id IN (SELECT OwnerId
FROM Animals)
GROUP BY o.Name
ORDER BY COUNT(*) DESC, o.[Name]


--8
SELECT CONCAT_WS('-',o.[Name], a.[Name]) AS OwnersAnimals,
	o.PhoneNumber, c.Id
FROM Owners AS o
JOIN Animals AS a ON a.OwnerId = o.Id
JOIN AnimalTypes AS [at] ON at.Id = a.AnimalTypeId
JOIN AnimalsCages AS ac ON ac.AnimalId = a.Id
JOIN Cages AS c ON ac.CageId = c.Id 
WHERE [at].Id = 1
ORDER BY o.[Name], a.[Name] DESC


--9
SELECT v.Name, v.PhoneNumber, SUBSTRING(v.[Address], CHARINDEX(',', v.[Address], 0) + 1, LEN(v.[Address])) 
 AS [Address]
FROM Volunteers AS v
JOIN VolunteersDepartments AS vd ON vd.Id = v.DepartmentId
WHERE vd.DepartmentName = 'Education program assistant' 
	AND v.Address LIKE '%SOFIA%' 
ORDER BY v.[Name]

--10
SELECT [Name], DATEPART(YEAR,BirthDate) AS BirthYear, at.AnimalType
FROM Animals AS a
JOIN AnimalTypes AS at ON at.Id = a.AnimalTypeId
WHERE OwnerId IS NULL
	AND BirthDate < '01/01/2023' AND BirthDate >= '01/01/2018'
	AND at.Id != 3
ORDER BY [Name]

--11
CREATE FUNCTION udf_GetVolunteersCountFromADepartment (@VolunteersDepartment VARCHAR(60))
RETURNS INT 
AS
BEGIN
	DECLARE @ID INT =(
	SELECT Id
	FROM VolunteersDepartments
	WHERE DepartmentName = @VolunteersDepartment)
	
	RETURN (SELECT COUNT(DepartmentId)
	FROM Volunteers 
	WHERE DepartmentId = @ID)
END 

SELECT dbo.udf_GetVolunteersCountFromADepartment('Zoo events')

--12
CREATE PROCEDURE usp_AnimalsWithOwnersOrNot(@AnimalName VARCHAR(50))
AS
	
	SELECT a.[Name],
	Case
		WHEN a.OwnerId IS NULL THEN 'For adoption'
		ELSE o.Name
	END AS OwnersName
	FROM Animals AS a
	LEFT JOIN Owners AS o ON o.Id = a.OwnerId
	WHERE a.[Name] = @AnimalName
