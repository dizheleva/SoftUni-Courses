--1---------------------------------------------------

CREATE TABLE Logs
(
    LogId INT NOT NULL,
    AccountId INT NOT NULL,
    OldSum MONEY NOT NULL,
    NewSum MONEY NOT NULL,
    CONSTRAINT PK_Logs PRIMARY KEY (LogId),
	CONSTRAINT FK_Account_Logs FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
)

CREATE TRIGGER tr_AddToLogsOnAccountUpdate
ON Accounts FOR UPDATE
AS
INSERT INTO Logs(AccountId, OldSum, NewSum)
SELECT i.Id, d.Balance, i.Balance
FROM inserted AS i
JOIN deleted AS d ON i.Id = d.Id
WHERE i.Balance != d.Balance

--2--------------------------------------------

CREATE TABLE NotificationEmails
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Recipient INT,
	[Subject] VARCHAR(500),
	Body VARCHAR(500)
)

CREATE TRIGGER tr_AddNewEmail ON Logs FOR INSERT
AS
BEGIN
	INSERT INTO NotificationEmails(Recipient, [Subject], Body)
	SELECT
	    i.AccountId AS Recipient, 
	    'Balance change for account: ' + CAST(i.AccountId AS VARCHAR(15)) AS [Subject],
	    'On ' + CAST(GETDATE() AS VARCHAR(50)) + ' your balance was changed from ' +
	    CAST(i.OldSum AS VARCHAR(30)) + ' to ' + CAST(i.NewSum AS VARCHAR(30)) + '.' AS Body
		FROM inserted AS i	
END

--3-----------------------------------------------------

CREATE PROCEDURE usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN
	UPDATE [dbo].[Accounts]
	   SET [dbo].[Accounts].[Balance] += @MoneyAmount
	 WHERE [dbo].[Accounts].[Id] = @AccountId
END

/*DECLARE @targetAccountId INT = (SELECT a.Id FROM dbo.Accounts a WHERE a.Id = @AccountId)
	IF(@MoneyAmount < 0 OR @MoneyAmount IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid amount of money', 16, 1)
		RETURN
	END	
	IF(@targetAccountId IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid Id Parameter', 16, 2)
		RETURN
	END*/

--4---------------------------------------------------

