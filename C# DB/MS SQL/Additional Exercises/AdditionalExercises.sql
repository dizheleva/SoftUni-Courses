--1------------------------------------------

SELECT 
       Substring(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS [Email Provider],
	   COUNT(Users.Id) AS [Number Of Users]
FROM Users
GROUP BY Substring(Email, CHARINDEX('@', Email) + 1, LEN(Email))
ORDER BY [Number Of Users] DESC, [Email Provider];

--2----------------------------------------------------

SELECT g.[Name] AS [Game], gt.[Name] AS [Game Type], u.Username, ug.[Level], ug.Cash, c.[Name] AS [Character]
FROM UsersGames AS ug 
JOIN Games AS g ON g.Id = ug.GameId
JOIN GameTypes AS gt ON gt.Id = g.GameTypeId
JOIN Characters AS c ON c.Id = ug.CharacterId
JOIN Users As u ON u.Id = ug.UserId
ORDER BY ug.[Level] DESC, u.Username,  g.[Name];

--3--------------------------------------------------

SELECT u.Username, g.[Name] AS [Game], COUNT(ugi.ItemID) AS [Items Count], Sum(i.Price) AS [Items Price]
FROM UsersGames AS ug 
JOIN Games AS g ON g.Id = ug.GameId
JOIN Users As u ON u.Id = ug.UserId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
GROUP BY u.Username, g.[Name]
HAVING COUNT(ugi.ItemID) >= 10
ORDER BY COUNT(ugi.ItemID) DESC, Sum(i.Price) DESC,  u.Username;

--4----------------------------------------------------------

SELECT 
		u.Username,
		g.[Name] AS [Game],
	    MAX(c.[Name]) AS [Character],
		SUM(its.Strength) + MAX(gts.Strength) + MAX(cs.Strength) AS [Strength],
		SUM(its.Defence) + MAX(gts.Defence) + MAX(cs.Defence) AS Defence,
		SUM(its.Speed) + MAX(gts.Speed) + MAX(cs.Speed) AS Speed,
		SUM(its.Mind) + MAX(gts.Mind) + MAX(cs.Mind) AS Mind,
		SUM(its.Luck) + MAX(gts.Luck) + MAX(cs.Luck) AS Luck		
FROM Users AS u 
JOIN UsersGames AS ug ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
JOIN GameTypes AS gt ON gt.Id = g.GameTypeId
JOIN [Statistics] AS gts ON gts.Id = gt.BonusStatsId
JOIN Characters AS c ON ug.CharacterId = c.Id
JOIN [Statistics] AS cs ON cs.Id = c.StatisticId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
JOIN [Statistics] AS its ON its.Id = i.StatisticId
GROUP BY u.Username, g.[Name]
ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC

--5------------------------------------------------------

SELECT i.[Name],i.Price, i.MinLevel, s.[Strength], s.Defence, s.Speed,	s.Luck,	s.Mind
FROM Items AS i
JOIN [Statistics] AS s ON s.Id = i.StatisticId
WHERE s.Mind > (SELECT AVG(s.Mind) FROM [Statistics] AS s) 
	  AND s.Luck > (SELECT AVG(s.Luck)FROM [Statistics] AS s) 
	  AND s.Speed > (SELECT AVG(s.Speed)FROM [Statistics] AS s)
ORDER BY i.[Name]

--6-----------------------------------------------------

SELECT i.[Name] AS Item, i.Price,	i.MinLevel,	gt.[Name] AS [Forbidden Game Type]
FROM Items AS i
JOIN GameTypeForbiddenItems AS gtf ON i.Id = gtf.ItemId
JOIN GameTypes AS gt ON gtf.GameTypeId = gt.Id
ORDER BY gt.[Name] DESC, i.[Name]

--7---------------------------------------------------------

DECLARE @gameName NVARCHAR(50) = 'Edinburgh'
DECLARE @username NVARCHAR(50) = 'Alex'

DECLARE @userGameId INT = (SELECT ug.Id FROM UsersGames AS ug 
										JOIN Users AS u ON ug.UserId = u.Id
										JOIN Games AS g ON ug.GameId = g.Id
										WHERE u.Username = @username AND g.Name = @gameName)

DECLARE @Price MONEY = (SELECT SUM(Price) FROM Items
    WHERE [Name] IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)',
                   'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet'))

