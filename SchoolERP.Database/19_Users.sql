CREATE TABLE Users
(
   Id INT IDENTITY(1,1) PRIMARY KEY,

    RoleId INT NOT NULL,

    FirstName NVARCHAR(100) NOT NULL,

    LastName NVARCHAR(100) NOT NULL,

    UserName NVARCHAR(100) NOT NULL UNIQUE,

    Email NVARCHAR(200) NOT NULL UNIQUE,

    PasswordHash NVARCHAR(500) NOT NULL,

    PhoneNumber NVARCHAR(20) NULL,

    EmailConfirmed BIT NOT NULL DEFAULT 0,

    PhoneNumberConfirmed BIT NOT NULL DEFAULT 0,

    IsActive BIT NOT NULL DEFAULT 1,

    CreatedBy int,

    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    UpdatedBy int,

    UpdatedDate DATETIME2 NULL,

    LastLoginDate DATETIME2 NULL,

    IsDeleted BIT NOT NULL DEFAULT 0

    FOREIGN KEY(RoleId) REFERENCES Roles(Id)
);