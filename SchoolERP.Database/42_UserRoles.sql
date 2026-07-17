CREATE TABLE UserRoles
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NOT NULL,

    RoleId INT NOT NULL,

    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_UserRoles_Users
        FOREIGN KEY(UserId) REFERENCES Users(Id),

    CONSTRAINT FK_UserRoles_Roles
        FOREIGN KEY(RoleId) REFERENCES Roles(Id),

    CONSTRAINT UQ_UserRole UNIQUE(UserId, RoleId)
);