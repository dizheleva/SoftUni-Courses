USE SoftUni

--2--------------------

SELECT * FROM Departments

--3------------------------

SELECT [Name] FROM Departments

--4-------------------------

SELECT FirstName, LastName, Salary FROM Employees

--5------------------------------

SELECT FirstName, MiddleName, LastName FROM Employees

--6------------------------------------

SELECT FirstName + '.' + LastName + '@softuni.bg' AS [Full Email Address] FROM Employees

--7-----------------------------------

SELECT DISTINCT Salary FROM Employees
--Distinct takes ony the different values

--8-----------------------------------

SELECT * FROM Employees
WHERE JobTitle = 'Sales Representative'

--9---------------------------------------

SELECT FirstName, LastName, JobTitle FROM Employees
WHERE Salary BETWEEN 20000 AND 30000

--10----------------------------------
--Using IN / NOT IN to specify a set of values:

SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name] FROM Employees
WHERE Salary IN (25000, 14000, 12500, 23600)

--11----------------------------------------------

SELECT FirstName, LastName FROM Employees
WHERE ManagerId IS NULL

--12-------------------------------------------------

SELECT FirstName, LastName, Salary FROM Employees
WHERE Salary > 50000 ORDER BY Salary DESC

--13-------------------------------------------------

SELECT TOP (5) FirstName, LastName FROM Employees
ORDER BY Salary DESC

--14---------------------------------------------------

SELECT FirstName, LastName FROM Employees
WHERE NOT (DepartmentId = 4)

--15-----------------------------------------------

SELECT * FROM Employees
ORDER BY Salary DESC, FirstName, LastName DESC, MiddleName

--16---------create view----

CREATE VIEW V_EmployeesSalaries AS
SELECT FirstName, LastName, Salary FROM Employees

SELECT * FROM V_EmployeesSalaries

--17-------------------------------------

CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + 
CASE
        WHEN MiddleName IS NULL THEN ' '+LastName
        ELSE MiddleName+' '+LastName
    END
	AS [Full Name], JobTitle FROM Employees

	--SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name]
	--SELECT CONCAT([FirstName] , ' ', [MiddleName] , ' ' , [LastName]) AS [Full Name]

SELECT * FROM V_EmployeeNameJobTitle

--18----------------------------------------------------

SELECT DISTINCT JobTitle FROM Employees

--19-------------------------------------------------

SELECT TOP (10) * FROM Projects
ORDER BY StartDate, [Name]

--20-----------------------------------------------------

SELECT TOP (7) FirstName, LastName, HireDate FROM Employees
ORDER BY HireDate DESC

--21------UPDATE - SET --------------------------------------------

UPDATE Employees
SET Salary *= 1.12
WHERE DepartmentID IN (SELECT DepartmentID FROM Departments
WHERE [Name] IN ('Engineering', 'Tool Design', 'Marketing', 'Information Services'))

SELECT Salary FROM Employees;


--22-----------------------------------------------------------

USE Geography

SELECT PeakName FROM Peaks ORDER BY PeakName

--23--------------------------------------------------------

SELECT TOP (30) CountryName, [Population] FROM Countries
WHERE ContinentCode = 'EU'
ORDER BY [Population] DESC, CountryName

--24-------------------------------------------------------

SELECT CountryName, CountryCode, 
CASE
        WHEN CurrencyCode = 'EUR' THEN 'Euro'
        ELSE 'Not Euro'
    END AS Currency
	FROM Countries
	ORDER BY CountryName

	--SELECT country_name, country_code, IF(`currencyCode` = 'EUR', 'Euro', 'Not Euro') as `currency` 
	--FROM countries as c ORDER BY c.countrName;

--25-------------------------------------------------------------------

USE Diablo

SELECT [Name] FROM Characters
ORDER BY [Name]