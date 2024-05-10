CREATE DATABASE TableRelations
--1
USE TableRelations

CREATE TABLE Passports(
  PassportID INT PRIMARY KEY IDENTITY(101, 1),
  PassportNumber CHAR(8)
)

CREATE TABLE Persons(
  PersonID INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(15),
  Salary DECIMAL (7,2),
  PassportID INT FOREIGN KEY REFERENCES Passports(PassportID)
)

INSERT INTO Passports
  VALUES  ('N34FG21B'),
          ('K65LO4R7'),
		  ('ZE657QP2')

INSERT INTO Persons(FirstName, Salary, PassportID)
  VALUES  ('Roberto',43300.00, 102),
          ('Tom', 56100.00, 103),
		  ('Yana', 60200.00, 101)


--2
CREATE TABLE Manufacturers(
  ManufacturerID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20),
  EstablishedON DATE
)

CREATE TABLE Models(
  ModelID INT PRIMARY KEY IDENTITY(100,1),
  Name NVARCHAR(20),
  ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID) 
)

INSERT INTO Manufacturers(Name, EstablishedON)
  VALUES  ('BMW' , '07/03/1916'),
          ('Tesla', '01/01/2003'),
		  ('Lada', '01/05/1966')

INSERT INTO Models(Name, ManufacturerID)
  VALUES  ('X1', 1),
          ('i6', 1),
		  ('Model S', 2),
		  ('Model X', 2),
		  ('Model 3', 2),
		  ('Nova', 3)


--3
CREATE TABLE Students(
  StudentID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20)
)

CREATE TABLE Exams(
  ExamID INT PRIMARY KEY IDENTITY(101, 1),
  Name NVARCHAR(20)
)


CREATE TABLE StudentsExams(
  StudentID INT,
  ExamID INT,
  PRIMARY KEY(StudentID, ExamID),
  FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
  FOREIGN KEY(ExamID) REFERENCES Exams(ExamID),
)

INSERT INTO Students(Name)
  VALUES ('Mila'),
         ('Toni'),
		 ('Ron')


INSERT INTO Exams(Name)
  VALUES ('SpringMVC'),
         ('Neo4j'),
		 ('Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
  VALUES (1,101),
         (1, 102),
		 (2, 101),
		 (3, 103),
		 (2, 102),
		 (2, 103)


--4
CREATE TABLE Teachers(
  TeacherID INT PRIMARY KEY IDENTITY(101, 1),
  Name NVARCHAR(20),
  ManagerID INT REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers(Name, ManagerID)
  VALUES  ('John', NULL),
          ('Maya', 106),
		  ('Silvia', 106),
          ('Ted', 105),
		  ('Mark', 101),
          ('Mark', 101)


--5
CREATE TABLE Cities(
  CityID INT PRIMARY KEY,
  Name NVARCHAR(20)
)

CREATE TABLE Customers(
  CustomerID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20),
  Birthday DATE,
  CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)

CREATE TABLE Orders(
  OrderID INT PRIMARY KEY IDENTITY,
  CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes(
  ItemTypeID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20),
)

CREATE TABLE Items(
  ItemID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20),
  ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems(
  OrderID INT ,
  ItemID INT ,
  PRIMARY KEY(OrderID, ItemID),
  FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
  FOREIGN KEY(ItemID) REFERENCES Items(ItemID)

)


--6
CREATE TABLE Majors(
  MajorID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(20)
)

CREATE TABLE Students(
  StudentID INT PRIMARY KEY IDENTITY,
  StudentNumber INT ,
  StudentName NVARCHAR(25),
  MajorID INT FOREIGN KEY REFERENCES Majors(MajorID)
)

CREATE TABLE Subjects(
  SubjectID INT PRIMARY KEY IDENTITY,
  SubjectName NVARCHAR(20)
)

CREATE TABLE Payments(
  PaymentID INT PRIMARY KEY IDENTITY,
  PaymentDate DATE,
  PaymentAmount DECIMAL,
  StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
)

CREATE TABLE Agenda(
  StudentID INT,
  SubjectID INT,
  PRIMARY KEY(StudentID, SubjectID),
  FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
  FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
)


USE Geography

SELECT m.MountainRange, p.PeakName, p.Elevation 
FROM Mountains AS m
JOIN Peaks AS p ON p.MountainId = m.Id
WHERE m.MountainRange = 'Rila'
ORDER BY Elevation DESC

