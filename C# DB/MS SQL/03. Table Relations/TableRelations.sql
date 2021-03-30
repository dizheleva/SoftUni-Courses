--1-----------------------------
CREATE TABLE Passports(
PassportID INT PRIMARY KEY,
PassportNumber VARCHAR(50) UNIQUE

)
INSERT INTO Passports(PassportID, PassportNumber)
VALUES (101,'N34FG21B'),(102,'K65LO4R7'),(103,'ZE657QP2')
  
CREATE TABLE Persons(
PersonID INT PRIMARY KEY,
FirstName VARCHAR(50),
Salary DECIMAL(7, 2),
PassportID INT
CONSTRAINT FK_PassportNumbers FOREIGN KEY
(PassportID) REFERENCES Passports(PassportID)
)  
  
INSERT INTO Persons (PersonId, FirstName, Salary, PassportID)
VALUES(1, 'Roberto', 43300.00, 102), (2, 'Tom', 56100.00, 103), (3, 'Yana', 60200.00, 101)

/*ALTER TABLE Persons
ADD CONSTRAINT pk_PersonID PRIMARY KEY (PersonID)

ALTER TABLE Passports
ADD CONSTRAINT pk_PassportsID PRIMARY KEY ([PassportID]) 

ALTER TABLE Persons
ADD CONSTRAINT fk_Pesrons_Passports FOREIGN KEY([PassportID])
REFERENCES Passports([PassportID])*/

--2--------------------------------------

CREATE TABLE Manufacturers(
ManufacturerID INT PRIMARY KEY,
[Name] VARCHAR(50),
EstablishedOn DATE
)

CREATE TABLE Models(
ModelID INT PRIMARY KEY,
[Name] VARCHAR(50),
ManufacturerID INT
CONSTRAINT FK_Manufacturers
FOREIGN KEY (ManufacturerID)
REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers (ManufacturerID, [Name], EstablishedOn)
VALUES(1, 'BMW', '07/03/1916'), (2, 'Tesla', '01/01/2003'), (3, 'Lada', '01/05/1966') 

INSERT INTO Models(ModelID, [Name], ManufacturerID)
VALUES (101, 'X1', 1), (102, 'i6', 1), (103, 'Model S', 2), (104, 'Model X', 2), (105, 'Model 3', 2), (106, 'Nova', 3)

--3--------------------------------------------------------------

CREATE TABLE Students(
StudentID INT PRIMARY KEY,
[Name] NVARCHAR(50)
)
 
CREATE TABLE Exams(
ExamID INT PRIMARY KEY,
[Name] NVARCHAR(250)
)
 
CREATE TABLE StudentsExams(
StudentID INT,
ExamID INT,
CONSTRAINT PK_StudentID_ExamID PRIMARY KEY(StudentID, ExamID),
CONSTRAINT FK_StudentsExams_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
CONSTRAINT FK_StudentsExams_ExamID FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
)
 
INSERT INTO Students
VALUES (1, 'Mila'), (2, 'Toni'), (3, 'Ron')
 
INSERT INTO Exams 
VALUES (101, 'SpringMVC'), (102, 'Neo4j'), (103, 'Oracle 11g')
 
INSERT INTO StudentsExams 
VALUES (1, 101), (1, 102), (2, 101), (3, 103), (2, 102), (2, 103)

--4------------------------------------------------------------------

CREATE TABLE Teachers(
TeacherID INT PRIMARY KEY IDENTITY(101, 1), --identity(101, 1) - starts from 101, increments with 1
[Name] VARCHAR(50),
ManagerID INT FOREIGN KEY REFERENCES Teachers(TeacherID)
); 

INSERT INTO Teachers
VALUES ('John', NULL), ('Maya', 106), ('Silvia', 106), ('Ted', 105), ('Mark', 101), ('Greta', 101);

 

--ALTER TABLE Teachers
--ADD CONSTRAINT fk_manager_id FOREIGN KEY(ManagerID)
--REFERENCES Teachers(TeacherID)

--ON DELETE NO ACTION;
--SET FOREIGN_KEY_CHECKS = 0;

--5--------------------------------------------------------------------------

CREATE TABLE Cities
(
    CityID INT PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Birthday DATE,
    CityID INT NOT NULL,
    CONSTRAINT FK_Customers_Cities FOREIGN KEY (CityID) REFERENCES Cities(CityID)
)

CREATE TABLE Orders
(
    OrderID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes
(
    ItemTypeID INT PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Items
(
    ItemID INT PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    ItemTypeID INT NOT NULL,
    CONSTRAINT FK_Items_ItemTypes FOREIGN KEY(ItemTypeID) REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems
(
    OrderID INT NOT NULL,
    ItemID INT NOT NULL,
    CONSTRAINT PK_OrderItems PRIMARY KEY(OrderID,ItemID),
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    CONSTRAINT FK_OrderItems_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
)

--6--------------------------------------------------------------------

CREATE TABLE Majors(
	MajorID INT PRIMARY KEY IDENTITY,
    [Name] VARCHAR(50)
);

CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY,
    StudentNumber VARCHAR(12),
    StudentName VARCHAR(50),
    MajorID INT,
    CONSTRAINT fk_students_majors
    FOREIGN KEY (MajorID)
    REFERENCES Majors(MajorID)
);

CREATE TABLE Payments(
	PaymentID INT PRIMARY KEY IDENTITY,
    PaymentDate DATE,
    PaymentAmount DECIMAL(8, 2),
    StudentID INT,
    CONSTRAINT fk_payments_students
    FOREIGN KEY (StudentID)
    REFERENCES Students(StudentID)
);

CREATE TABLE Subjects(
	SubjectID INT PRIMARY KEY IDENTITY,
    SubjectName VARCHAR(50)
);

CREATE TABLE Agenda(
	StudentID INT,
    SubjectID INT,
    CONSTRAINT pk_agenda PRIMARY KEY (StudentID, SubjectID),
    CONSTRAINT fk_agenda_students FOREIGN KEY (StudentID) REFERENCES Students (StudentID),
    CONSTRAINT fk_agenda_subjects FOREIGN KEY (SubjectID) REFERENCES Subjects (SubjectID)
);

--7------------------------------------------------------------------



--9------------------------------------------------------------------

USE Geography;

SELECT MountainRange, PeakName, Elevation 
FROM Peaks
     JOIN Mountains ON Peaks.MountainId = Mountains.Id
WHERE Peaks.MountainId =
(
    SELECT Id
    FROM Mountains
    WHERE MountainRange = 'Rila'
)
ORDER BY Peaks.Elevation DESC;




