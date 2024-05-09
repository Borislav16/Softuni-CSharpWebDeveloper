USE SoftUni

--1
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 AS
SELECT FirstName, LastName
FROM Employees
WHERE Salary > 35000

EXEC usp_GetEmployeesSalaryAbove35000


--2
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber 
@number DECIMAL(18,4)
AS
SELECT FirstName, LastName
FROM Employees
WHERE Salary >= @number

EXEC usp_GetEmployeesSalaryAboveNumber 48100


--3
CREATE OR ALTER PROCEDURE usp_GetTownsStartingWith
@string NVARCHAR(20)
AS
SELECT Name
FROM Towns
WHERE Name LIKE CONCAT(@string, '%')

EXEC usp_GetTownsStartingWith BE


--4
CREATE PROCEDURE usp_GetEmployeesFromTown
@town NVARCHAR(20)
AS
SELECT e.FirstName, e.LastName
FROM Employees AS e
JOIN Addresses AS a ON a.AddressID = e.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
WHERE t.Name LIKE @town

EXEC usp_GetEmployeesFromTown Sofia


--5
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10)
AS
BEGIN
DECLARE @text NVARCHAR(10)
  IF(@salary < 30000)
    SET @text = 'Low'
  ELSE IF(@salary BETWEEN 30000 AND 50000)
    SET @text = 'Average'
  ELSE 
    SET @text = 'High'
RETURN @text
END  


--6
CREATE PROCEDURE usp_EmployeesBySalaryLevel
@levelOfSalary NVARCHAR(10)
AS
SELECT FirstName, LastName
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) LIKE @levelOfSalary


--7
CREATE OR ALTER FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(50))
RETURNS BIT
BEGIN
  DECLARE @i INT = 1
  
  WHILE(@i <= LEN(@word))
  BEGIN
  DECLARE @ch NVARCHAR(1) = SUBSTRING(@word, @i, 1)
    IF(CHARINDEX(@ch, @setOfLetters)) = 0
	BEGIN
	  RETURN 0
	END
	SET @i += 1
  END
  RETURN 1
END

SELECT dbo.ufn_IsWordComprised('abcd', 'asb')


--8
CREATE PROCEDURE usp_DeleteEmployeesFromDepartment
@departmentID INT
AS
BEGIN 
    DECLARE @employeesToDelete TABLE (ID INT)

	INSERT INTO @employeesToDelete(ID)
	SELECT EmployeeID
	FROM Employees
	WHERE DepartmentID = @departmentID

    ALTER TABLE [Departments]
	ALTER COLUMN [ManagerID] INT

	UPDATE Departments
	SET ManagerID  = NULL
	WHERE ManagerID IN (SELECT * FROM @employeesToDelete)
    
	UPDATE Employees
	SET ManagerID  = NULL
	WHERE ManagerID IN (SELECT * FROM @employeesToDelete)

	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT * FROM @employeesToDelete)

	DELETE FROM Employees
	WHERE DepartmentID = @departmentID

	DELETE FROM Departments
	WHERE DepartmentID = @departmentID

	SELECT COUNT(*)
	FROM Employees
	WHERE DepartmentID = @departmentID
END


--9
USE Bank

CREATE PROCEDURE usp_GetHoldersFullName
AS
SELECT FirstName + ' ' + LastName AS [FULL NAME] 
FROM AccountHolders

EXEC usp_GetHoldersFullName


--10

CREATE OR ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan
(
	@sum MONEY
)
AS
BEGIN 
	SELECT FirstName AS [First Name], LastName AS [Last Name] FROM
	(
		SELECT FirstName, LastName, SUM(a.Balance) AS TotalBalance FROM AccountHolders AS ah
		JOIN Accounts AS a
		ON a.AccountHolderId = ah.Id
		GROUP BY ah.FirstName, ah.LastName
	) AS tb
	WHERE tb.TotalBalance > @sum
			ORDER BY FirstName, LastName
END

EXEC usp_GetHoldersWithBalanceHigherThan 7000



--11
CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(18,4), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(18,4)
BEGIN
    DECLARE @FutureValue DECIMAL(18,4);

    SET @FutureValue = @Sum * POWER((1 + @YearlyInterestRate), @NumberOfYears)
    
    RETURN @FutureValue
END



--12

CREATE PROC usp_CalculateFutureValueForAccount (@AccountId INT, @InterestRate FLOAT) AS
SELECT a.Id AS [Account Id],
	   ah.FirstName AS [First Name],
	   ah.LastName AS [Last Name],
	   a.Balance,
	   dbo.ufn_CalculateFutureValue(Balance, @InterestRate, 5) AS [Balance in 5 years]
  FROM AccountHolders AS ah
  JOIN Accounts AS a ON ah.Id = a.Id
 WHERE a.Id = @AccountId


--13
CREATE FUNCTION ufn_CashInUsersGames(@gameName NVARCHAR(50))
  RETURNS TABLE
             AS
         RETURN
                (
                    SELECT SUM(Cash)
                        AS SumCash
                      FROM (
                                SELECT g.Name,
                                       ug.Cash,
                                       ROW_NUMBER() OVER(ORDER BY ug.Cash DESC)
                                    AS RowNumber
                                  FROM UsersGames
                                    AS ug
                            INNER JOIN Games
                                    AS g
                                    ON ug.GameId = g.Id
                                 WHERE g.Name = @gameName
                           ) 
                        AS RankingSubQuery
                     WHERE RowNumber % 2 <> 0
                )
 
SELECT * FROM [dbo].[ufn_CashInUsersGames]('Love in a mist')