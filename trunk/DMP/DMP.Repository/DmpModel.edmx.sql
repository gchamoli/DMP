
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/15/2013 13:18:28
-- Generated from EDMX file: D:\WORK\Work\DMP\trunk\DMP\DMP.Repository\DmpModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DMP];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DealerDealerManpower]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowers] DROP CONSTRAINT [FK_DealerDealerManpower];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductVarient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductVarients] DROP CONSTRAINT [FK_ProductProductVarient];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[States] DROP CONSTRAINT [FK_RegionState];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Targets] DROP CONSTRAINT [FK_DealerManpowerTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_StateDealer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dealers] DROP CONSTRAINT [FK_StateDealer];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Targets] DROP CONSTRAINT [FK_MonthTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDealerManpower]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowers] DROP CONSTRAINT [FK_UserDealerManpower];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCategoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductCategoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthDealerManpowerTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowerTargets] DROP CONSTRAINT [FK_MonthDealerManpowerTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDealerManpowerTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowerTargets] DROP CONSTRAINT [FK_UserDealerManpowerTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowers] DROP CONSTRAINT [FK_DealerManpowerProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerTrainingProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingProfileMaps] DROP CONSTRAINT [FK_DealerManpowerTrainingProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainingTrainingProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingProfileMaps] DROP CONSTRAINT [FK_TrainingTrainingProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerCompetencyProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompetencyProfileMaps] DROP CONSTRAINT [FK_DealerManpowerCompetencyProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetencyCompetencyProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompetencyProfileMaps] DROP CONSTRAINT [FK_CompetencyCompetencyProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionSpecialScheme]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialSchemes] DROP CONSTRAINT [FK_RegionSpecialScheme];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthSpecialScheme]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialSchemes] DROP CONSTRAINT [FK_MonthSpecialScheme];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerIncentive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Incentives] DROP CONSTRAINT [FK_DealerManpowerIncentive];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerDealerManpowerTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowerTargets] DROP CONSTRAINT [FK_DealerDealerManpowerTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductDealerManpowerTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowerTargets] DROP CONSTRAINT [FK_ProductDealerManpowerTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_AttritionAttritionProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttritionProfileMaps] DROP CONSTRAINT [FK_AttritionAttritionProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthIncentive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Incentives] DROP CONSTRAINT [FK_MonthIncentive];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerAttritionProfileMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttritionProfileMaps] DROP CONSTRAINT [FK_DealerManpowerAttritionProfileMap];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerUserDealerMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDealerMaps] DROP CONSTRAINT [FK_DealerUserDealerMap];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserDealerMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDealerMaps] DROP CONSTRAINT [FK_UserUserDealerMap];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductDealerManpower]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DealerManpowers] DROP CONSTRAINT [FK_ProductDealerManpower];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionUserRegionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRegionMaps] DROP CONSTRAINT [FK_RegionUserRegionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserRegionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRegionMaps] DROP CONSTRAINT [FK_UserUserRegionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerManpowerManpowerSalary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ManpowerSalaries] DROP CONSTRAINT [FK_DealerManpowerManpowerSalary];
GO
IF OBJECT_ID(N'[dbo].[FK_IncentiveSpecialSchemeIncentive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialSchemeIncentives] DROP CONSTRAINT [FK_IncentiveSpecialSchemeIncentive];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductVarientTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Targets] DROP CONSTRAINT [FK_ProductVarientTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthDsmDseTargetMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DsmDseTargetMaps] DROP CONSTRAINT [FK_MonthDsmDseTargetMap];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDsmDseTargetMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DsmDseTargetMaps] DROP CONSTRAINT [FK_UserDsmDseTargetMap];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DealerManpowers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DealerManpowers];
GO
IF OBJECT_ID(N'[dbo].[Dealers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dealers];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductVarients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductVarients];
GO
IF OBJECT_ID(N'[dbo].[Regions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regions];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Months]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Months];
GO
IF OBJECT_ID(N'[dbo].[Targets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Targets];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[ProductCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCategories];
GO
IF OBJECT_ID(N'[dbo].[Trainings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainings];
GO
IF OBJECT_ID(N'[dbo].[DealerManpowerTargets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DealerManpowerTargets];
GO
IF OBJECT_ID(N'[dbo].[TrainingProfileMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingProfileMaps];
GO
IF OBJECT_ID(N'[dbo].[Competencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competencies];
GO
IF OBJECT_ID(N'[dbo].[CompetencyProfileMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompetencyProfileMaps];
GO
IF OBJECT_ID(N'[dbo].[SpecialSchemes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecialSchemes];
GO
IF OBJECT_ID(N'[dbo].[Incentives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Incentives];
GO
IF OBJECT_ID(N'[dbo].[Attritions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attritions];
GO
IF OBJECT_ID(N'[dbo].[AttritionProfileMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttritionProfileMaps];
GO
IF OBJECT_ID(N'[dbo].[UserDealerMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDealerMaps];
GO
IF OBJECT_ID(N'[dbo].[UserRegionMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRegionMaps];
GO
IF OBJECT_ID(N'[dbo].[ManpowerSalaries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManpowerSalaries];
GO
IF OBJECT_ID(N'[dbo].[SpecialSchemeIncentives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecialSchemeIncentives];
GO
IF OBJECT_ID(N'[dbo].[DsmDseTargetMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DsmDseTargetMaps];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DealerManpowers'
CREATE TABLE [dbo].[DealerManpowers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [DealerId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [ProductId] int  NOT NULL,
    [Profile_Id] int  NOT NULL
);
GO

-- Creating table 'Dealers'
CREATE TABLE [dbo].[Dealers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StateId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ProductCategoryId] int  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [IsCommon] bit  NOT NULL
);
GO

-- Creating table 'ProductVarients'
CREATE TABLE [dbo].[ProductVarients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RegionId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [ParentId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'Months'
CREATE TABLE [dbo].[Months] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Year] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [NumberOfEmployee] int  NOT NULL
);
GO

-- Creating table 'Targets'
CREATE TABLE [dbo].[Targets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MonthId] int  NOT NULL,
    [DealerManpowerId] int  NOT NULL,
    [Actual] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Target1] int  NOT NULL,
    [Target2] int  NOT NULL,
    [ProductVarientId] int  NOT NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContactNumber] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [Designation] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [PANNumber] nvarchar(max)  NULL,
    [DateOfJoining] datetime  NULL,
    [PreviousCompany] nvarchar(max)  NULL,
    [PreviousJobProfile] nvarchar(max)  NULL,
    [TotalWorkExperience] float  NULL,
    [ExperienceWithVE] float  NULL,
    [TIVRepresenting] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DateOfLeaving] datetime  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [TrainingLevel] nvarchar(max)  NULL,
    [SAPCode] nvarchar(max)  NULL,
    [DateOfBirth] datetime  NULL,
    [AreaRepresenting] nvarchar(max)  NULL
);
GO

-- Creating table 'ProductCategories'
CREATE TABLE [dbo].[ProductCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- Creating table 'Trainings'
CREATE TABLE [dbo].[Trainings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Designation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DealerManpowerTargets'
CREATE TABLE [dbo].[DealerManpowerTargets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MonthId] int  NOT NULL,
    [Planned] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [UserId] int  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [DealerId] int  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'TrainingProfileMaps'
CREATE TABLE [dbo].[TrainingProfileMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DealerManpowerId] int  NOT NULL,
    [TrainingId] int  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [LastTrainingDate] datetime  NULL
);
GO

-- Creating table 'Competencies'
CREATE TABLE [dbo].[Competencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [Designation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CompetencyProfileMaps'
CREATE TABLE [dbo].[CompetencyProfileMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DealerManpowerId] int  NOT NULL,
    [CompetencyId] int  NOT NULL,
    [Score] float  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'SpecialSchemes'
CREATE TABLE [dbo].[SpecialSchemes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [RegionId] int  NOT NULL,
    [MonthId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [StateId] int  NOT NULL,
    [DealerId] int  NOT NULL
);
GO

-- Creating table 'Incentives'
CREATE TABLE [dbo].[Incentives] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Dealer] float  NOT NULL,
    [Volvo] float  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [DealerManpowerId] int  NOT NULL,
    [MonthId] int  NOT NULL
);
GO

-- Creating table 'Attritions'
CREATE TABLE [dbo].[Attritions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [Designation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AttritionProfileMaps'
CREATE TABLE [dbo].[AttritionProfileMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [AttritionId] int  NOT NULL,
    [DateOfLeaving] datetime  NULL,
    [DealerManpower_Id] int  NOT NULL
);
GO

-- Creating table 'UserDealerMaps'
CREATE TABLE [dbo].[UserDealerMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [DealerId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserRegionMaps'
CREATE TABLE [dbo].[UserRegionMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [RegionId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'ManpowerSalaries'
CREATE TABLE [dbo].[ManpowerSalaries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Salary] float  NOT NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [DealerManpowerId] int  NOT NULL
);
GO

-- Creating table 'SpecialSchemeIncentives'
CREATE TABLE [dbo].[SpecialSchemeIncentives] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IncentiveId] int  NOT NULL,
    [SpecialIncentive] float  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL,
    [SpecialSchemeId] int  NOT NULL
);
GO

-- Creating table 'DsmDseTargetMaps'
CREATE TABLE [dbo].[DsmDseTargetMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DsmId] int  NOT NULL,
    [DseId] int  NOT NULL,
    [MonthId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ObjectInfo_CreatedDate] datetime  NOT NULL,
    [ObjectInfo_ModifiedDate] datetime  NOT NULL,
    [ObjectInfo_DeletedDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DealerManpowers'
ALTER TABLE [dbo].[DealerManpowers]
ADD CONSTRAINT [PK_DealerManpowers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dealers'
ALTER TABLE [dbo].[Dealers]
ADD CONSTRAINT [PK_Dealers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductVarients'
ALTER TABLE [dbo].[ProductVarients]
ADD CONSTRAINT [PK_ProductVarients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Months'
ALTER TABLE [dbo].[Months]
ADD CONSTRAINT [PK_Months]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Targets'
ALTER TABLE [dbo].[Targets]
ADD CONSTRAINT [PK_Targets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trainings'
ALTER TABLE [dbo].[Trainings]
ADD CONSTRAINT [PK_Trainings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DealerManpowerTargets'
ALTER TABLE [dbo].[DealerManpowerTargets]
ADD CONSTRAINT [PK_DealerManpowerTargets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainingProfileMaps'
ALTER TABLE [dbo].[TrainingProfileMaps]
ADD CONSTRAINT [PK_TrainingProfileMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Competencies'
ALTER TABLE [dbo].[Competencies]
ADD CONSTRAINT [PK_Competencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompetencyProfileMaps'
ALTER TABLE [dbo].[CompetencyProfileMaps]
ADD CONSTRAINT [PK_CompetencyProfileMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpecialSchemes'
ALTER TABLE [dbo].[SpecialSchemes]
ADD CONSTRAINT [PK_SpecialSchemes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Incentives'
ALTER TABLE [dbo].[Incentives]
ADD CONSTRAINT [PK_Incentives]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attritions'
ALTER TABLE [dbo].[Attritions]
ADD CONSTRAINT [PK_Attritions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AttritionProfileMaps'
ALTER TABLE [dbo].[AttritionProfileMaps]
ADD CONSTRAINT [PK_AttritionProfileMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDealerMaps'
ALTER TABLE [dbo].[UserDealerMaps]
ADD CONSTRAINT [PK_UserDealerMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRegionMaps'
ALTER TABLE [dbo].[UserRegionMaps]
ADD CONSTRAINT [PK_UserRegionMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ManpowerSalaries'
ALTER TABLE [dbo].[ManpowerSalaries]
ADD CONSTRAINT [PK_ManpowerSalaries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpecialSchemeIncentives'
ALTER TABLE [dbo].[SpecialSchemeIncentives]
ADD CONSTRAINT [PK_SpecialSchemeIncentives]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DsmDseTargetMaps'
ALTER TABLE [dbo].[DsmDseTargetMaps]
ADD CONSTRAINT [PK_DsmDseTargetMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DealerId] in table 'DealerManpowers'
ALTER TABLE [dbo].[DealerManpowers]
ADD CONSTRAINT [FK_DealerDealerManpower]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerDealerManpower'
CREATE INDEX [IX_FK_DealerDealerManpower]
ON [dbo].[DealerManpowers]
    ([DealerId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductVarients'
ALTER TABLE [dbo].[ProductVarients]
ADD CONSTRAINT [FK_ProductProductVarient]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductVarient'
CREATE INDEX [IX_FK_ProductProductVarient]
ON [dbo].[ProductVarients]
    ([ProductId]);
GO

-- Creating foreign key on [RegionId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_RegionState]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionState'
CREATE INDEX [IX_FK_RegionState]
ON [dbo].[States]
    ([RegionId]);
GO

-- Creating foreign key on [DealerManpowerId] in table 'Targets'
ALTER TABLE [dbo].[Targets]
ADD CONSTRAINT [FK_DealerManpowerTarget]
    FOREIGN KEY ([DealerManpowerId])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerTarget'
CREATE INDEX [IX_FK_DealerManpowerTarget]
ON [dbo].[Targets]
    ([DealerManpowerId]);
GO

-- Creating foreign key on [StateId] in table 'Dealers'
ALTER TABLE [dbo].[Dealers]
ADD CONSTRAINT [FK_StateDealer]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StateDealer'
CREATE INDEX [IX_FK_StateDealer]
ON [dbo].[Dealers]
    ([StateId]);
GO

-- Creating foreign key on [MonthId] in table 'Targets'
ALTER TABLE [dbo].[Targets]
ADD CONSTRAINT [FK_MonthTarget]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthTarget'
CREATE INDEX [IX_FK_MonthTarget]
ON [dbo].[Targets]
    ([MonthId]);
GO

-- Creating foreign key on [UserId] in table 'DealerManpowers'
ALTER TABLE [dbo].[DealerManpowers]
ADD CONSTRAINT [FK_UserDealerManpower]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDealerManpower'
CREATE INDEX [IX_FK_UserDealerManpower]
ON [dbo].[DealerManpowers]
    ([UserId]);
GO

-- Creating foreign key on [ProductCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductCategoryProduct]
    FOREIGN KEY ([ProductCategoryId])
    REFERENCES [dbo].[ProductCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategoryProduct'
CREATE INDEX [IX_FK_ProductCategoryProduct]
ON [dbo].[Products]
    ([ProductCategoryId]);
GO

-- Creating foreign key on [MonthId] in table 'DealerManpowerTargets'
ALTER TABLE [dbo].[DealerManpowerTargets]
ADD CONSTRAINT [FK_MonthDealerManpowerTargets]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthDealerManpowerTargets'
CREATE INDEX [IX_FK_MonthDealerManpowerTargets]
ON [dbo].[DealerManpowerTargets]
    ([MonthId]);
GO

-- Creating foreign key on [UserId] in table 'DealerManpowerTargets'
ALTER TABLE [dbo].[DealerManpowerTargets]
ADD CONSTRAINT [FK_UserDealerManpowerTargets]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDealerManpowerTargets'
CREATE INDEX [IX_FK_UserDealerManpowerTargets]
ON [dbo].[DealerManpowerTargets]
    ([UserId]);
GO

-- Creating foreign key on [Profile_Id] in table 'DealerManpowers'
ALTER TABLE [dbo].[DealerManpowers]
ADD CONSTRAINT [FK_DealerManpowerProfile]
    FOREIGN KEY ([Profile_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerProfile'
CREATE INDEX [IX_FK_DealerManpowerProfile]
ON [dbo].[DealerManpowers]
    ([Profile_Id]);
GO

-- Creating foreign key on [DealerManpowerId] in table 'TrainingProfileMaps'
ALTER TABLE [dbo].[TrainingProfileMaps]
ADD CONSTRAINT [FK_DealerManpowerTrainingProfileMap]
    FOREIGN KEY ([DealerManpowerId])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerTrainingProfileMap'
CREATE INDEX [IX_FK_DealerManpowerTrainingProfileMap]
ON [dbo].[TrainingProfileMaps]
    ([DealerManpowerId]);
GO

-- Creating foreign key on [TrainingId] in table 'TrainingProfileMaps'
ALTER TABLE [dbo].[TrainingProfileMaps]
ADD CONSTRAINT [FK_TrainingTrainingProfileMap]
    FOREIGN KEY ([TrainingId])
    REFERENCES [dbo].[Trainings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainingTrainingProfileMap'
CREATE INDEX [IX_FK_TrainingTrainingProfileMap]
ON [dbo].[TrainingProfileMaps]
    ([TrainingId]);
GO

-- Creating foreign key on [DealerManpowerId] in table 'CompetencyProfileMaps'
ALTER TABLE [dbo].[CompetencyProfileMaps]
ADD CONSTRAINT [FK_DealerManpowerCompetencyProfileMap]
    FOREIGN KEY ([DealerManpowerId])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerCompetencyProfileMap'
CREATE INDEX [IX_FK_DealerManpowerCompetencyProfileMap]
ON [dbo].[CompetencyProfileMaps]
    ([DealerManpowerId]);
GO

-- Creating foreign key on [CompetencyId] in table 'CompetencyProfileMaps'
ALTER TABLE [dbo].[CompetencyProfileMaps]
ADD CONSTRAINT [FK_CompetencyCompetencyProfileMap]
    FOREIGN KEY ([CompetencyId])
    REFERENCES [dbo].[Competencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetencyCompetencyProfileMap'
CREATE INDEX [IX_FK_CompetencyCompetencyProfileMap]
ON [dbo].[CompetencyProfileMaps]
    ([CompetencyId]);
GO

-- Creating foreign key on [RegionId] in table 'SpecialSchemes'
ALTER TABLE [dbo].[SpecialSchemes]
ADD CONSTRAINT [FK_RegionSpecialScheme]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionSpecialScheme'
CREATE INDEX [IX_FK_RegionSpecialScheme]
ON [dbo].[SpecialSchemes]
    ([RegionId]);
GO

-- Creating foreign key on [MonthId] in table 'SpecialSchemes'
ALTER TABLE [dbo].[SpecialSchemes]
ADD CONSTRAINT [FK_MonthSpecialScheme]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthSpecialScheme'
CREATE INDEX [IX_FK_MonthSpecialScheme]
ON [dbo].[SpecialSchemes]
    ([MonthId]);
GO

-- Creating foreign key on [DealerManpowerId] in table 'Incentives'
ALTER TABLE [dbo].[Incentives]
ADD CONSTRAINT [FK_DealerManpowerIncentive]
    FOREIGN KEY ([DealerManpowerId])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerIncentive'
CREATE INDEX [IX_FK_DealerManpowerIncentive]
ON [dbo].[Incentives]
    ([DealerManpowerId]);
GO

-- Creating foreign key on [DealerId] in table 'DealerManpowerTargets'
ALTER TABLE [dbo].[DealerManpowerTargets]
ADD CONSTRAINT [FK_DealerDealerManpowerTargets]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerDealerManpowerTargets'
CREATE INDEX [IX_FK_DealerDealerManpowerTargets]
ON [dbo].[DealerManpowerTargets]
    ([DealerId]);
GO

-- Creating foreign key on [ProductId] in table 'DealerManpowerTargets'
ALTER TABLE [dbo].[DealerManpowerTargets]
ADD CONSTRAINT [FK_ProductDealerManpowerTargets]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductDealerManpowerTargets'
CREATE INDEX [IX_FK_ProductDealerManpowerTargets]
ON [dbo].[DealerManpowerTargets]
    ([ProductId]);
GO

-- Creating foreign key on [AttritionId] in table 'AttritionProfileMaps'
ALTER TABLE [dbo].[AttritionProfileMaps]
ADD CONSTRAINT [FK_AttritionAttritionProfileMap]
    FOREIGN KEY ([AttritionId])
    REFERENCES [dbo].[Attritions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AttritionAttritionProfileMap'
CREATE INDEX [IX_FK_AttritionAttritionProfileMap]
ON [dbo].[AttritionProfileMaps]
    ([AttritionId]);
GO

-- Creating foreign key on [MonthId] in table 'Incentives'
ALTER TABLE [dbo].[Incentives]
ADD CONSTRAINT [FK_MonthIncentive]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthIncentive'
CREATE INDEX [IX_FK_MonthIncentive]
ON [dbo].[Incentives]
    ([MonthId]);
GO

-- Creating foreign key on [DealerManpower_Id] in table 'AttritionProfileMaps'
ALTER TABLE [dbo].[AttritionProfileMaps]
ADD CONSTRAINT [FK_DealerManpowerAttritionProfileMap]
    FOREIGN KEY ([DealerManpower_Id])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerAttritionProfileMap'
CREATE INDEX [IX_FK_DealerManpowerAttritionProfileMap]
ON [dbo].[AttritionProfileMaps]
    ([DealerManpower_Id]);
GO

-- Creating foreign key on [DealerId] in table 'UserDealerMaps'
ALTER TABLE [dbo].[UserDealerMaps]
ADD CONSTRAINT [FK_DealerUserDealerMap]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerUserDealerMap'
CREATE INDEX [IX_FK_DealerUserDealerMap]
ON [dbo].[UserDealerMaps]
    ([DealerId]);
GO

-- Creating foreign key on [UserId] in table 'UserDealerMaps'
ALTER TABLE [dbo].[UserDealerMaps]
ADD CONSTRAINT [FK_UserUserDealerMap]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserDealerMap'
CREATE INDEX [IX_FK_UserUserDealerMap]
ON [dbo].[UserDealerMaps]
    ([UserId]);
GO

-- Creating foreign key on [ProductId] in table 'DealerManpowers'
ALTER TABLE [dbo].[DealerManpowers]
ADD CONSTRAINT [FK_ProductDealerManpower]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductDealerManpower'
CREATE INDEX [IX_FK_ProductDealerManpower]
ON [dbo].[DealerManpowers]
    ([ProductId]);
GO

-- Creating foreign key on [RegionId] in table 'UserRegionMaps'
ALTER TABLE [dbo].[UserRegionMaps]
ADD CONSTRAINT [FK_RegionUserRegionMap]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionUserRegionMap'
CREATE INDEX [IX_FK_RegionUserRegionMap]
ON [dbo].[UserRegionMaps]
    ([RegionId]);
GO

-- Creating foreign key on [UserId] in table 'UserRegionMaps'
ALTER TABLE [dbo].[UserRegionMaps]
ADD CONSTRAINT [FK_UserUserRegionMap]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRegionMap'
CREATE INDEX [IX_FK_UserUserRegionMap]
ON [dbo].[UserRegionMaps]
    ([UserId]);
GO

-- Creating foreign key on [DealerManpowerId] in table 'ManpowerSalaries'
ALTER TABLE [dbo].[ManpowerSalaries]
ADD CONSTRAINT [FK_DealerManpowerManpowerSalary]
    FOREIGN KEY ([DealerManpowerId])
    REFERENCES [dbo].[DealerManpowers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerManpowerManpowerSalary'
CREATE INDEX [IX_FK_DealerManpowerManpowerSalary]
ON [dbo].[ManpowerSalaries]
    ([DealerManpowerId]);
GO

-- Creating foreign key on [IncentiveId] in table 'SpecialSchemeIncentives'
ALTER TABLE [dbo].[SpecialSchemeIncentives]
ADD CONSTRAINT [FK_IncentiveSpecialSchemeIncentive]
    FOREIGN KEY ([IncentiveId])
    REFERENCES [dbo].[Incentives]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IncentiveSpecialSchemeIncentive'
CREATE INDEX [IX_FK_IncentiveSpecialSchemeIncentive]
ON [dbo].[SpecialSchemeIncentives]
    ([IncentiveId]);
GO

-- Creating foreign key on [ProductVarientId] in table 'Targets'
ALTER TABLE [dbo].[Targets]
ADD CONSTRAINT [FK_ProductVarientTarget]
    FOREIGN KEY ([ProductVarientId])
    REFERENCES [dbo].[ProductVarients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductVarientTarget'
CREATE INDEX [IX_FK_ProductVarientTarget]
ON [dbo].[Targets]
    ([ProductVarientId]);
GO

-- Creating foreign key on [MonthId] in table 'DsmDseTargetMaps'
ALTER TABLE [dbo].[DsmDseTargetMaps]
ADD CONSTRAINT [FK_MonthDsmDseTargetMap]
    FOREIGN KEY ([MonthId])
    REFERENCES [dbo].[Months]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthDsmDseTargetMap'
CREATE INDEX [IX_FK_MonthDsmDseTargetMap]
ON [dbo].[DsmDseTargetMaps]
    ([MonthId]);
GO

-- Creating foreign key on [UserId] in table 'DsmDseTargetMaps'
ALTER TABLE [dbo].[DsmDseTargetMaps]
ADD CONSTRAINT [FK_UserDsmDseTargetMap]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDsmDseTargetMap'
CREATE INDEX [IX_FK_UserDsmDseTargetMap]
ON [dbo].[DsmDseTargetMaps]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------