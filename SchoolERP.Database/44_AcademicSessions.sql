CREATE TABLE AcademicSessions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    SessionName NVARCHAR(50) NOT NULL,

    StartDate DATE NOT NULL,

    EndDate DATE NOT NULL,

    IsCurrent BIT NOT NULL DEFAULT(0),

    Description NVARCHAR(250) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0)
);