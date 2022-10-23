USE master;
GO

CREATE DATABASE movies;
GO

USE movies;
GO

CREATE TABLE [dbo].[Movie] (
    [Id] VARCHAR(32) NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    [ProductionYear] DATETIME NOT NULL,
    [BoxOffice] INT NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED([Id] ASC)
);

CREATE TABLE [dbo].[Actor] (
    [Id] VARCHAR(32) NOT NULL,
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED([Id] ASC) 
);


CREATE TABLE [dbo].[Director] (
    [Id] VARCHAR(32) NOT NULL,
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED([Id] ASC)
);
GO


CREATE TABLE [dbo].[ActorMovie] (
    [MovieId] VARCHAR(32) NOT NULL CONSTRAINT FK_ActorMovie_Movie REFERENCES dbo.Movie(Id),
    [ActorId] VARCHAR(32) NOT NULL CONSTRAINT FK_ActorMovie_Actor REFERENCES dbo.Actor(Id)
    CONSTRAINT [PK_ActorMovie] PRIMARY KEY CLUSTERED([MovieId] ASC, [ActorId] ASC)
);

CREATE TABLE [dbo].[DirectorMovie] (
    [MovieId] VARCHAR(32) NOT NULL CONSTRAINT FK_DirectorMovie_Movie REFERENCES dbo.Movie(Id),
    [DirectorId] VARCHAR(32) NOT NULL CONSTRAINT FK_DirectorMovie_Director REFERENCES dbo.Director(Id)
    CONSTRAINT [PK_DirectorMovie] PRIMARY KEY CLUSTERED([MovieId] ASC, [DirectorId] ASC)
);
GO