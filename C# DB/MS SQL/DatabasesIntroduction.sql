--1
CREATE DATABASE [Minions]

--2
CREATE TABLE [Minions](
  [Id] INT PRIMARY KEY,
  [Name] NVARCHAR(50) NOT NULL,
  [Age] INT NOT NULL
)

CREATE TABLE [Towns](
  [Id] INT PRIMARY KEY,
  [Name] NVARCHAR(50) NOT NULL
)

--3
ALTER TABLE [Minions]
ADD [TownId] INT FOREIGN KEY REFERENCES [Towns] ([Id]) NOT NULL


--4
ALTER TABLE [Minions]
ALTER COLUMN [Age] INT

INSERT INTO [TOWNS] ([Id], [Name])
	Values (1, 'Sofia'),
	       (2, 'Plovdiv'),
		   (3, 'Varna')



INSERT INTO [Minions] ([Id], [Name], [Age], [TownId]) 
    VALUES (1,'Kevin', 22 , 1),
           (2,'Bob', 15 , 3),
           (3,'Steward', NULL , 2)

-- 5

TRUNCATE TABLE [MINIONS]

--6
DELETE FROM [Minions]


--7
CREATE TABLE [People](
  [Id] INT PRIMARY KEY IDENTITY(1,1),
  [Name] NVARCHAR(200) NOT NULL,
  [Picture] VARBINARY(MAX)
  CHECK(DATALENGTH(Picture) <= 2000000),
  [Height] DECIMAL(3,2),
  [Weight] DECIMAL(5,2),
  [Gender] CHAR(1) NOT NULL
  CHECK(Gender = 'f' OR Gender = 'm'),
  [Birthdate] DATE NOT NULL,
  [Biography] NVARCHAR(MAX)
)

INSERT INTO [People](Name, Picture, Height, Weight, Gender, Birthdate)
  Values('GOSHO', NULL , 1.00, 45.32, 'm', '2005-12-03'),
        ('GOSHO', NULL , 1.00, 45.32, 'm', '2005-12-03'),
		('GOSHO', NULL , 1.00, 45.32, 'm', '2005-12-03'),
		('GOSHO', NULL , 1.00, 45.32, 'm', '2005-12-03'),
		('GOSHO', NULL , 1.00, 45.32, 'm', '2005-12-03')


--8
CREATE TABLE Users(
  Id BIGINT PRIMARY KEY IDENTITY(1,1),
  Username VARCHAR(30) UNIQUE NOT NULL,
  Password VARCHAR(26) NOT NULL,
  ProfilePicture VARBINARY(900),
  LastLoginTime TIME,
  IsDeleted BIT
)

INSERT INTO Users(Username, Password)
  Values('Gosho', '123456'),
        ('Gosh', '123456'),
		('Gesho', '123456'),
		('Gsho', '123456'),
		('Goho', '123456')


--9
ALTER TABLE Users
  DROP CONSTRAINT PK__Users__3214EC07F2F26FD2

ALTER TABLE Users
  ADD CONSTRAINT PK_Users_Complex
    PRIMARY KEY(Id, Username)

--10
ALTER TABLE Users
  ADD CONSTRAINT IsTrue
    CHECK(PASSWORD > 5)

ALTER TABLE Users
  ADD CONSTRAINT CK_Users_Password_Length 
    CHECK (LEN(Password) >= 5);

--11
ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime 
DEFAULT GETDATE() FOR LastLoginTime

--12
ALTER TABLE Users
  DROP CONSTRAINT PK_Users_Complex


ALTER TABLE Users
  ADD CONSTRAINT UK_Username
    CHECK(LEN(Username) >= 3)

13
CREATE TABLE Directors(
  Id INT PRIMARY KEY IDENTITY(1,1),
  DirectorName NVARCHAR(50),
  NOTES NVARCHAR(50)
)
CREATE TABLE Genres(
  Id INT PRIMARY KEY IDENTITY(1,1),
  GenreName NVARCHAR(50),
  NOTES NVARCHAR(50)
)
CREATE TABLE Categories(
  Id INT PRIMARY KEY IDENTITY(1,1),
  CategoryName NVARCHAR(50),
  NOTES NVARCHAR(50)
)
CREATE TABLE Movies(
  Id INT PRIMARY KEY,
  Title NVARCHAR(50),
  DirectorId INT FOREIGN KEY REFERENCES DIRECTORS (Id),
  CopyRightYear Date,
  Length INT,
  GenreId INT FOREIGN KEY REFERENCES GENRES (Id),
  CategoryId INT FOREIGN KEY REFERENCES Categories (Id),
  Rating DECIMAL (3,2),
  Notes NVARCHAR(500)
)


