USE master;
GO

CREATE DATABASE movies;
GO

USE movies;
GO

-- CREATE TABLES

CREATE TABLE [dbo].[Movie] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] VARCHAR(100) NOT NULL,
    [ProductionYear] DATETIME NOT NULL,
    [BoxOffice] INT NOT NULL,
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

CREATE TABLE [dbo].[Review] (
	[Id] INT NOT NULL IDENTITY(1,1),
	[UserKey] VARCHAR(36) NOT NULL,
	[MovieId] INT NOT NULL CONSTRAINT FK_Review_Movie REFERENCES dbo.Movie(Id),
	[Rating] INT NOT NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED([Id] ASC)
);


-- CREATE SUPERID

-- yyyyMMddhhmmss


