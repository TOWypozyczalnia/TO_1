USE master;
GO

CREATE DATABASE movies;
GO

USE movies;
GO

-- tables

CREATE TABLE [dbo].[Movie] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] VARCHAR(100) NOT NULL,
    [ProductionYear] DATETIME NOT NULL,
    [BoxOffice] INT NOT NULL,
    [Unavailable] INT,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [dbo].[Actor] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED([Id] ASC) 
);


CREATE TABLE [dbo].[Director] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED([Id] ASC)
);
GO


CREATE TABLE [dbo].[ActorMovie] (
    [MovieId] INT NOT NULL CONSTRAINT FK_ActorMovie_Movie REFERENCES dbo.Movie(Id),
    [ActorId] INT NOT NULL CONSTRAINT FK_ActorMovie_Actor REFERENCES dbo.Actor(Id)
    CONSTRAINT [PK_ActorMovie] PRIMARY KEY CLUSTERED([MovieId] ASC, [ActorId] ASC)
);


CREATE TABLE [dbo].[DirectorMovie] (
    [MovieId] INT NOT NULL CONSTRAINT FK_DirectorMovie_Movie REFERENCES dbo.Movie(Id),
    [DirectorId] INT NOT NULL CONSTRAINT FK_DirectorMovie_Director REFERENCES dbo.Director(Id)
    CONSTRAINT [PK_DirectorMovie] PRIMARY KEY CLUSTERED([MovieId] ASC, [DirectorId] ASC)
);
GO

CREATE TABLE [dbo].[LoggedUser] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Username] VARCHAR(32) NOT NULL,
    [UserKey] VARCHAR(36) NOT NULL,
    [MoviesWatched] INT NOT NULL,
    CONSTRAINT [PK_LoggedUser] PRIMARY KEY CLUSTERED([Id] ASC)
);
GO

CREATE TABLE [dbo].[Review] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [UserId] INT NOT NULL CONSTRAINT FK_Review_LoggedUser REFERENCES dbo.LoggedUser(Id),
    [MovieId] INT NOT NULL CONSTRAINT FK_Review_Movie REFERENCES dbo.Movie(Id),
    [Rating] INT NOT NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [dbo].[Reservation] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [UserId] INT NOT NULL CONSTRAINT FK_Reservation_LoggedUser REFERENCES dbo.LoggedUser(Id),
    [MovieId] INT NOT NULL CONSTRAINT FK_Reservation_Movie REFERENCES dbo.Movie(Id),
    [ReservationDate] DATETIME NOT NULL,
    [ExpirationDate ] DATETIME,
    CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [dbo].[LoggedUserMovie] (
    [UserId] INT NOT NULL CONSTRAINT FK_LoggedUserMovie_LoggedUser REFERENCES dbo.LoggedUser(Id),
    [MovieId] INT NOT NULL CONSTRAINT FK_LoggedUserMovie_Movie REFERENCES dbo.Movie(Id),
    CONSTRAINT [PK_LoggedUserMovie] PRIMARY KEY CLUSTERED([UserId] ASC, [MovieId] ASC)
);
GO

-- functions

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id('fntGetUserSimilarites') AND xtype IN ('FN', 'IF', 'TF'))
	DROP FUNCTION [dbo].[fntGetUserSimilarites]
GO


CREATE FUNCTION [dbo].[fntGetUserSimilarites] (@userId INT)
RETURNS @T TABLE(UserId INT, Count INT)
AS
BEGIN
  DECLARE @SimilaritiesCount TABLE (UserId INT, MovieId INT);
  DECLARE @CurrentUserId INT;
  DECLARE UserMovieCursor CURSOR FOR
  SELECT
    Id
  FROM dbo.LoggedUser;

  OPEN UserMovieCursor;
  FETCH NEXT FROM UserMovieCursor INTO @CurrentUserId;

  WHILE @@FETCH_STATUS = 0
  BEGIN
    IF (@UserId != @CurrentUserId)
    BEGIN
      INSERT INTO @SimilaritiesCount
      SELECT
        @CurrentUserId AS UserId,
        MovieId
      FROM (
        SELECT
          MovieId
        FROM dbo.LoggedUserMovie
        WHERE UserId = @CurrentUserId
        INTERSECT
        SELECT
          MovieId
        FROM dbo.LoggedUserMovie
        WHERE UserId = @userId
      ) Counter
    END
    FETCH NEXT FROM UserMovieCursor INTO @CurrentUserId;
  END

  INSERT INTO @T
  SELECT 
    UserId,
    Count(MovieId) AS Count 
  FROM @SimilaritiesCount
  GROUP BY UserId;

  RETURN
END
GO