INSERT INTO Directors(DirectorName, Notes)
VALUES ('Director Name1', 'Notes1'),
       ('Director Name2', 'Notes2'),
       ('Director Name3', 'Notes3'),
       ('Director Name4', 'Notes4'),
       ('Director Name5', 'Notes5')

INSERT INTO Genres(GenreName, Notes)
VALUES ('Genre Name1', 'Notes1'),
       ('Genre Name2', 'Notes2'),
       ('Genre Name3', 'Notes3'),
       ('Genre Name4', 'Notes4'),
       ('Genre Name5', 'Notes5')

INSERT INTO Categories(CategoryName, Notes)
VALUES ('Category Name1', 'Notes1'),
       ('Category Name2', 'Notes2'),
       ('Category Name3', 'Notes3'),
       ('Category Name4', 'Notes4'),
       ('Category Name5', 'Notes5')

INSERT INTO Movies(Title, DirectorId, CopyRightYear, Length, GenreId, CategoryId, Rating, Notes)
VALUES ('Movie Title1', 1, '2022-01-01', 120, 1, 1, 4.5, 'Notes1'),
       ('Movie Title2', 2, '2022-01-02', 130, 2, 2, 3.5, 'Notes2'),
       ('Movie Title3', 3, '2022-01-03', 140, 3, 3, 2.5, 'Notes3'),
       ('Movie Title4', 4, '2022-01-04', 150, 4, 4, 4.0, 'Notes4'),
       ('Movie Title5', 5, '2022-01-05', 160, 5, 5, 5.0, 'Notes5')


14
CREATE TABLE [Categories](
	[Id] INT IDENTITY,
	[CategoryName] NVARCHAR(50) NOT NULL,
	[DailyRate] INT NOT NULL,
	[WeeklyRate] INT NOT NULL,
	[MonthlyRate] INT NOT NULL,
	[WeekendRate] INT NOT NULL,
)
CREATE TABLE [Cars](
	[Id] INT IDENTITY,
	[PlateNumber] NVARCHAR(50) NOT NULL,
	[Manufacturer] NVARCHAR(50) NOT NULL,
	[Model] NVARCHAR(50) NOT NULL,
	[CarYear]  INT NOT NULL,
	[CategoryId] INT NOT NULL,
	[Doors] INT NOT NULL,
	[Picture] VARBINARY(MAX),
	CHECK (DATALENGTH([Picture]) <= 2000000),
	[Condition] NVARCHAR(50) NOT NULL,
	[Available] BIT
)
CREATE TABLE [Employees](
	[Id] INT IDENTITY,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[Notes] NVARCHAR(50) NOT NULL
)
CREATE TABLE [Customers](
	[Id] INT IDENTITY,
	[DriverLicenceNumber] NVARCHAR(50) NOT NULL,
	[FullName] NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	[City] NVARCHAR(50) NOT NULL,
	[ZIPCode] INT,
	[Notes] NVARCHAR(50) NOT NULL,
)
CREATE TABLE [RentalOrders](
	[Id] INT IDENTITY,
	[EmployeeId] INT NOT NULL,
	[CustomerId] INT NOT NULL,
	[CarId] INT NOT NULL,
	[TankLevel] NVARCHAR(50) NOT NULL,
	[KilometrageStart] INT NOT NULL,
	[KilometrageEnd] INT NOT NULL,
	[TotalKilometrage] INT NOT NULL,
	[StartDate]  DATE,
	[EndDate]  DATE,
	[TotalDays] INT NOT NULL,
	[RateApplied] INT NOT NULL,
	[TaxRate] INT NOT NULL,
	[OrderStatus] BIT,
	[Notes] NVARCHAR(50) NOT NULL,
)

INSERT INTO Categories(CategoryName,[DailyRate], [WeeklyRate], [MonthlyRate], [WeekendRate])
VALUES ('Sedan', 2, 14, 10, 50), 
		('Coupe', 4, 24, 20, 60),
		('Cabrio', 6, 34, 30, 70) 
		
