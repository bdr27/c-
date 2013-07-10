
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/22/2013 19:28:34
-- Generated from EDMX file: G:\CP2011\C-\Assignment2\Iteration1\Iteration1\CheckersDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [E:\Documents\Dropbox\University\2013S1\CP2011\c-\Assignment2\DataBase\CheckersDatabase.mdf];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [PlayerID] int IDENTITY(1,1) NOT NULL,
    [PlayerUsername] nvarchar(max)  NOT NULL,
    [PlayerPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HighScores'
CREATE TABLE [dbo].[HighScores] (
    [PlayerMoves] int  NOT NULL,
    [PlayerScoreDate] datetime  NOT NULL,
    [PlayerID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PlayerID] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([PlayerID] ASC);
GO

-- Creating primary key on [PlayerMoves], [PlayerScoreDate], [PlayerID] in table 'HighScores'
ALTER TABLE [dbo].[HighScores]
ADD CONSTRAINT [PK_HighScores]
    PRIMARY KEY CLUSTERED ([PlayerMoves], [PlayerScoreDate], [PlayerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PlayerID] in table 'HighScores'
ALTER TABLE [dbo].[HighScores]
ADD CONSTRAINT [FK_PlayerHighScores]
    FOREIGN KEY ([PlayerID])
    REFERENCES [dbo].[Players]
        ([PlayerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerHighScores'
CREATE INDEX [IX_FK_PlayerHighScores]
ON [dbo].[HighScores]
    ([PlayerID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------