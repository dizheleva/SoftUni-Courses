--1--------------------------------

SELECT
COUNT(ID) AS Count
FROM WizzardDeposits

--2---------------------------------

SELECT
MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits

--3

SELECT DepositGroup,
MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits 
GROUP BY DepositGroup

--4--------------------------------------

SELECT TOP(2) DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

--5---------------------------------------

SELECT DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup

--6-------------------------------------

SELECT DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

--7---------------------------------------

SELECT DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC

--8---------------------------------------------

SELECT DepositGroup, MagicWandCreator,
MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

--9-----------------------------------------

SELECT AgeGroup, COUNT(AgeGroup) AS WizardCount 
FROM 
(
SELECT 
CASE
	WHEN Age >= 0 AND Age <= 10 THEN '[0-10]' 
	WHEN Age >= 11 AND Age <= 20 THEN '[11-20]'
	WHEN Age >= 21 AND Age <= 30 THEN '[21-30]'
	WHEN Age >= 31 AND Age <= 40 THEN '[31-40]'
	WHEN Age >= 41 AND Age <= 50 THEN '[41-50]'
	WHEN Age >= 51 AND Age <= 60 THEN '[51-60]'
	WHEN Age >= 61 THEN '[61+]' 
END AS AgeGroup
FROM WizzardDeposits) AS Groups
GROUP BY Groups.AgeGroup

--10------------------------------------------

SELECT SUBSTRING(FirstName, 1, 1) AS FirstLetter
FROM WizzardDeposits 
WHERE DepositGroup = 'Troll Chest'
GROUP BY SUBSTRING(FirstName, 1, 1)

--11----------------------------------------

SELECT DepositGroup, IsDepositExpired,
AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC

--12----------------------------------------------

SELECT 
SUM(HostWizard.DepositAmount - GuestWizard.DepositAmount) AS SumDifference
FROM WizzardDeposits AS HostWizard
JOIN WizzardDeposits AS GuestWizard 
ON GuestWizard.Id = HostWizard.Id + 1

--13--------------------------------------------------

SELECT DepartmentId,
SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

--14--------------------------------------------

SELECT DepartmentId,
MIN(Salary) AS TotalSalary
FROM Employees
WHERE HireDate > '01/01/2000'
GROUP BY DepartmentID
HAVING DepartmentID IN (2, 5, 7)
ORDER BY DepartmentID

--15-----------------------------------------------

--Gives the same result, but not right
/*SELECT Filtered.DepartmentID, AVG(NewSalary) AS AverageSalary
FROM
(SELECT DepartmentID,
	CASE
		WHEN DepartmentID = 1 THEN Salary + 5000
		ELSE Salary
	END AS NewSalary
FROM Employees
WHERE Salary > 30000 AND ManagerID NOT IN (42)
)
AS Filtered
GROUP BY Filtered.DepartmentID*/

SELECT * INTO MyTable  -- Copy table
FROM Employees WHERE Salary > 30000

DELETE MyTable WHERE ManagerID = 42

UPDATE MyTable
SET Salary += 5000 WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM MyTable
GROUP BY DepartmentID

--16-----------------------------------------

SELECT DepartmentID, 
MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--17-----------------------------------------

SELECT COUNT(Salary) AS Count
FROM Employees
WHERE ManagerID IS NULL

--18-----------------------------------------

SELECT DISTINCT Result.DepartmentID, Result.Salary AS ThirdHighestSalary 
FROM (
SELECT DepartmentID, Salary,
DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS Rank
FROM Employees
) AS Result
WHERE Rank = 3

--19---------------------------------------------

SELECT TOP (10) FirstName, LastName, DepartmentID FROM Employees AS e
WHERE Salary > (
SELECT AVG(Salary) FROM Employees
WHERE DepartmentID = e.DepartmentID
)
ORDER BY DepartmentID