INSERT INTO Cars([PlateNumber],[Manufacturer], [Model], [CarYear], [CategoryId], [Doors], [Condition], [Available])
VALUES ('E 1001 E', 'BMW', 'E46', 2001, 1, 4, 'Broken Car', 0), 
		('CB 1001 CB', 'VW', 'Toureg', 2003, 2, 4, 'Big Car', 1),
		('CB XAXAXA', 'Mercedes', 'S500', 2017, 3, 4, 'New Car', 1) 

INSERT INTO Employees([FirstName],[LastName], [Title], [Notes])
VALUES ('Iwan', 'Gerasimow', 'Drifter', 'Nowi gumi wsqka sedmica!!!'), 
		('Gerasim', 'Cecow', 'Truck Driver', 'Profesional driver!'),
		('Ceco', 'Gerasimow', 'Pizza Deliver', 'Fast driver!') 

INSERT INTO Customers([DriverLicenceNumber],[FullName], [Address], [City], [Notes])
VALUES ('DSADSA','Iwan Gerasimow', 'Ul.Ivan Sirakow 21', 'Novi Iskur','Nowi gumi wsqka sedmica!!!'), 
		('DSweqDSA','Gerasim Cecow', 'Ul.Ivan Sirakow 22', 'Stari Iskur','Nowi gumi wsqka sedmica!!!'), 
		('DSA23fA','Ceco Gerasimow', 'Ul.Ivan Sirakow 23', 'Renoviran Iskur','Nowi gumi wsqka sedmica!!!')

INSERT INTO RentalOrders(
[EmployeeId],
[CustomerId], 
[CarId], 
[TankLevel], 
[KilometrageStart], 
[KilometrageEnd],
[TotalKilometrage],
[TotalDays],
[RateApplied],
[TaxRate],
[OrderStatus],
[Notes])
Values(1, 1, 1, 'Full', 0, 1000, 1000,1, 1,1, 1, 'Nice trip!'),
		(2, 2, 2, 'Full', 0, 1000, 1000,2, 2, 2, 2, 'Nice trip!'),
		(3, 3, 3, 'Full', 0, 1000, 1000,3, 3, 3, 3, 'Nice trip!')

--15
CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY NOT NULL,
FirstName VARCHAR(50),
LastName VARCHAR(50),
Title VARCHAR(50),
Notes VARCHAR(MAX)
)
 
INSERT INTO Employees
VALUES
('Velizar', 'Velikov', 'Receptionist', 'Nice customer'),
('Ivan', 'Ivanov', 'Concierge', 'Nice one'),
('Elisaveta', 'Bagriana', 'Cleaner', 'Poetesa')
 
CREATE TABLE Customers(
Id INT PRIMARY KEY IDENTITY NOT NULL,
AccountNumber BIGINT,
FirstName VARCHAR(50),
LastName VARCHAR(50),
PhoneNumber VARCHAR(15),
EmergencyName VARCHAR(150),
EmergencyNumber VARCHAR(15),
Notes VARCHAR(100)
)
 
INSERT INTO Customers
VALUES
(123456789, 'Ginka', 'Shikerova', '359888777888', 'Sistry mi', '7708315342', 'Kinky'),
(123480933, 'Chaika', 'Stavreva', '359888777888', 'Sistry mi', '7708315342', 'Lawer'),
(123454432, 'Mladen', 'Isaev', '359888777888', 'Sistry mi', '7708315342', 'Wants a call girl')
 
CREATE TABLE RoomStatus(
Id INT PRIMARY KEY IDENTITY NOT NULL,
RoomStatus BIT,
Notes VARCHAR(MAX)
)
 
INSERT INTO RoomStatus(RoomStatus, Notes)
VALUES
(1,'Refill the minibar'),
(2,'Check the towels'),
(3,'Move the bed for couple')
 
CREATE TABLE RoomTypes(
RoomType VARCHAR(50) PRIMARY KEY,
Notes VARCHAR(MAX)
)
 
INSERT INTO RoomTypes (RoomType, Notes)
VALUES
('Suite', 'Two beds'),
('Wedding suite', 'One king size bed'),
('Apartment', 'Up to 3 adults and 2 children')
 
CREATE TABLE BedTypes(
BedType VARCHAR(50) PRIMARY KEY,
Notes VARCHAR(MAX)
)
 
