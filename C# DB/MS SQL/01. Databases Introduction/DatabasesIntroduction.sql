--1

CREATE DATABASE Minions

USE Minions

--2------------------------------------------------------

CREATE TABLE Minions(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Age TINYINT
)

CREATE TABLE Towns(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

--3-----------------------------------------------------

ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

--4---------------------------------------------------

-- [] escaping special words like Name in SQL
INSERT INTO Towns(Id, [Name])
	VALUES
				(1, 'Sofia'),
				(2, 'Plovdiv'),
				(3, 'Varna')

SELECT * FROM Towns

--4 j

INSERT INTO Minions(Id, [Name], Age, TownId)
	VALUES
				(1, 'Kevin', 22, 1),
				(2, 'Bob', 15, 3),
				(3, 'Steward', NULL, 2)

SELECT * FROM Minions

--5------------------------------------------------------------------------

-- Removes the records in the table
TRUNCATE TABLE Minions

--6--------------------------------------------------------------------------

-- Deletes the table, startig from the table with foreign key inside
DROP TABLE Minions

DROP TABLE Towns

--7 j------------------------------------------------------------------------------

CREATE TABLE People(
-- unique number for every person there will be no more than 2 31 -1 people. (Auto incremented)
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(5, 2),
	[Weight] DECIMAL(5, 2),
	Gender CHAR(1) NOT NULL CHECK(Gender='m' OR Gender='f'),
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX),
	-- if not above // CONSTRAINT CK_People_Gender CHECK (Gender='m' OR Gender='f')
)

INSERT INTO People ([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES
	('Kaloyan', Null, 1.82, 72.77, 'm', '1984/01/28', 'Kaloyans Bio'),
	('Dilyana', Null, 1.74, 87.90, 'f', '1989/01/27', 'Dilyanas Bio'),
	('Kaliya', Null, 1.01, 15.60, 'f', '2017/07/14', 'Kaliyas Bio'),
	('Daliya', Null, 0.83, 10.40, 'f', '2019/07/01', 'Daliyas Bio'),
	('Nikolay', Null, 1.21, 28.7, 'm', '2009/12/06', 'Nikolays Bio')

--8 j-----------------------------------------------------------------------------------

CREATE TABLE Users(
-- unique number for every person there will be no more than 2 31 -1 people. (Auto incremented)
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX) CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024),
	LastLoginTime DATETIME2,
	IsDeleted BIT NOT NULL
)

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
VALUES
	('Kaloyan', 'kokoshanel', Null, '01.10.2021', 0),
	('Dilyana', 'kokoshanel', Null, '01.10.2021', 0),
	('Kaliya', 'kokoshanel', Null, '12.31.2020', 1),
	('Daliya', 'kokoshanel', Null, '12.31.2020', 1),
	('Nikolay', 'kokoshanel', Null, '01.10.2021', 0)

DELETE FROM Users -- Deleting row from table
WHERE Id = 5

--9-----------------------------------------------------------------------------------------

ALTER TABLE Users
DROP CONSTRAINT [PK__Users__3214EC07A4D6671C] --Removes the PK Constraint, the name is in Keys folder

ALTER TABLE Users
ADD CONSTRAINT PK_Users_CompositeIdUsername
PRIMARY KEY(Id, Username) -- Creates PK - combination of the 2 columns

--10-----------------------------------------------------------------------

ALTER TABLE Users
ADD CONSTRAINT CK_Users_PasswordLength
CHECK(LEN([Password]) >= 5)

--11-----------------------------------------------------------------------------

ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime
DEFAULT GETDATE() FOR LastLoginTime -- Sets the default value to the current date

--12---------------------------------------------------------------------------

ALTER TABLE Users
DROP CONSTRAINT PK_Users_CompositeIdUsername

ALTER TABLE Users
ADD CONSTRAINT PK_Users_Id
PRIMARY KEY(Id)

ALTER TABLE Users
ADD CONSTRAINT CK_Users_UsernameLength
CHECK(LEN(Username) >= 3)

--13 j---------------------------------------------------------------

CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	DirectorId BIGINT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear INT NOT NULL,
	[Length] DECIMAL(4, 2),
	GenreId BIGINT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId BIGINT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(4, 2),
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors(DirectorName, Notes)
VALUES
	('Kaloyan', NULL),
	('Dilyana', NULL),
	('Kaliya', NULL),
	('Daliya', NULL),
	('Nikolay', NULL)
	
INSERT INTO Genres(GenreName, Notes)
VALUES
	('Drama', NULL),
	('Triller', NULL),
	('Si-Fi', NULL),
	('Comedy', NULL),
	('Fantasy', NULL)

