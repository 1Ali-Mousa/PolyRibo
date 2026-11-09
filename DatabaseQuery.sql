
CREATE DATABASE ProjectGeneratorDB;
GO

USE ProjectGeneratorDB;
GO

-- 1. Countries
CREATE TABLE Countries (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(10) NOT NULL UNIQUE
);

-- 2. Users
CREATE TABLE Users (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHashed NVARCHAR(500) NOT NULL,
    CountryID INT NOT NULL,

    CONSTRAINT FK_Users_Countries
        FOREIGN KEY (CountryID) REFERENCES Countries(ID)
);

-- 3. FrameworkTypes
CREATE TABLE FrameworkTypes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Version NVARCHAR(50) NOT NULL
);

-- 4. ArchitectureTypes
CREATE TABLE ArchitectureTypes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL
);

-- 5. Languages
CREATE TABLE Languages (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- 6. DatabaseTypes
CREATE TABLE DatabaseTypes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL
);

-- 7. Databases
CREATE TABLE Databases (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DatabaseTypeID INT NOT NULL,
    Name NVARCHAR(150) NOT NULL,
    BackUpFileName NVARCHAR(255) NOT NULL,
    UploadedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Databases_DatabaseTypes
        FOREIGN KEY (DatabaseTypeID) REFERENCES DatabaseTypes(ID)
);

-- 8. DatabaseTables
CREATE TABLE DatabaseTables (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DatabaseID INT NOT NULL,
    Name NVARCHAR(128) NOT NULL,
    SchemaName NVARCHAR(128) NOT NULL DEFAULT 'dbo',

    CONSTRAINT FK_DatabaseTables_Databases
        FOREIGN KEY (DatabaseID) REFERENCES Databases(ID),

    CONSTRAINT UQ_DatabaseTables_Database_Schema_Name
        UNIQUE (DatabaseID, SchemaName, Name)
);


-- 9. DatabaseColumns
CREATE TABLE DatabaseColumns (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DatabaseTableID INT NOT NULL,
    Name NVARCHAR(128) NOT NULL,
    DataType NVARCHAR(100) NOT NULL,
    IsNullable BIT NOT NULL DEFAULT 1,
    IsPrimaryKey BIT NOT NULL DEFAULT 0,
    IsIdentity BIT NOT NULL DEFAULT 0,
    FKColumnID INT NULL,

    CONSTRAINT FK_DatabaseColumns_DatabaseTables
        FOREIGN KEY (DatabaseTableID)
        REFERENCES DatabaseTables(ID),

    CONSTRAINT FK_DatabaseColumns_FKColumn
        FOREIGN KEY (FKColumnID)
        REFERENCES DatabaseColumns(ID),

    CONSTRAINT UQ_DatabaseColumns_Table_Name
        UNIQUE (DatabaseTableID, Name)
);


-- 9. Packages
CREATE TABLE Packages (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Version NVARCHAR(50) NOT NULL
);

-- 10. DataAccessTechnologies
CREATE TABLE DataAccessTechnologies (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- 11. Projects
CREATE TABLE Projects (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ArchitectureTypeID INT NOT NULL,
    FrameworkTypeID INT NOT NULL,
    DatabaseID INT NOT NULL,
    LanguageID INT NOT NULL,
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CreateAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Projects_Users
        FOREIGN KEY (UserID) REFERENCES Users(ID),

    CONSTRAINT FK_Projects_ArchitectureTypes
        FOREIGN KEY (ArchitectureTypeID) REFERENCES ArchitectureTypes(ID),

    CONSTRAINT FK_Projects_FrameworkTypes
        FOREIGN KEY (FrameworkTypeID) REFERENCES FrameworkTypes(ID),

    CONSTRAINT FK_Projects_Databases
        FOREIGN KEY (DatabaseID) REFERENCES Databases(ID),

    CONSTRAINT FK_Projects_Languages
        FOREIGN KEY (LanguageID) REFERENCES Languages(ID)
);

-- 12. PackagesOfProjects
CREATE TABLE PackagesOfProjects (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    PackageID INT NOT NULL,

    CONSTRAINT FK_PackagesOfProjects_Projects
        FOREIGN KEY (ProjectID) REFERENCES Projects(ID),

    CONSTRAINT FK_PackagesOfProjects_Packages
        FOREIGN KEY (PackageID) REFERENCES Packages(ID),

    CONSTRAINT UQ_PackagesOfProjects
        UNIQUE (ProjectID, PackageID)
);

-- 13. DataAccessesOfProjects
CREATE TABLE DataAccessesOfProjects (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    DataAccessTechnologyID INT NOT NULL,

    CONSTRAINT FK_DataAccessesOfProjects_Projects
        FOREIGN KEY (ProjectID) REFERENCES Projects(ID),

    CONSTRAINT FK_DataAccessesOfProjects_DataAccessTechnologies
        FOREIGN KEY (DataAccessTechnologyID)
        REFERENCES DataAccessTechnologies(ID),

    CONSTRAINT UQ_DataAccessesOfProjects
        UNIQUE (ProjectID, DataAccessTechnologyID)
);

-- 14. GitConfigurations
CREATE TABLE GitConfigurations (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL UNIQUE,
    IsEnabled BIT NOT NULL DEFAULT 0,
    RemoteUrl NVARCHAR(500) NULL,
    DefaultBranch NVARCHAR(100) NULL,

    CONSTRAINT FK_GitConfigurations_Projects
        FOREIGN KEY (ProjectID) REFERENCES Projects(ID)
);

-- 15. DockerConfigurations
CREATE TABLE DockerConfigurations (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL UNIQUE,
    IsEnabled BIT NOT NULL DEFAULT 0,
    DockerFileEnabled BIT NOT NULL DEFAULT 0,
    ComposeEnabled BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_DockerConfigurations_Projects
        FOREIGN KEY (ProjectID) REFERENCES Projects(ID)
);
GO
