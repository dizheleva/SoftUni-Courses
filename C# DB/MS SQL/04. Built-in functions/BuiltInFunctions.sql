USE SoftUni

--1--------------------------------

SELECT FirstName, LastName FROM Employees
WHERE Substring(FirstName, 1, 2) = 'Sa';
--WHERE FirstName LIKE 'Sa%'

--2---------------------------------

SELECT FirstName, LastName FROM Employees
WHERE LastName LIKE '%ei%';

--3---------------------------------

SELECT FirstName FROM Employees
WHERE DepartmentID IN (3, 10)
        AND YEAR(HireDate) >= 1995
        AND YEAR(HireDate) <= 2005;

--4---------------------------------

SELECT FirstName, LastName FROM Employees
WHERE JobTitle NOT LIKE '%engineer%';

--5----------------------------------

SELECT [Name] FROM Towns
WHERE LEN([Name]) IN (5, 6)
ORDER BY [Name];

--6---------------------------------

SELECT TownID, [Name] FROM Towns
WHERE Substring([Name], 1, 1) IN ('M', 'K', 'B', 'E')
ORDER BY [Name];

--7---------------------------------

SELECT TownID, [Name] FROM Towns
WHERE Substring([Name], 1, 1) NOT IN ('R', 'B', 'D')
ORDER BY [Name];

--8----------------------------------

CREATE VIEW V_EmployeesHiredAfter2000 AS
    SELECT FirstName, LastName FROM Employees
    WHERE YEAR(HireDate) > 2000;

--9--------------------------------------

SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5;

--10------------------------------------------

SELECT EmployeeID, FirstName, LastName, Salary,
    DENSE_RANK() OVER   
      (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]  
    FROM Employees   
  WHERE Salary BETWEEN 10000 AND 50000   
  ORDER BY Salary DESC;

--11---------------------------------------------

SELECT * FROM (
SELECT EmployeeID, FirstName, LastName, Salary,
    DENSE_RANK() OVER   
      (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]  
    FROM Employees   
  WHERE Salary BETWEEN 10000 AND 50000) AS RankedTable
  WHERE [Rank] = 2 ORDER BY Salary DESC;

  ----------------------------------------------------

  USE Geography;

--12------------------------------------------------

SELECT CountryName, IsoCode FROM Countries
WHERE CountryName Like '%a%a%a%'
ORDER BY IsoCode;

--13------------------------------------------------

SELECT Peaks.PeakName, Rivers.RiverName,
       LOWER(CONCAT(LEFT(Peaks.PeakName, LEN(Peaks.PeakName)-1), Rivers.RiverName)) AS Mix
FROM Peaks
     JOIN Rivers ON RIGHT(Peaks.PeakName, 1) = LEFT(Rivers.RiverName, 1)
ORDER BY Mix;

-------------
USE Diablo;

--14----------------------------------------------

SELECT TOP (50) [Name], FORMAT(CAST([Start] AS DATE), 'yyyy-MM-dd') AS [Start]
FROM Games
WHERE DATEPART(YEAR, Start) BETWEEN 2011 AND 2012
ORDER BY [Start], [Name];

--15---------------------------------------------

SELECT Username,
       Substring(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS [Email Provider] 
FROM Users
ORDER BY [Email Provider], Username;

--16--------------------------------------------------

SELECT Username, IpAddress FROM Users
WHERE IpAddress LIKE '___.1_%._%.___'
ORDER BY Username;

--17--------------------------------------------------

SELECT [Name] AS [Game],
       CASE
           WHEN DATEPART(HOUR, [Start]) BETWEEN 0 AND 11
           THEN 'Morning'
           WHEN DATEPART(HOUR, [Start]) BETWEEN 12 AND 17
           THEN 'Afternoon'
           WHEN DATEPART(HOUR, [Start]) BETWEEN 18 AND 23
           THEN 'Evening'
       END AS [Part of the Day],
       CASE
           WHEN Duration <= 3
           THEN 'Extra Short'
           WHEN Duration BETWEEN 4 AND 6
           THEN 'Short'
           WHEN Duration > 6
           THEN 'Long'
           WHEN Duration IS NULL
           THEN 'Extra Long'
       END AS Duration
FROM Games
ORDER BY [Name], Duration, [Part of the Day];

--18---------------------------------------------

SELECT ProductName,
       OrderDate,
       DATEADD(DAY, 3, OrderDate) AS [Pay Due],
       DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders;

--19-------------------------------------------------------

CREATE TABLE People
(
             Id INT PRIMARY KEY IDENTITY,
             [Name] NVARCHAR(50) NOT NULL,
             Birthdate DATETIME2 NOT NULL
);

INSERT INTO People
VALUES
('Victor', '2000-12-07 00:00:00.000'),
('Steven', '1992-09-10 00:00:00.000'),
('Stephen', '1910-09-19 00:00:00.000'),
('John', '2010-01-06 00:00:00.000');

SELECT [Name],
       DATEDIFF(YEAR, Birthdate, GETDATE()) AS [Age in Years],
       DATEDIFF(MONTH, Birthdate, GETDATE()) AS [Age in Months],
       DATEDIFF(DAY, Birthdate, GETDATE()) AS [Age in Days],
       DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Minutes]
FROM People;

--DROP TABLE People;