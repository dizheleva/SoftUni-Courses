--1-------------------------------

CREATE PROC usp_GetEmployeesSalaryAbove35000 AS
SELECT FirstName, LastName FROM Employees
WHERE Salary > 35000

--2-----------------------------------

CREATE PROC usp_GetEmployeesSalaryAboveNumber 
(@minSalary DECIMAL(18,4))
AS
SELECT FirstName, LastName FROM Employees
WHERE Salary >= @minSalary

----------------------
EXEC usp_GetEmployeesSalaryAboveNumber
@minSalary = 34000

--3-------------------------------------

CREATE PROC usp_GetTownsStartingWith 
(@startingString VARCHAR(20))
AS
SELECT [Name] FROM Towns
WHERE [Name] LIKE @startingString + '%'

--4--------------------------------------

CREATE PROC usp_GetEmployeesFromTown 
(@town VARCHAR(20))
AS
SELECT FirstName, LastName FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID 
JOIN Towns AS t ON a.TownID = t.TownID
WHERE t.[Name] = @town

--5----------------------------------------

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10)
AS
BEGIN
DECLARE @salaryLevel VARCHAR(10)
	IF (@Salary < 30000)
		SET @salaryLevel = 'Low'
	ELSE IF(@Salary <= 50000)
		SET @salaryLevel = 'Average'
	ELSE
		SET @salaryLevel = 'High'
RETURN @salaryLevel
END

--6---------------------------------------------

CREATE PROCEDURE usp_EmployeesBySalaryLevel (@SalaryLevel NVARCHAR(10))
AS
BEGIN
	SELECT FirstName AS [First Name], 
	LastName AS [Last Name]
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel
END

--7------------------------------------------------

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(10), @word NVARCHAR(10))
RETURNS BIT
AS
BEGIN
DECLARE @position INT = 1;
	WHILE (@position <= LEN(@word))
		BEGIN
		DECLARE @currentLetter CHAR(1) = SUBSTRING (@word, @position, 1);
		IF(CHARINDEX(@currentLetter, @setOfLetters) = 0)
			RETURN 0;
		SET @position += 1
	END
RETURN 1
END

--8---------------------------------------------------

CREATE PROCEDURE usp_DeleteEmployeesFromDepartment (@departmentId INT)
AS
BEGIN
ALTER TABLE Departments
ALTER COLUMN ManagerID INT NULL 
 
DELETE FROM EmployeesProjects
WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)
 
UPDATE Employees
SET ManagerID = NULL
WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)
 
UPDATE Employees
SET ManagerID = NULL
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)
 
UPDATE Departments
SET ManagerID = NULL
WHERE DepartmentID = @departmentId
 
DELETE FROM Employees 
WHERE DepartmentID = @departmentId
 
DELETE FROM Departments
WHERE DepartmentID = @departmentId
 
SELECT COUNT(*) FROM Employees WHERE DepartmentID = @departmentId
END

--9----------------------------------------------------------

CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT FirstName + ' ' +  LastName AS [Full Name]
	FROM AccountHolders	
END

--10-----------------------------------------------------

CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan(@minMoney DECIMAL(18,4))
AS
BEGIN
	SELECT FirstName, LastName FROM AccountHolders AS ah
	JOIN Accounts AS a ON ah.Id = a.AccountHolderId
	GROUP BY ah.Id, ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @minMoney
	ORDER BY ah.FirstName, ah.LastName
END

--11------------------------------------------------------

CREATE FUNCTION ufn_CalculateFutureValue(@Sum MONEY, @Rate FLOAT , @Years INT)
RETURNS MONEY AS
BEGIN
    RETURN @Sum * POWER(1+@Rate,@Years)
END

--12------------------------------------------------------

CREATE PROCEDURE usp_CalculateFutureValueForAccount(@id INT, @rate FLOAT)
AS
BEGIN
	SELECT ah.Id AS [Account Id],
	ah.FirstName AS [First Name],
	ah.LastName AS [Last Name],
	a.Balance AS [Current Balance],
	dbo.ufn_CalculateFutureValue(a.Balance, @rate, 5) AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON ah.Id = a.AccountHolderId
	WHERE a.Id = @id
END

--13-----------------------------------------------------

CREATE FUNCTION ufn_CashInUsersGames(@gameName VARCHAR(MAX))
RETURNS @resultTable TABLE (SumCash money)
AS
BEGIN
	DECLARE @result DECIMAL(18,4)
	SET @result = 
	(SELECT SUM(k.Cash) AS Cash
	FROM
		(SELECT Cash, GameId, ROW_NUMBER() OVER (ORDER BY Cash DESC) AS RowNumber
		FROM UsersGames
		WHERE GameId = (SELECT Id FROM Games WHERE Name = @gameName)
		) AS k
	WHERE k.RowNumber % 2 != 0)

	INSERT INTO @resultTable SELECT @result
	RETURN
END
