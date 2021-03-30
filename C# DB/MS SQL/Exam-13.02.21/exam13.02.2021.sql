CREATE DATABASE Bitbucket

--1--------------------------------------
CREATE TABLE Users
(
Id	Int Primary key Identity,
Username	VARCHAR(30) NOT NULL,
Password	VARCHAR(30) NOT	NULL,
Email	VARCHAR(50) NOT	NULL
)

CREATE TABLE Repositories
(
Id	Int Primary key Identity,
Name VARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors
(
RepositoryId	Int	NOT NULL,
ContributorId	Int NOT NULL

CONSTRAINT PK_RepositoriesContributors PRIMARY KEY (RepositoryId, ContributorId),

CONSTRAINT FK_RepositoriesContributors_Repository FOREIGN KEY (RepositoryId) REFERENCES Repositories(Id),
CONSTRAINT FK_RepositoriesContributors_Contributor FOREIGN KEY (ContributorId) REFERENCES Users(Id)
)

CREATE TABLE Issues
(
Id	Int Primary key Identity,
Title	VARCHAR(255) NOT NULL,
IssueStatus	CHAR(6) NOT NULL,
RepositoryId	Int	References Repositories(Id),
AssigneeId	Int References Users(Id)
)

CREATE TABLE Commits
(
Id	Int Primary key Identity,
Message	VARCHAR(255) NOT NULL,
IssueId	Int References Issues(Id) NULL,
RepositoryId	Int	References Repositories(Id),
ContributorId	Int References Users(Id)
)

CREATE TABLE Files
(
Id	Int Primary key Identity,
Name VARCHAR(100) NOT NULL,
Size	Decimal(18, 2) NOT NULL,
ParentId	Int References Files(Id),
CommitId	Int References Commits(Id)
)

--2-----------------------------------------------------

INSERT INTO Files (Name,	Size,	ParentId,	CommitId) VALUES
('Trade.idk',	2598.0,	1,	1),
('menu.net',	9238.31,	2,	2),
('Administrate.soshy',	1246.93,	3,	3),
('Controller.php',	7353.15, 4,	4),
('Find.java',	9957.86,	5,	5),
('Controller.json',	14034.87,	3,	6),
('Operate.xix',	7662.92,	7,	7)


INSERT INTO  Issues (Title,	IssueStatus,	RepositoryId,	AssigneeId) VALUES
('Critical Problem with HomeController.cs file',	'open',	1,	4),
('Typo fix in Judge.html',	'open', 4,	3),
('Implement documentation for UsersService.cs',	'closed',	8,	2),
('Unreachable code in Index.cs',	'open',	9,	8)

--3-------------------------------------------------------

UPDATE Issues
SET IssueStatus = 'closed'
WHERE AssigneeId = 6

--4-----------------------------------------------------

DELETE FROM RepositoriesContributors
WHERE RepositoryId = (SELECT Id FROM Repositories WHERE Name = 'Softuni-Teamwork')

DELETE FROM Issues
WHERE RepositoryId = (SELECT Id FROM Repositories WHERE Name = 'Softuni-Teamwork')

--5------------------------------------------------------

SELECT Id,	Message,	RepositoryId,	ContributorId FROM Commits 
ORDER BY Id, Message, RepositoryId, ContributorId

--6-----------------------------------------------
/*Select all of the files, which have size, greater than 1000, and a name containing "html". 
Order them by size (descending), id (ascending) and by file name (ascending).*/

SELECT Id,	Name,	Size FROM Files
WHERE Size > 1000 AND Name LIKE '%html%'
ORDER BY Size DESC, Id, Name 

--7-----------------------------------------------
/*Select all of the issues, and the users that are assigned to them, 
so that they end up in the following format: 
{username} : {issueTitle}. Order them by issue id (descending) and issue assignee (ascending).*/

SELECT i.Id,	u.Username + ' : ' + i.Title AS IssueAssignee FROM Issues As i
JOIN Users As u ON u.Id = i.AssigneeId
ORDER BY i.Id DESC, i.AssigneeId

--8-----------------------------------------------
/*Select all of the files, which are NOT a parent to any other file. 
Select their size of the file and add "KB" to the end of it. 
Order them file id (ascending), file name (ascending) and file size (descending).*/

SELECT f.Id,	f.Name,	CONVERT(VARCHAR, f.Size) + 'KB' AS Size  FROM Files AS f
LEFT JOIN Files AS p ON f.Id = p.ParentId
GROUP BY f.Id,p.ParentId, f.Name, f.Size
HAVING p.ParentId IS NULL
ORDER BY Id, Name, Size DESC

--9-------------------------------------------------
/*Select the top 5 repositories in terms of count of commits. 
Order them by commits count (descending), repository id (ascending) then by repository name (ascending).*/

SELECT TOP(5) r.Id, r.Name, COUNT(*) AS [Commits] FROM RepositoriesContributors AS rc
JOIN Repositories AS r ON r.Id = rc.RepositoryId
JOIN Commits As c ON c.RepositoryId = rc.RepositoryId
GROUP BY r.Id,r.Name, rc.RepositoryId
ORDER BY [Commits] DESC, rc.RepositoryId, r.Name

--10---------------------------------------------------------
/*Select all users which have commits. 
Select their username and average size of the file, which were uploaded by them. 
Order the results by average upload size (descending) and by username (ascending).*/

SELECT u.Username,	 AVG(f.Size) AS [Size] FROM Users AS u
JOIN Commits AS c ON c.ContributorId = u.Id
JOIN Files AS f ON f.CommitId = c.Id
GROUP BY u.Username
ORDER BY [Size] DESC, u.Username

--11--------------------------------------------------

CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(100))
RETURNS INT
AS
BEGIN
RETURN (SELECT COUNT(*) FROM Commits WHERE ContributorId = (SELECT Id FROM Users WHERE Username = @username))
END
--12--------------------------------------------------

CREATE PROCEDURE usp_SearchForFiles(@fileExtension VARCHAR(20))
AS 
BEGIN
SELECT Id, Name, CONVERT(VARCHAR, Size) + 'KB' AS Size FROM Files 
WHERE Name LIKE '%' + @fileExtension
Order by Id, Name, Size DESC
END

exec usp_SearchForFiles 'txt'
