--1-------------

CREATE DATABASE Airport

CREATE TABLE Planes(
Id	Int Primary key Identity,
Name varchar(30) NOT NULL,
Seats	Int NOT NULL,
Range	Int NOt	NULL
)
CREATE TABLE Flights(
Id	Int Primary key Identity,
DepartureTime	Datetime,
ArrivalTime	Datetime,
Origin	varchar(50) NOT NULL,
Destination	varchar(50) NOT NULL,
PlaneId	Int Foreign Key references Planes(Id)
)
CREATE TABLE Passengers(
Id	Int Primary key Identity,
FirstName	varchar(30) NOT NULL,
LastName	varchar(30) NOT NULL,
Age	Int NOt	NULL,
Address	varchar(30) NOT NULL,
PassportId	CHAR(11) NOT NULL
)
CREATE TABLE LuggageTypes(
Id	Int Primary key Identity,
Type	varchar(30) NOT NULL
)
CREATE TABLE Luggages(
Id	Int Primary key Identity,
LuggageTypeId	Int Foreign Key references LuggageTypes(Id),
PassengerId	Int Foreign Key references Passengers(Id)
)
CREATE TABLE Tickets(
Id	Int Primary key Identity,
PassengerId	Int Foreign Key references Passengers(Id),
FlightId	Int Foreign Key references Flights(Id),
LuggageId	Int Foreign Key references Luggages(Id),
Price	Decimal(8, 2) NOT NULL
)

--2----------------------------------------------------

INSERT INTO Planes
VALUES
('Airbus 336',  112,    5132),
('Airbus 330',	432,	5325),
('Boeing 369',	231,	2355),
('Stelt 297',	254,	2143),
('Boeing 338',	165,	5111),
('Airbus 558',	387,	1342),
('Boeing 128',	345,	5541)

INSERT INTO LuggageTypes
VALUES
('Crossbody Bag'),
('School Backpack'),
('Shoulder Bag')

--3-------------------------------

UPDATE Tickets
SET Price = 1.13 * Price
WHERE FlightId = (SELECT FlightId FROM Flights WHERE Destination = 'Carlsbad')

--4-------------------------------
--first delete references
DELETE FROM Tickets WHERE FlightId = (SELECT Id FROM Flights WHERE Destination = 'Ayn Halagim')
DELETE FROM Flights WHERE Destination = 'Ayn Halagim'

--5-------------------------------

SELECT Id,	Name,	Seats,	Range FROM Planes 
WHERE Name LIKE ('%tr%')
Order by Id,  Name, Seats, Range

--6-------------------------------

SELECT FlightId,	SUM(Price) AS Price FROM Tickets
GROUP BY FlightId
ORDER BY SUM(Price) DESC, FlightId

--7-------------------------------

Select p.FirstName + ' ' + p.LastName AS [Full Name],  f.Origin, f.Destination 
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
Order by [Full Name], f.Origin, f.Destination

--8--------------------------------

Select p.FirstName AS [First Name], p.LastName AS [Last Name],  p.Age 
FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
WHERE t.Id IS NULL
Order by p.Age DESC, p.FirstName, p.LastName

--9----------------------------------

Select p.FirstName + ' ' + p.LastName AS [Full Name],
		pl.Name As [Plane Name],
		f.Origin + ' - ' + f.Destination AS [Trip],
		lt.Type As [Luggage Type]
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Luggages AS l ON l.Id = t.LuggageId
JOIN LuggageTypes AS lt ON lt.Id = l.LuggageTypeId
JOIN Flights AS f ON f.Id = t.FlightId
JOIN Planes AS pl ON pl.Id = f.PlaneId
--WHERE t.Id IS NOT NULL
Order by [Full Name], [Plane Name], f.Origin, f.Destination, lt.Type

--10------------------------------------------------

Select pl.Name, pl.Seats, COUNT(p.Id) AS [Passengers Count]
FROM Planes AS pl
LEFT JOIN Flights As f ON f.PlaneId = pl.Id
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
LEFT JOIN Passengers AS p ON p.Id = t.PassengerId
GROUP BY pl.Name, pl.Seats
Order by COUNT(p.Id) DESC, pl.Name, pl.Seats

--11----------------------------------------------------

CREATE FUNCTION udf_CalculateTickets(@origin varchar(50), @destination varchar(50), @peopleCount INT) 
RETURNS VARCHAR(30)
AS
BEGIN
DECLARE @flightID INT = (SELECT Id FROM Flights WHERE Origin = @origin AND Destination = @destination)
--dont`t forget begin and end!
	IF (@peopleCount <= 0)
		return 'Invalid people count!'
	ELSE IF(@flightID IS NULL)
		return 'Invalid flight!'
	ELSE
		DECLARE @totalPrice DECIMAL(8, 2) = (SELECT Price*@peopleCount From Tickets WHERE FlightId = @flightID)
		return'Total price ' + CAST(@totalPrice As VARCHAR(30))
END

--12----------------------------------------------------

CREATE PROC usp_CancelFlights
AS
BEGIN TRANSACTION

	SELECT Id INTO FlightsToCancel FROM Flights 
	WHERE ArrivalTime > DepartureTime
	
	UPDATE Flights
	SET ArrivalTime = NULL
	WHERE Id IN (SELECT Id FROM FlightsToCancel)
	UPDATE Flights
	SET DepartureTime = NULL
	WHERE Id IN (SELECT Id FROM FlightsToCancel)
	
COMMIT