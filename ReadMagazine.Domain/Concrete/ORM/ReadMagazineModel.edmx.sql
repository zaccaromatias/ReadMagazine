
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/18/2014 16:44:48
-- Generated from EDMX file: C:\Users\popo\documents\visual studio 2012\Projects\ReadMagazine\ReadMagazine.Domain\Concrete\ORM\ReadMagazineModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ReadMagazine];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Channels_fk]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Channels] DROP CONSTRAINT [FK_Channels_fk];
GO
IF OBJECT_ID(N'[dbo].[FK_Noticias_fk]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Noticias] DROP CONSTRAINT [FK_Noticias_fk];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Channels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Channels];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Noticias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Noticias];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [UserName] varchar(200)  NOT NULL,
    [Password] varchar(200)  NOT NULL,
    [Email] varchar(200)  NOT NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'ChannelDBs'
CREATE TABLE [dbo].[ChannelDBs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ID_Client] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [UrlXml] varchar(200)  NOT NULL,
    [Order] int  NOT NULL,
    [MaxItems] int  NOT NULL
);
GO

-- Creating table 'Noticias'
CREATE TABLE [dbo].[Noticias] (
    [Id_Noticia] int IDENTITY(1,1) NOT NULL,
    [Id_Channel] int  NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [Link] varchar(200)  NOT NULL,
    [Descripcion] varchar(200)  NOT NULL,
    [Contenido] varchar(max)  NULL,
    [UrlImage] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientId] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [Id] in table 'ChannelDBs'
ALTER TABLE [dbo].[ChannelDBs]
ADD CONSTRAINT [PK_ChannelDBs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id_Noticia] in table 'Noticias'
ALTER TABLE [dbo].[Noticias]
ADD CONSTRAINT [PK_Noticias]
    PRIMARY KEY CLUSTERED ([Id_Noticia] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Client] in table 'ChannelDBs'
ALTER TABLE [dbo].[ChannelDBs]
ADD CONSTRAINT [FK_Channels_fk]
    FOREIGN KEY ([ID_Client])
    REFERENCES [dbo].[Clients]
        ([ClientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Channels_fk'
CREATE INDEX [IX_FK_Channels_fk]
ON [dbo].[ChannelDBs]
    ([ID_Client]);
GO

-- Creating foreign key on [Id_Channel] in table 'Noticias'
ALTER TABLE [dbo].[Noticias]
ADD CONSTRAINT [FK_Noticias_fk]
    FOREIGN KEY ([Id_Channel])
    REFERENCES [dbo].[ChannelDBs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Noticias_fk'
CREATE INDEX [IX_FK_Noticias_fk]
ON [dbo].[Noticias]
    ([Id_Channel]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------