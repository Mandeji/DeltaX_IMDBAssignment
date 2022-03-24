IF NOT EXISTS (SELECT 1 from master..sysdatabases WHERE name = N'IMDB_Images' )
BEGIN
CREATE DATABASE "IMDB_Images"
END
GO

USE "IMDB_Images"
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE [name] = N'Actor')
BEGIN
CREATE TABLE [Actor] (
    [Id]		     INT    IDENTITY (1, 1)		NOT NULL,
    [Actor_Name]     VARCHAR (30)				NOT NULL,
	[Date_Birth]	 DATETIME					NOT NULL,
	[Bio]			 NVARCHAR (100)				NULL,
	[Gender]		 VARCHAR (10)				NULL,
	CONSTRAINT [PK_Actor] PRIMARY KEY ([Id] ASC)
);
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE [name] = N'Producer')
BEGIN
CREATE TABLE [Producer] (
    [Id]		     INT    IDENTITY (1, 1)		NOT NULL,
    [Producer_Name]  VARCHAR (30)				NOT NULL,
	[Date_Birth]	 DATETIME					NOT NULL,
	[Company_Name]   NVARCHAR (50)				NOT NULL,
	[Bio]			 NVARCHAR (100)				NULL,
	[Gender]		 VARCHAR (10)				NULL,
	CONSTRAINT [PK_Producer] PRIMARY KEY ([Id] ASC)
);
END
GO


IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE [name] = N'Movie')
BEGIN
CREATE TABLE [Movie] (
    [Id]		     INT     IDENTITY (1, 1)	NOT NULL,
    [Movie_Name]   	 NVARCHAR (50)				NOT NULL,
	[Date_Release]   DATETIME					NOT NULL,
	[Producer_Id]	 INT						NOT NULL,
	[Description]    NVARCHAR (100)				NULL,
	[Poster_Path]    NVARCHAR (500)				NULL,
	CONSTRAINT [PK_Movie] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_Movie_Producer] FOREIGN KEY([Producer_Id]) REFERENCES [Producer] ([Id])
);
END
GO


IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE [name] = N'MovieActor')
BEGIN
CREATE TABLE [MovieActor] (
    [Id]		     INT     IDENTITY (1, 1)	NOT NULL,
	[Movie_Id]		 INT						NOT NULL,
	[Actor_Id]		 INT						NOT NULL,
	CONSTRAINT [PK_MovieActor] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_MovieActor_Movie] FOREIGN KEY([Movie_Id]) REFERENCES [Movie]    ([Id]),
	CONSTRAINT [FK_MovieActor_Actor] FOREIGN KEY([Actor_Id]) REFERENCES [Actor]    ([Id]),
	CONSTRAINT UC_MovieActor UNIQUE ([Movie_Id],[Actor_Id])
);
END
GO
