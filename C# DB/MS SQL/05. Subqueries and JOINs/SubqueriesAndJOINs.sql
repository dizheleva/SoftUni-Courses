--1----------------------------------

SELECT TOP(5) EmployeeID, JobTitle, a.AddressID, a.AddressText FROM Employees AS e
INNER JOIN Addresses AS a
ON e.AddressID = a.AddressID
ORDER BY AddressID;

--2-------------------------------------

SELECT TOP(50) FirstName, LastName, t.[Name] AS Town, a.AddressText FROM Employees AS e
INNER JOIN Addresses AS a
ON e.AddressID = a.AddressID
INNER JOIN Towns AS t
ON a.TownID = t.TownID
ORDER BY FirstName, LastName;

--3------------------------------------

SELECT EmployeeID, FirstName, LastName, d.[Name] AS DepartmentName FROM Employees AS e
INNER JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY EmployeeID;

--4------------------------------------

SELECT TOP(5) EmployeeID, FirstName, Salary, d.[Name] AS DepartmentName FROM Employees AS e
INNER JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE Salary >= 15000
ORDER BY e.DepartmentID;

--5------------------------------------------

SELECT TOP(3) e.EmployeeID, FirstName FROM Employees AS e
LEFT OUTER JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
WHERE ep.ProjectID IS NULL
ORDER BY EmployeeID;

--6--------------------------------------------

SELECT e.FirstName, e.LastName, e.HireDate, d.[Name] AS DeptName FROM Employees e
INNER JOIN Departments d
ON (e.DepartmentId = d.DepartmentId
AND e.HireDate > '1/1/1999'
AND d.Name IN ('Sales', 'Finance'))
ORDER BY e.HireDate ASC;

--7--------------------------------------------

SELECT TOP(5) e.EmployeeID, FirstName, p.[Name] AS ProjectName FROM Employees AS e
JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '08/13/2002' AND p.EndDate IS NULL --OR p.StartDate > (SELECT CONVERT(DATE, '13.08.2002', 103)) AND p.EndDate IS NULL
ORDER BY EmployeeID;

--8--------------------------------------------

SELECT e.EmployeeID, FirstName, 
       CASE
           WHEN p.StartDate > '2005'
           THEN NULL
           ELSE p.Name
       END AS ProjectName FROM Employees AS e
JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24 
ORDER BY EmployeeID;

--9-----------------------------------------

SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS ManagerName FROM Employees AS e
     JOIN Employees AS m ON e.ManagerID = m.EmployeeID
WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID;

--10----------------------------------------

SELECT TOP(50) 
       e.EmployeeID,
       e.FirstName + ' ' + e.LastName AS EmployeeName,
	   m.FirstName+' '+m.LastName AS ManagerName,
	   d.[Name] AS DepartmentName
	   FROM Employees AS e
     JOIN Employees AS m ON e.ManagerID = m.EmployeeID
	 JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
ORDER BY EmployeeID;

--11---------------------------------------------

SELECT MIN(ads.AverageSalary) AS MinAverageSalary
FROM
(
    SELECT AVG(Salary) AS AverageSalary
    FROM Employees
    GROUP BY DepartmentID
) AS ads;

--12----------------------------------------------

SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation FROM Countries AS c
JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
JOIN Mountains AS m
ON mc.MountainId = m.Id
JOIN Peaks AS p
ON m.Id = p.MountainId
WHERE c.CountryName = 'Bulgaria' AND p.Elevation > 2835 
ORDER BY p.Elevation DESC;

--13-----------------------------------------------

SELECT c.CountryCode, COUNT(mc.MountainId) AS MountainRanges FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
GROUP BY mc.CountryCode,
         c.CountryCode,
         CountryName
HAVING c.CountryName IN ('United States', 'Russia', 'Bulgaria');

--14------------------------------------------------

SELECT TOP (5) c.CountryName, r.RiverName FROM Countries AS c
LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
JOIN Continents AS cnt ON c.ContinentCode = cnt.ContinentCode
WHERE cnt.ContinentName = 'Africa'
ORDER BY c.CountryName;

--15------------------------------------------------

SELECT ranked.ContinentCode, ranked.CurrencyCode, ranked.CurrencyUsage
FROM
(
    SELECT manyCurrencies.ContinentCode,
           manyCurrencies.CurrencyCode,
           manyCurrencies.CurrencyUsage,
           DENSE_RANK() OVER(PARTITION BY manyCurrencies.ContinentCode ORDER BY manyCurrencies.CurrencyUsage DESC) AS UsageRank
    FROM
    (
        SELECT ContinentCode,
               CurrencyCode,
               COUNT(CurrencyCode) AS CurrencyUsage
        FROM Countries
        GROUP BY ContinentCode,
                 CurrencyCode
        HAVING COUNT(CurrencyCode) > 1
    ) AS manyCurrencies
) AS ranked
WHERE ranked.UsageRank = 1
ORDER BY ranked.ContinentCode;

--16---------------------------------------------

SELECT COUNT(c.CountryCode) AS [Count] FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
WHERE mc.CountryCode IS NULL;

--17---------------------------------------------

SELECT TOP (5) peaks.CountryName,
               peaks.Elevation AS HighestPeakElevation,
               rivers.[Length] AS LongestRiverLength
FROM
(
    SELECT c.CountryName,
           c.CountryCode,
           DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS DescendingElevationRank,
           p.Elevation
    FROM Countries AS c
         FULL OUTER JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
         FULL OUTER JOIN Mountains AS m ON mc.MountainId = m.Id
         FULL OUTER JOIN Peaks AS p ON m.Id = p.MountainId
) AS peaks
FULL OUTER JOIN
(
    SELECT c.CountryName,
           c.CountryCode,
           DENSE_RANK() OVER(PARTITION BY c.CountryCode ORDER BY r.[Length] DESC) AS DescendingRiversLenghRank,
           r.Length
    FROM Countries AS c
         FULL OUTER JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
         FULL OUTER JOIN Rivers AS r ON cr.RiverId = r.Id
) AS rivers ON peaks.CountryCode = rivers.CountryCode
WHERE peaks.DescendingElevationRank = 1
      AND rivers.DescendingRiversLenghRank = 1
      AND (peaks.Elevation IS NOT NULL
           OR rivers.[Length] IS NOT NULL)
ORDER BY HighestPeakElevation DESC,
         LongestRiverLength DESC,
         CountryName;

--18------------------------------------------------

SELECT TOP (5) jt.CountryName AS Country,
               ISNULL(jt.PeakName, '(no highest peak)') AS HighestPeakName,
               ISNULL(jt.Elevation, 0) AS HighestPeakElevation,
               ISNULL(jt.MountainRange, '(no mountain)') AS Mountain
FROM
(
    SELECT c.CountryName,
           DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS PeakRank,
           p.PeakName,
           p.Elevation,
           m.MountainRange
    FROM Countries AS c
         LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
         LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
         LEFT JOIN Peaks AS p ON m.Id = p.MountainId
) AS jt
WHERE jt.PeakRank = 1
ORDER BY jt.CountryName,
         jt.PeakName;