INSERT INTO Categories(CategoryName, Notes)
VALUES
	('SomeDrama', NULL),
	('SomeTriller', NULL),
	('SomeSi-Fi', NULL),
	('SomeComedy', NULL),
	('SomeFantasy', NULL)
	
INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
VALUES
	('SomeTitle', 4, 2010, 02.45, 3, 3, 6.06, NULL),
	('SomeTitle', 4, 2010, 02.45, 3, 3, 6.06, NULL),
	('SomeTitle', 4, 2010, 02.45, 3, 3, 6.06, NULL),
	('SomeTitle', 4, 2010, 02.45, 3, 3, 6.06, NULL),
	('SomeTitle', 4, 2010, 02.45, 3, 3, 6.06, NULL)

--14 j------------------------------------------------------------------

CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories(
	Id INT PRIMARY KEY  NOT NULL,
	CategoryName NVARCHAR(50),
	DailyRate DECIMAL(4, 2),
	WeeklyRate DECIMAL(4, 2),
	MonthlyRate DECIMAL(4, 2), 
	WeekendRate DECIMAL(4, 2)
)
INSERT INTO Categories(Id)
VALUES	(1), (2), (3)

CREATE TABLE Cars(
	Id INT PRIMARY KEY NOT NULL,
	PlateNumber VARCHAR(50) NOT NULL,
	Manufacturer VARCHAR(50),
	Model VARCHAR(50),
	CarYear INT, 
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Doors INT,
	Picture VARBINARY(MAX),
	Condition VARCHAR(50),
	Available BIT DEFAULT 1
)
INSERT INTO Cars(Id, PlateNumber, CategoryId)
VALUES	(1, 'B8364PN', 1),	(2, 'B8364PN', 2),	(3, 'B8364PN', 3)


CREATE TABLE Employees(
	Id INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(50)
)
INSERT INTO Employees(Id, FirstName, LastName)
VALUES	(1, 'Kaloyan', 'Zhelev'),	(2, 'Dilyana', 'Zheleva'),	(3, 'Kaliya', 'Zheleva')

CREATE TABLE Customers(
	Id INT PRIMARY KEY NOT NULL,
	DriverLicenceNumber NVARCHAR(50) UNIQUE NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(50),
	City NVARCHAR(50),
	ZIPCode NVARCHAR(50),
	Notes NVARCHAR(50)
)
INSERT INTO Customers(Id, DriverLicenceNumber, FullName)
VALUES	(1, '56bb 76', 'Kaloyan Zhelev'),	(2, '56bb 77', 'Dilyana Zheleva'),	(3, '56bb 78', 'Kaliya Zheleva')

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel DECIMAL(5, 2),
	KilometrageStart INT,
	KilometrageEnd INT,
	TotalKilometrage INT,
	StartDate DATE,
	EndDate DATE,
	TotalDays INT,
	RateApplied DECIMAL(4, 2),
	TaxRate DECIMAL(4, 2),
	OrderStatus VARCHAR(30), 
	Notes VARCHAR(30)
)
INSERT INTO RentalOrders(Id, EmployeeId, CustomerId, CarId)
VALUES	(1, 1, 1, 1), (2, 2, 2, 2), (3, 3, 3, 3)

--15 j-------------------------------------------------------------------

CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees(
	Id INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(50)
)
INSERT INTO Employees(Id, FirstName, LastName)
VALUES	(1, 'Kaloyan', 'Zhelev'),	(2, 'Dilyana', 'Zheleva'),	(3, 'Kaliya', 'Zheleva')

CREATE TABLE Customers(
	AccountNumber INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	PhoneNumber NVARCHAR(50), 
	EmergencyName NVARCHAR(50), 
	EmergencyNumber NVARCHAR(50),
	Notes NVARCHAR(50)
)
INSERT INTO Customers(AccountNumber, FirstName, LastName)
VALUES	(12345, 'Kaloyan', 'Zhelev'),	(12346, 'Dilyana', 'Zheleva'),	(12347, 'Kaliya', 'Zheleva')

CREATE TABLE RoomStatus(RoomStatus VARCHAR(10) PRIMARY KEY NOT NULL, Notes VARCHAR(100))
INSERT INTO Roomstatus(RoomStatus) VALUES ('free'), ('busy'), ('booked')

CREATE TABLE RoomTypes(RoomType VARCHAR(10) PRIMARY KEY NOT NULL, Notes VARCHAR(100))
INSERT INTO RoomTypes(RoomType) VALUES ('double'), ('single'), ('apartment')

CREATE TABLE BedTypes(BedType VARCHAR(10) PRIMARY KEY NOT NULL, Notes VARCHAR(100))
INSERT INTO BedTypes(BedType) VALUES ('double'), ('single'), ('baby')

