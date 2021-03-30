namespace Ado.Net_Exercise.Constants
{
    public static class Queries
    {
        public const string CreateDB = "CREATE DATABASE MinionsDB";
        
        public const string CreateTables =
            @"USE MinionsDB
            CREATE TABLE Countries(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))
            CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))
            CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))
            CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))
            CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))
            CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))

            INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')
            INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)
            INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)
            INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')
            INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)
            INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

        public const string CountVillainNames =
            @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
            FROM Villains AS v
            JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                GROUP BY v.Id, v.Name
                HAVING COUNT(mv.VillainId) > 3 
            ORDER BY COUNT(mv.VillainId)";

        public const string SelectVillainNameById = "SELECT Name FROM Villains WHERE Id = @villainId";
        
        public const string SelectVillainsMinions =
            @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @villainId
                                ORDER BY m.Name";

        public const string SelectVillainIdByName = "SELECT Id FROM Villains WHERE Name = @villainName";

        public const string SelectMinionByName = "SELECT Id FROM Minions WHERE Name = @minionName";

        public const string SelectTownIdByName = "SELECT Id FROM Towns WHERE Name = @townName";
        
        public const string AddNewTown = "INSERT INTO Towns (Name) VALUES (@townName)";

        public const string AddNewVillain = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

        public const string AddNewMinion = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

        public const string AddVillainMinion  = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

        public const string UpdateTownNames =
            @"UPDATE Towns
               SET Name = UPPER(Name)
             WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

        public const string SelectTownsByCountry =
            @"SELECT t.Name 
               FROM Towns as t
               JOIN Countries AS c ON c.Id = t.CountryCode
              WHERE c.Name = @countryName";

        public const string DeleteFromVillainMinions = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";

        public const string DeleteVillain = "DELETE FROM Villains WHERE Id = @villainId";

        public const string SelectMinionNames = "SELECT Name FROM Minions";

        public const string UpdateMinionNames =
            @"UPDATE Minions
               SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
             WHERE Id = @minionId";

        public const string SelectMinionNameAge = "SELECT Name, Age FROM Minions";

        public const string ExecuteProcGetOlder = "EXECUTE usp_GetOlder @minionId";
           
        public const string SelectNameAgeByMinionId = "SELECT Name, Age FROM Minions WHERE Id = @minionId";

    }
}