CREATE PROCEDURE usp_WithdrawMoney(@AccountId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN
	UPDATE Accounts
	   SET Accounts.Balance -= @MoneyAmount
	 WHERE Accounts.Id = @AccountId
END

--5----------------------------------------------------

CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN
	UPDATE [dbo].[Accounts]
	   SET [dbo].[Accounts].[Balance] -= @MoneyAmount
	 WHERE [dbo].[Accounts].[Id] = @SenderId
	 UPDATE [dbo].[Accounts]
	   SET [dbo].[Accounts].[Balance] += @MoneyAmount
	 WHERE [dbo].[Accounts].[Id] = @ReceiverId
END

--6--------------------------------------------------------

/* Trigger for validating user's level when buying items */
CREATE TRIGGER tr_NewItemsLevelRestriction ON UserGameItems
INSTEAD OF INSERT
AS 
BEGIN
	 DECLARE @gameId INT = (SELECT UserGameId FROM inserted)
	 DECLARE @level INT = (SELECT [Level] FROM UsersGames WHERE Id = @gameId)
	 DECLARE @itemId INT = (SELECT ItemId FROM inserted)
	 DECLARE @minLevel INT = (SELECT MinLevel FROM Items WHERE Id = @itemId)
	 	 
	 IF(@level >= @minLevel)
     BEGIN
		INSERT INTO UserGameItems
		VALUES(@itemId, @gameId)
	 END
END


/* Add bonus cash */
UPDATE UsersGames 
  SET UsersGames.Cash+= 50000
FROM UsersGames
     JOIN Users ON Users.Id = UsersGames.UserId
     JOIN Games ON Games.Id = UsersGames.GameId
WHERE Users.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos') AND Games.[Name] = 'Bali'

/* Take cash, add items */

CREATE PROC usp_BuyItem(@itemId INT, @gameName NVARCHAR(50), @username NVARCHAR(50))
AS
BEGIN TRANSACTION
	DECLARE @game INT = (SELECT Id FROM Games WHERE [Name] = @gameName);	
	DECLARE @user INT = (SELECT Id FROM Users WHERE Username = @username);
	DECLARE @item INT = (SELECT Id FROM Items WHERE Id = @itemId);

	IF(@user IS NULL OR @item IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid User or Item Id', 16, 1)
		RETURN
	END

	DECLARE @cash MONEY = (SELECT UsersGames.Cash FROM UsersGames WHERE UsersGames.UserId = @user AND UsersGames.GameId = @game);
	DECLARE @itemPrice MONEY = (SELECT Items.Price FROM Items WHERE Items.Id = @itemId);
	DECLARE @userGameId INT = (SELECT UsersGames.Id FROM UsersGames WHERE UsersGames.UserId = @user AND UsersGames.GameId = @game)
	
	IF(@cash < @itemPrice)
	BEGIN
		ROLLBACK
		RAISERROR('Insufficient funds!', 16, 2)
		RETURN
	END

	UPDATE UsersGames
	   SET Cash -= @itemPrice
	 WHERE UserId = @user AND GameId = @game
	INSERT INTO UserGameItems
	    VALUES(@itemId, @userGameId)
	
COMMIT

-- ('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')

DECLARE @firstCount INT = 251

WHILE (@firstCount <= 299)
BEGIN			
	EXEC usp_BuyItem @itemId = @firstCount,	@gameName = 'Bali', @username = 'baleremuda'
	EXEC usp_BuyItem @itemId = @firstCount,	@gameName = 'Bali', @username = 'loosenoise'
	EXEC usp_BuyItem @itemId = @firstCount,	@gameName = 'Bali', @username = 'inguinalself'
	EXEC usp_BuyItem @itemId = @firstCount,	@gameName = 'Bali', @username = 'buildingdeltoid'
	EXEC usp_BuyItem @itemId = @firstCount,	@gameName = 'Bali', @username = 'monoxidecos'

	SET @firstCount+=1
END

DECLARE @secondCount INT = 501

WHILE (@secondCount <= 539)
BEGIN			
	EXEC usp_BuyItem @itemId = @secondCount,	@gameName = 'Bali', @username = 'baleremuda'
	EXEC usp_BuyItem @itemId = @secondCount,	@gameName = 'Bali', @username = 'loosenoise'
	EXEC usp_BuyItem @itemId = @secondCount,	@gameName = 'Bali', @username = 'inguinalself'
	EXEC usp_BuyItem @itemId = @secondCount,	@gameName = 'Bali', @username = 'buildingdeltoid'
	EXEC usp_BuyItem @itemId = @secondCount,	@gameName = 'Bali', @username = 'monoxidecos'

	SET @secondCount+=1
END

--7-----------------------------------------------------------------

DECLARE @UserName VARCHAR(50) = 'Stamat'
DECLARE @GameName VARCHAR(50) = 'Safflower'
DECLARE @UserID int = (SELECT Id FROM Users WHERE Username = @UserName)
DECLARE @GameID int = (SELECT Id FROM Games WHERE [Name] = @GameName)
DECLARE @UserCash money = (SELECT Cash FROM UsersGames WHERE UserId = @UserID AND GameId = @GameID)
DECLARE @ItemsTotalPrice money
DECLARE @UserGameID int = (SELECT Id FROM UsersGames WHERE UserId = @UserID AND GameId = @GameID)

BEGIN TRANSACTION
	SET @ItemsTotalPrice = (SELECT SUM(Price) FROM Items WHERE MinLevel BETWEEN 11 AND 12)

	IF(@UserCash >= @ItemsTotalPrice)
	BEGIN
		INSERT INTO UserGameItems
		SELECT Id, @UserGameID FROM Items
		WHERE Id IN (SELECT Id FROM Items WHERE MinLevel BETWEEN 11 AND 12)

		UPDATE UsersGames
		SET Cash -= @ItemsTotalPrice
		WHERE GameId = @GameID AND UserId = @UserID
		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END

SET @UserCash = (SELECT Cash FROM UsersGames WHERE UserId = @UserID AND GameId = @GameID)
BEGIN TRANSACTION
	SET @ItemsTotalPrice = (SELECT SUM(Price) FROM Items WHERE MinLevel BETWEEN 19 AND 21)

	IF(@UserCash >= @ItemsTotalPrice)
	BEGIN
		INSERT INTO UserGameItems
		SELECT Id, @UserGameID FROM Items
		WHERE Id IN (SELECT Id FROM Items WHERE MinLevel BETWEEN 19 AND 21)

		UPDATE UsersGames
		SET Cash -= @ItemsTotalPrice
		WHERE GameId = @GameID AND UserId = @UserID
		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END

SELECT [Name] AS [Item Name]
FROM Items
WHERE Id IN (SELECT ItemId FROM UserGameItems WHERE UserGameId = @userGameID)
ORDER BY [Item Name]

--8------------------------------------------------------------------------

CREATE PROCEDURE usp_AssignProject(@employeeId INT, @projectID INT)
AS
BEGIN TRANSACTION
	DECLARE @ProjectsCount INT = (SELECT COUNT(EmployeeID) FROM EmployeesProjects WHERE EmployeeID = @employeeId)
	IF(@ProjectsCount < 3)
	BEGIN
		INSERT INTO EmployeesProjects
	    VALUES(@employeeId, @projectID)
	END	
	ELSE
	BEGIN
		ROLLBACK
		RAISERROR ('The employee has too many projects!', 16, 1)
	END
COMMIT

--9-----------------------------------------------------------------------------

CREATE TABLE Deleted_Employees
(
	EmployeeId INT PRIMARY KEY,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	MiddleName NVARCHAR(50),
	JobTitle NVARCHAR(50),
	DepartmentId INT FOREIGN KEY REFERENCES Departments(DepartmentId),
	Salary MONEY
)

CREATE TRIGGER tr_AddDelitedEmployeesRecord ON Employees
FOR DELETE
AS 
INSERT INTO Deleted_Employees(FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary)
SELECT d.FirstName, d.LastName, d.MiddleName, d.JobTitle, d.DepartmentId, d.Salary
FROM deleted AS d