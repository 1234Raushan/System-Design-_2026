CREATE TABLE Teachers
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NOT NULL,

    EmployeeCode NVARCHAR(20) NOT NULL,

    FirstName NVARCHAR(100) NOT NULL,

    LastName NVARCHAR(100) NOT NULL,

    Email NVARCHAR(150) NOT NULL,

    PhoneNumber NVARCHAR(20) NULL,

    Gender NVARCHAR(20) NULL,

    DateOfBirth DATE NULL,

    JoiningDate DATE NOT NULL,

    Qualification NVARCHAR(200) NULL,

    Experience INT NULL,

    Address NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Teachers_Users
        FOREIGN KEY(UserId)
        REFERENCES Users(Id)
);