INSERT INTO BedTypes
VALUES
('Double', 'One adult and one child'),
('King size', 'Two adults'),
('Couch', 'One child')
 
CREATE TABLE Rooms (
RoomNumber INT PRIMARY KEY IDENTITY NOT NULL,
RoomType VARCHAR(50) FOREIGN KEY REFERENCES RoomTypes(RoomType),
BedType VARCHAR(50) FOREIGN KEY REFERENCES BedTypes(BedType),
Rate DECIMAL(6,2),
RoomStatus NVARCHAR(50),
Notes NVARCHAR(MAX)
)
 
INSERT INTO Rooms (Rate, Notes)
VALUES
(12,'Free'),
(15, 'Free'),
(23, 'Clean it')
 
CREATE TABLE Payments(
Id INT PRIMARY KEY IDENTITY NOT NULL,
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
PaymentDate DATE,
AccountNumber BIGINT,
FirstDateOccupied DATE,
LastDateOccupied DATE,
TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
AmountCharged DECIMAL(14,2),
TaxRate DECIMAL(8, 2),
TaxAmount DECIMAL(8, 2),
PaymentTotal DECIMAL(15, 2),
Notes VARCHAR(MAX)
)
 
INSERT INTO Payments (EmployeeId, PaymentDate, AmountCharged)
VALUES
(1, '12/12/2018', 2000.40),
(2, '12/12/2018', 1500.40),
(3, '12/12/2018', 1000.40)
 
CREATE TABLE Occupancies(
Id  INT PRIMARY KEY IDENTITY NOT NULL,
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
DateOccupied DATE,
AccountNumber BIGINT,
RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
RateApplied DECIMAL(6,2),
PhoneCharge DECIMAL(6,2),
Notes VARCHAR(MAX)
)
 
INSERT INTO Occupancies (EmployeeId, RateApplied, Notes) VALUES
(1, 55.55, 'too'),
(2, 15.55, 'much'),
(3, 35.55, 'typing')

16
CREATE DATABASE [SoftUni]
USE [SoftUni]
CREATE TABLE Towns(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)
CREATE TABLE[Addresses](
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[AddressText] NVARCHAR(50) NOT NULL,
	[TownId] INT NOT NULL
)
CREATE TABLE Departments(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)
CREATE TABLE [Employees](
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[JobTitle] NVARCHAR(50) NOT NULL,
	[DepartmentId] INT NOT NULL,
	[HireDate] DATE,
	[Salary] INT NOT NULL,
	[AddressId] INT NOT NULL
)

INSERT INTO [dbo].[Towns]([Name])
VALUES('Sofia'), ('Plovdiv'), ('Varna'), ('Burgas')

INSERT INTO [dbo].[Departments]([Name])
VALUES('Engineering'), ('Sales'),('Marketing'), ('Software Development'), ('Quality Assurance')

INSERT INTO [dbo].[Employees]([FirstName], [MiddleName], [LastName], [JobTitle], [DepartmentId], [HireDate], [Salary], [AddressId])
VALUES	('Ivan','Ivanov','Ivanov', '.NET Developer', 4, '01/02/2013', 3500.00, 1),
		('Peter','Petrov','Petrov', 'Senior Engineer', 1, '02/03/2004', 4000.00, 2),
		('Maria','Petrova','Ivanova', 'Intern', 5, '28/08/2016', 525.25, 3),
		('Georgi','Terziev','Ivanov', 'CEO', 2, '09/12/2007', 3000.00,4),
		('Peter','Pan','Pan', 'Intern', 3, '28/08/2016', 599.88,1)


19
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

20
SELECT * 
FROM Towns
ORDER BY NAME

SELECT * 
FROM Departments
ORDER BY NAME

SELECT * 
FROM Employees
ORDER BY Salary DESC

21
SELECT Name
FROM Towns
ORDER BY NAME

SELECT Name
FROM Departments
ORDER BY NAME

SELECT FirstName, LastName, JobTitle, Salary
FROM Employees
ORDER BY Salary DESC

22
UPDATE Employees
SET Salary = Salary * 1.10

SELECT Salary
FROM Employees

23
UPDATE Payments
  SET TaxRate = TaxRate * 0.7;

SELECT TaxRate
FROM Payments

24
TRUNCATE TABLE Occupancies;