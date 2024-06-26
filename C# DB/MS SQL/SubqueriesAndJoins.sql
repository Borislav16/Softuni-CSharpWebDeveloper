USE SoftUni

--1
SELECT TOP 5 e.EmployeeID , e.JobTitle, e.AddressID, a.AddressText
FROM Employees AS e
LEFT JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY e.AddressID 


--2
SELECT TOP 50 e.FirstName, e.LastName, t.Name, a.AddressText
FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
ORDER BY FirstName, LastName


--3
SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name
FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID


--4
SELECT TOP 5 e.EmployeeID, e.FirstName, e.Salary, d.Name
FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID 

--5
SELECT TOP 3 e.EmployeeID, e.FirstName
FROM Employees AS e
LEFT JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
WHERE ep.ProjectID IS NULL
ORDER BY EmployeeID


--6
SELECT e.FirstName, e.LastName, e.HireDate, d.Name
FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE e.HireDate > '1999-1-1' AND d.Name = 'Sales' OR d.Name = 'Finance'
ORDER BY e.HireDate


--7
SELECT TOP 5 e.EmployeeID, e.FirstName, p.Name
FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE p.ProjectID IS NOT NULL AND p.StartDate > '2002-08-13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID


--8
SELECT e.EmployeeID, e.FirstName,
CASE
  WHEN YEAR(p.StartDate) >= 2005 THEN NULL
  ELSE p.Name
  END
FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24 


--9
SELECT e.EmployeeID, e.FirstName, m.EmployeeID AS [ManagerID], m.FirstName AS [ManagerName] 
FROM Employees AS e
JOIN Employees AS m ON e.ManagerID = m.EmployeeID
WHERE m.EmployeeID IN (3,7)
ORDER BY e.EmployeeID


--10
SELECT TOP 50 
   e.EmployeeID
   ,e.FirstName + ' ' + e.LastName AS [EmployeeName]
   ,m.FirstName + ' ' + m.LastName AS [ManagerName]
   ,d.Name
FROM Employees AS e
JOIN Employees AS m ON e.ManagerID = m.EmployeeID 
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID



--11
SELECT TOP 1 a AS [AVERAGE]
FROM (
		SELECT AVG(SALARY) AS [a]
		FROM Employees
		GROUP BY DepartmentID
     ) AS AverageSalary
ORDER BY AVERAGE 


USE Geography

--12
SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
JOIN Mountains AS m ON m.Id = mc.MountainId
JOIN Peaks AS p ON p.MountainId = m.Id
WHERE c.CountryName = 'Bulgaria' AND p.Elevation > 2835
ORDER BY p.Elevation DESC


--13
SELECT c.CountryCode, COUNT(mc.MountainId)
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE c.CountryName IN ('Bulgaria','Russia', 'United States')
GROUP BY c.CountryCode



--14
SELECT TOP 5 c.CountryName, r.RiverName
FROM Countries AS c
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName



--16
SELECT COUNT(c.CountryCode) 
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL


--17
SELECT TOP 5 c.CountryName, MAX(p.Elevation) AS [Eevation], MAX(r.Length) AS [Rivr]
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
LEFT JOIN Peaks AS p ON p.MountainId = m.Id
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY Eevation DESC, Rivr DESC, c.CountryName 

SELECT TOP (5)
		[c].[CountryName],
		MAX([p].[Elevation]) AS [HighestPeakElevation],
		MAX([r].[Length]) AS [LongestRiverLength]
FROM [Countries] AS [c]
	LEFT JOIN [CountriesRivers] AS [cr]
		ON [c].[CountryCode] = [cr].[CountryCode]
	LEFT JOIN [Rivers] AS [r]
		ON [cr].[RiverId] = [r].[Id]
	LEFT JOIN [MountainsCountries] AS [mc]
		ON [mc].[CountryCode] = [c].[CountryCode]
	LEFT JOIN [Mountains] AS [m]
		ON [mc].[MountainId] = [m].[Id]
	LEFT JOIN [Peaks] AS [p]
		ON [p].[MountainId] = [m].[Id]
GROUP BY [c].[CountryName]
ORDER BY [HighestPeakElevation] DESC, [LongestRiverLength] DESC, [CountryName]