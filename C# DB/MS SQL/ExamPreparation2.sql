CREATE DATABASE Accounting

CREATE TABLE Countries(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(10) NOT NULL,
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	StreetName NVARCHAR(20) NOT NULL,
	StreetNumber INT,
	PostCode INT NOT NULL,
	City NVARCHAR(25) NOT NULL,
	CountryId INT FOREIGN KEY REFERENCES Countries(Id) NOT NULL
)

CREATE TABLE Vendors(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(25) NOT NULL,
	NumberVAT NVARCHAR(15) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
)

CREATE TABLE Clients(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(25) NOT NULL,
	NumberVAT NVARCHAR(15) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(10) NOT NULL,
)

CREATE TABLE Products(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(35) NOT NULL,
	Price DECIMAL(18,2) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	VendorId INT FOREIGN KEY REFERENCES Vendors(Id) NOT NULL,
)

CREATE TABLE Invoices(
	Id INT PRIMARY KEY IDENTITY,
	Number INT UNIQUE NOT NULL,
	IssueDate DATETIME2 NOT NULL,
	DueDate DATETIME2 NOT NULL,
	Amount DECIMAL(18,2) NOT NULL,
	Currency NVARCHAR(5) NOT NULL,
	ClientId INT FOREIGN KEY REFERENCES Clients(Id) NOT NULL,
)

CREATE TABLE ProductsClients(
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	ClientId INT FOREIGN KEY REFERENCES Clients(Id),
	PRIMARY KEY(ProductId, ClientId)
)

--2
INSERT INTO Products
VALUES ('SCANIA Oil Filter XD01', 78.69, 1, 1),
	   ('MAN Air Filter XD01', 97.38, 1, 5), 
	   ('DAF Light Bulb 05FG87', 55.00, 2, 13), 
	   ('ADR Shoes 47-47.5', 49.85, 3, 5), 
	   ('Anti-slip pads S', 5.87, 5, 7)

INSERT INTO Invoices
VALUES (1219992181, '2023-03-01', '2023-04-30', 180.96, 'BGN', 3),
       (1729252340, '2022-11-06', '2023-01-04', 158.18, 'EUR', 13),
	   (1950101013, '2023-02-17', '2023-04-18', 615.15, 'USD', 19)


--3
UPDATE Invoices
SET DueDate = '2023-04-01'
WHERE DATEPART(MONTH, IssueDate) LIKE 11 AND DATEPART(YEAR, IssueDate) LIKE 2022

UPDATE Clients
SET AddressId = 3
WHERE [Name] LIKE '%CO%'


--4
DELETE FROM Invoices WHERE ClientId = 11
DELETE FROM ProductsClients WHERE ClientId = 11
DELETE FROM Clients WHERE LEFT(NumberVAT, 2) = 'IT'

-- ID 11
SELECT * FROM Clients WHERE LEFT(NumberVAT, 2) = 'IT'
SELECT * FROM ProductsClients WHERE ClientId = 11

--5
SELECT Number, Currency
FROM Invoices
ORDER BY Amount DESC, DueDate


--6
SELECT p.Id, p.[Name], p.Price, c.Name AS [CategoryName]
FROM Products AS p
JOIN Categories AS c ON c.Id = P.CategoryId
WHERE c.Name IN ('ADR', 'Others')
ORDER BY p.Price DESC


--7
SELECT c.Id, c.[Name] AS [Client] ,
	CONCAT_WS(' ',a.StreetName, CONCAT_WS(', ',a.StreetNumber,A.City,A.PostCode,'Spain')) AS [Address]
FROM Clients AS c
JOIN Addresses AS a ON a.Id = c.AddressId
WHERE c.Id NOT IN(SELECT ClientId
FROM ProductsClients)
ORDER BY c.[Name] 


--8
SELECT TOP(7) i.Number, i.Amount, c.Name
FROM INVOICES AS i
JOIN Clients AS c ON c.Id = i.ClientId
WHERE IssueDate < '2023-01-01' 
	AND Currency = 'EUR' OR Amount > 500
	AND c.NumberVAT LIKE 'DE%'
ORDER BY i.Number, i.Amount DESC



--9
SELECT c.Name, Max(p.Price), c.NumberVAT
FROM Clients AS c
JOIN ProductsClients AS pc ON pc.ClientId = c.Id
JOIN Products AS p ON p.Id = pc.ProductId
WHERE c.[Name] NOT LIKE '%KG'
GROUP BY c.Name, c.NumberVAT
ORDER BY Max(p.Price) DESC

--10
SELECT c.Name, FLOOR(AVG(p.Price))
FROM Clients AS c
JOIN ProductsClients AS pc ON pc.ClientId = c.Id
JOIN Products AS p ON p.Id = pc.ProductId
JOIN Vendors AS v ON v.Id = p.VendorId
WHERE v.NumberVAT LIKE '%FR%'
GROUP BY c.Name
ORDER BY AVG(p.Price), c.Name DESC

--11
CREATE FUNCTION udf_ProductWithClients(@name NVARCHAR(50)) 
RETURNS INT
AS
BEGIN
	DECLARE @ID INT =(
		SELECT Id
		FROM Products
		WHERE Name = @name
	)
	RETURN (SELECT COUNT(ClientId)
	FROM ProductsClients
	WHERE ProductId = @ID)
END


SELECT dbo.udf_ProductWithClients('DAF FILTER HU12103X')


--12
CREATE PROCEDURE usp_SearchByCountry(@country NVARCHAR(50)) 
AS
SELECT v.Name AS [Vendor],
	v.NumberVAT AS [VAT],
	CONCAT_WS(' ',a.StreetName, a.StreetNumber) AS [Street Info],
	CONCAT_WS(' ', a.City, a.PostCode) AS [City Info]
FROM Vendors AS v
JOIN Addresses AS a ON a.Id = v.AddressId
JOIN Countries AS c ON c.Id = a.CountryId
WHERE c.Name = @country
ORDER BY v.Name, a.City