BEGIN TRANSACTION
    UPDATE UsersGames
    SET Cash -= @Price
    WHERE Id = @userGameId

    INSERT INTO UserGameItems (ItemId, UserGameId)
    (SELECT Id, @userGameId FROM Items
    WHERE Name IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)',
                   'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet'))    
COMMIT

SELECT u.Username,	g.[Name],	ug.Cash, i.[Name] AS [Item Name]
FROM UsersGames AS ug
JOIN Users AS u ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE g.[Name] = 'Edinburgh'
ORDER BY i.[Name]

--8------------------------------------------------------

SELECT p.PeakName, m.MountainRange AS Mountain, p.Elevation
FROM Peaks AS p
JOIN Mountains AS m ON m.Id = p.MountainId
ORDER BY p.Elevation DESC, p.PeakName

--9------------------------------------------------------

SELECT p.PeakName, m.MountainRange AS Mountain, c.CountryName, cont.ContinentName
FROM Peaks AS p
JOIN Mountains AS m ON m.Id = p.MountainId
JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
JOIN Countries AS c ON c.CountryCode = mc.CountryCode
JOIN Continents AS cont ON cont.ContinentCode = c.ContinentCode
ORDER BY p.PeakName, c.CountryName

--10------------------------------------------------------------

SELECT c.CountryName, cont.ContinentName, 
	   IIF(COUNT(r.Id) IS NULL, 0, COUNT(r.Id)) AS [RiversCount],
	   IIF(SUM(r.[Length]) IS NULL, 0, SUM(r.[Length])) AS [TotalLength]
FROM Countries AS c 
LEFT JOIN Continents AS cont ON cont.ContinentCode = c.ContinentCode
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName, cont.ContinentName
ORDER BY RiversCount DESC, TotalLength DESC, c.CountryName

--11-------------------------------------------------------------

SELECT cur.CurrencyCode, cur.[Description] AS Currency, COUNT(c.CurrencyCode) AS NumberOfCountries
FROM Countries AS c 
RIGHT JOIN Currencies AS cur ON cur.CurrencyCode = c.CurrencyCode
GROUP BY cur.CurrencyCode, cur.[Description]
ORDER BY COUNT(c.CurrencyCode) DESC, cur.[Description]

--12----------------------------------------------------------------

SELECT cont.ContinentName, 
	   SUM(c.AreaInSqKm) AS [CountriesArea],
	   SUM(CAST(c.Population AS float)) AS [CountriesPopulation]
FROM Continents AS cont 
JOIN Countries AS c ON cont.ContinentCode = c.ContinentCode
GROUP BY cont.ContinentName
ORDER BY [CountriesPopulation] DESC

--13----------------------------------------------------------------
---------------------------1--------------------------
CREATE TABLE Monasteries(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR (50),
	CountryCode CHAR(2) FOREIGN KEY REFERENCES Countries(CountryCode)
)

INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')
-------------------2--------------------
ALTER TABLE Countries
ADD IsDeleted BIT NOT NULL DEFAULT 0
-------------------3----------------------
UPDATE Countries
SET IsDeleted = 1
WHERE CountryCode IN
(
	SELECT CountryCode
	FROM CountriesRivers 
	GROUP BY CountryCode
	HAVING COUNT(RiverId) > 3
)

--------------------4---------------------

SELECT m.[Name] AS Monastery, c.CountryName AS Country
FROM Monasteries AS m
JOIN Countries AS c ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted = 0
ORDER BY m.[Name]

--14----------------------------------------------------

-------------------------1--------------------------
UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

-------------------------2--------------------------
INSERT INTO Monasteries
VALUES('Hanga Abbey', (SELECT CountryCode FROM Countries WHERE CountryName = 'Tanzania'))

-------------------------3--------------------------
INSERT INTO Monasteries
VALUES('Myin-Tin-Daik', (SELECT CountryCode FROM Countries WHERE CountryName = 'Myanmar'))

-------------------------4--------------------------
SELECT cont.ContinentName, c.CountryName, 
	   IIF(COUNT(m.Id) IS NULL, 0, COUNT(m.Id)) AS [MonasteriesCount]
FROM Countries AS c 
LEFT JOIN Monasteries AS m ON m.CountryCode = c.CountryCode
JOIN Continents AS cont ON cont.ContinentCode = c.ContinentCode
WHERE c.IsDeleted=0
GROUP BY c.CountryName, cont.ContinentName
ORDER BY MonasteriesCount DESC, c.CountryName
