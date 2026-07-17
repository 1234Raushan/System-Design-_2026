CREATE TABLE Roles
(
    Id INT IDENTITY PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL,
    Description Varchar(200),
    CreatedBy int,
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Users_CreatedDate DEFAULT GETUTCDATE(),
    UpdatedBy int,
    UpdatedDate DATETIME2 NULL,
    IsDeleted BIT NOT NULL CONSTRAINT DF_Users_IsDeleted DEFAULT(0)
    isActive bit
);

INSERT INTO Roles (RoleName	,Description,IsActive)
VALUES
('Admin', 'System Administrator', 1),
('Teacher', 'Teacher', 1),
('Student', 'Student', 1),
('Accountant', 'Accountant', 1),
('Librarian', 'Librarian', 1),
('Principal', 'Principal', 1),
('Parent', 'Parent', 1);