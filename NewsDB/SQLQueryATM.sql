-------------------------------------------------------------------
-- This script will create a sample database "ATM" in        --
-- MS SQL Server and will populate sample data in its tables.    --
-------------------------------------------------------------------

USE master
GO

CREATE DATABASE ATM
COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

USE ATM
GO

CREATE TABLE CardAccounts (
  Id int IDENTITY NOT NULL,
  CardNumber  char(10) NOT NULL,
  CardPIN char(4) NOT NULL,
  CardCash money NOT NULL
  CONSTRAINT PK_CardAccounts PRIMARY KEY CLUSTERED(Id ASC)
)
GO

SET IDENTITY_INSERT CardAccounts ON

INSERT INTO CardAccounts (Id, CardNumber, CardPIN, CardCash)
VALUES (1, '4920567898', '1234', 2000.90),
	   (2, '1527904456', '3345', 16989.89),
	   (3, '5467332498', '2231', 50.60),
	   (4, '3344552232', '5664', 300.0),
	   (5, '1234543287', '1112', 1005.67),
	   (6, '1578905623', '7787', 20000.99),
	   (7, '1111199997', '2239', 7000.77)

SET IDENTITY_INSERT CardAccounts OFF
GO

CREATE TABLE TransactionHistory (
  Id int IDENTITY NOT NULL,
  CardNumber  char(10) NOT NULL,
  TransactionDate datetime NOT NULL,
  Amount money NOT NULL
  CONSTRAINT PK_TransactionHistory PRIMARY KEY CLUSTERED(Id ASC)
)
GO