CREATE TABLE Rooms(
	RoomNumber INT PRIMARY KEY NOT NULL, 
	RoomType VARCHAR(10) REFERENCES RoomTypes(RoomType),
	BedType VARCHAR(10) REFERENCES BedTypes(BedType),
	Rate DECIMAL(5, 2),
	RoomStatus VARCHAR(10) REFERENCES RoomStatus(RoomStatus),
	Notes VARCHAR(100))
INSERT INTO Rooms(RoomNumber) VALUES (101), (102), (103)

CREATE TABLE Payments(
	Id INT PRIMARY KEY NOT NULL, 
	EmployeeId INT REFERENCES Employees(Id),
	PaymentDate DATE,
	AccountNumber INT REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATE,
	LastDateOccupied DATE,
	TotalDays INT,
	AmountCharged INT,
	TaxRate DECIMAL(5, 2),
	TaxAmount DECIMAL(5, 2),
	PaymentTotal DECIMAL(5, 2) NOT NULL,
	Notes VARCHAR(100))
INSERT INTO Payments(Id, PaymentTotal) VALUES (101, 246), (102, 308), (103, 84)

CREATE TABLE Occupancies(
	Id INT PRIMARY KEY NOT NULL, 
	EmployeeId INT REFERENCES Employees(Id),
	DateOccupied DATE NOT NULL,
	AccountNumber INT REFERENCES Customers(AccountNumber),
	RoomNumber INT REFERENCES Rooms(RoomNumber),
	RateApplied DECIMAL(5, 2),
	PhoneCharge DECIMAL(5, 2),
	Notes VARCHAR(100))
INSERT INTO Occupancies(Id, DateOccupied) VALUES (1, '2021/01/28'), (2, '2021/01/28'), (3, '2021/01/28')

--16----------------------------------------------------------------

CREATE DATABASE SoftUni;
GO

USE SoftUni;

CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY NOT NULL, Name NVARCHAR(50) NOT NULL);

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
    AddressText NVARCHAR(100) NOT NULL,
    TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
);

CREATE TABLE Departments(Id INT PRIMARY KEY IDENTITY NOT NULL, [Name] NVARCHAR(50) NOT NULL);

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    LastName NVARCHAR(50),
    JobTitle NVARCHAR(100) NOT NULL,
    DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL,
    HireDate DATE,
    Salary DECIMAL(10, 2) NOT NULL,
    AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
);

--17. Backup Database---------------

BACKUP DATABASE SoftUni TO DISK = 'D:\softuni-backup.bak';

DROP DATABASE SoftUni;

RESTORE DATABASE SoftUni FROM DISK = 'D:\softuni-backup.bak';

--18------------------------------------------------------

INSERT INTO Towns([Name])
VALUES ('Sofia'), ('Plovdiv'), ('Varna'), ('Burgas');

INSERT INTO Departments([Name])
VALUES ('Engineering'), ('Sales'), ('Marketing'), ('Software Development'), ('Quality Assurance');

INSERT INTO Employees(FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
VALUES 
	('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, CONVERT(DATE, '02/03/2004', 103), 3500.00),
	('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, CONVERT(DATE, '02/03/2004', 103), 4000.00),
	('Maria', 'Petrova', 'Ivanova', 'Intern', 5, CONVERT(DATE, '28/08/2016', 103), 525.25),
	('Georgi', 'Teziev ', 'Ivanov', 'CEO', 2, CONVERT(DATE, '09/12/2007', 103), 3000.00),
	('Peter', 'Pan', 'Pan', 'Intern', 3, CONVERT(DATE, '28/08/2016', 103), 599.88);

	--CONVERT(DATE, '02/03/2004', 103) -> converts the date style to dd/MM/year, works for DATETIME also

--19 j-------------------------------

SELECT * FROM Towns

SELECT * FROM Departments 

SELECT * FROM Employees

--20 j-----------------------

SELECT * FROM Towns ORDER BY [Name] ASC;

SELECT * FROM Departments ORDER BY [Name] ASC;

SELECT * FROM Employees ORDER BY Salary DESC;

--21 j----------------------------------

SELECT [Name] FROM Towns ORDER BY [Name] ASC;

SELECT [Name] FROM Departments ORDER BY [Name] ASC;

SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC;

--22 j----------------------------------
USE SoftUni

UPDATE Employees
SET Salary *= 1.10;

SELECT Salary FROM Employees;

--23 j-----------------------------------

USE Hotel;

UPDATE Payments
SET TaxRate *= 0.97;

SELECT TaxRate FROM Payments;

--24 j-----------------------------------------------

USE Hotel

TRUNCATE TABLE Occupancies;

-- Crl + Shift + R -> Refresh