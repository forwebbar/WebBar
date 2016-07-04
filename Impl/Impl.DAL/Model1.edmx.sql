
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/04/2016 09:27:14
-- Generated from EDMX file: D:\Projects\WebBar\Impl\Impl.DAL\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BeerControl];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Device]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Device];
GO
IF OBJECT_ID(N'[dbo].[DeviceDayTotal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceDayTotal];
GO
IF OBJECT_ID(N'[dbo].[DeviceTap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceTap];
GO
IF OBJECT_ID(N'[dbo].[DeviceVurVals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceVurVals];
GO
IF OBJECT_ID(N'[dbo].[Drink]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Drink];
GO
IF OBJECT_ID(N'[dbo].[Fill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fill];
GO
IF OBJECT_ID(N'[dbo].[Market]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Market];
GO
IF OBJECT_ID(N'[dbo].[MarketDrink]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MarketDrink];
GO
IF OBJECT_ID(N'[dbo].[Price]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Price];
GO
IF OBJECT_ID(N'[dbo].[Producer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Producer];
GO
IF OBJECT_ID(N'[dbo].[Sell]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sell];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Device'
CREATE TABLE [dbo].[Device] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [Name] varchar(1024)  NOT NULL,
    [Uid] varchar(50)  NOT NULL,
    [idMarket] int  NOT NULL
);
GO

-- Creating table 'DeviceDayTotal'
CREATE TABLE [dbo].[DeviceDayTotal] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [idDevice] bigint  NOT NULL,
    [TapCode] int  NOT NULL,
    [DayTotal] bigint  NOT NULL,
    [Ts] datetimeoffset  NOT NULL
);
GO

-- Creating table 'DeviceTap'
CREATE TABLE [dbo].[DeviceTap] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idDevice] bigint  NOT NULL,
    [idDrink] int  NULL,
    [TapCode] int  NOT NULL,
    [idFutureDrink] int  NULL,
    [FutureDrinkDate] datetimeoffset  NULL
);
GO

-- Creating table 'DeviceVurVals'
CREATE TABLE [dbo].[DeviceVurVals] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idDevice] bigint  NOT NULL,
    [idMarket] int  NOT NULL,
    [TapCode] int  NOT NULL,
    [idFill] bigint  NOT NULL
);
GO

-- Creating table 'Drink'
CREATE TABLE [dbo].[Drink] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(1024)  NOT NULL,
    [Code] nvarchar(64)  NOT NULL,
    [idProducer] int  NOT NULL,
    [idUser] int  NOT NULL
);
GO

-- Creating table 'Fill'
CREATE TABLE [dbo].[Fill] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [idDevice] bigint  NOT NULL,
    [TapCode] int  NOT NULL,
    [Volume] int  NOT NULL,
    [Ts] datetimeoffset  NOT NULL,
    [OperationCode] int  NOT NULL,
    [Total] bigint  NOT NULL
);
GO

-- Creating table 'Market'
CREATE TABLE [dbo].[Market] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(1024)  NOT NULL,
    [Code] nvarchar(64)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [idUser] int  NOT NULL
);
GO

-- Creating table 'MarketDrink'
CREATE TABLE [dbo].[MarketDrink] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idMarket] int  NOT NULL,
    [idDrink] int  NOT NULL
);
GO

-- Creating table 'Price'
CREATE TABLE [dbo].[Price] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idDrink] int  NOT NULL,
    [Val] int  NOT NULL,
    [StartTs] datetimeoffset  NOT NULL,
    [EndTs] datetimeoffset  NULL,
    [idMarket] int  NULL
);
GO

-- Creating table 'Producer'
CREATE TABLE [dbo].[Producer] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(64)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [INN] nvarchar(64)  NULL,
    [idUser] int  NOT NULL,
    [Kpp] nvarchar(max)  NULL,
    [Ogrn] nvarchar(max)  NULL,
    [Account] nvarchar(max)  NULL,
    [Bik] nvarchar(max)  NULL,
    [Bank] nvarchar(max)  NULL,
    [ActualDate] datetimeoffset  NULL,
    [ActualAddress] nvarchar(max)  NULL
);
GO

-- Creating table 'Sell'
CREATE TABLE [dbo].[Sell] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [idMarket] int  NOT NULL,
    [idDrink] int  NULL,
    [idFill] bigint  NOT NULL,
    [idPrice] int  NOT NULL,
    [Volume] int  NOT NULL,
    [Sum] int  NOT NULL,
    [isCleaning] bit  NOT NULL,
    [Ts] datetimeoffset  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(128)  NOT NULL,
    [Password] varchar(128)  NOT NULL,
    [ExpireDate] datetimeoffset  NOT NULL,
    [Fio] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Email] varchar(256)  NOT NULL,
    [Phone] varchar(128)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Device'
ALTER TABLE [dbo].[Device]
ADD CONSTRAINT [PK_Device]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'DeviceDayTotal'
ALTER TABLE [dbo].[DeviceDayTotal]
ADD CONSTRAINT [PK_DeviceDayTotal]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'DeviceTap'
ALTER TABLE [dbo].[DeviceTap]
ADD CONSTRAINT [PK_DeviceTap]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'DeviceVurVals'
ALTER TABLE [dbo].[DeviceVurVals]
ADD CONSTRAINT [PK_DeviceVurVals]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Drink'
ALTER TABLE [dbo].[Drink]
ADD CONSTRAINT [PK_Drink]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Fill'
ALTER TABLE [dbo].[Fill]
ADD CONSTRAINT [PK_Fill]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Market'
ALTER TABLE [dbo].[Market]
ADD CONSTRAINT [PK_Market]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'MarketDrink'
ALTER TABLE [dbo].[MarketDrink]
ADD CONSTRAINT [PK_MarketDrink]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Price'
ALTER TABLE [dbo].[Price]
ADD CONSTRAINT [PK_Price]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Producer'
ALTER TABLE [dbo].[Producer]
ADD CONSTRAINT [PK_Producer]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Sell'
ALTER TABLE [dbo].[Sell]
ADD CONSTRAINT [PK_Sell]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------