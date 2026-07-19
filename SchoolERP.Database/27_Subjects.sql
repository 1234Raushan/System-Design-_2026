CREATE TABLE Subjects
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    SubjectName NVARCHAR(100) NOT NULL,

    SubjectCode NVARCHAR(20) NOT NULL,

    Description NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0)
);
INSERT INTO Subjects
(
    SubjectName,
    SubjectCode,
    Description,
    CreatedDate,
    IsActive,
    IsDeleted
)
VALUES
('English',      'ENG', 'English Language',        GETDATE(), 1, 0),
('Hindi',        'HIN', 'Hindi Language',          GETDATE(), 1, 0),
('Mathematics',  'MTH', 'Mathematics',             GETDATE(), 1, 0),
('Science',      'SCI', 'General Science',         GETDATE(), 1, 0),
('Social Science','SST','History Geography Civics',GETDATE(),1,0),
('Computer',     'CMP', 'Computer Science',        GETDATE(), 1, 0),
('Physics',      'PHY', 'Physics',                 GETDATE(), 1, 0),
('Chemistry',    'CHE', 'Chemistry',               GETDATE(), 1, 0),
('Biology',      'BIO', 'Biology',                 GETDATE(), 1, 0),
('Economics',    'ECO', 'Economics',               GETDATE(), 1, 0),
('Accountancy',  'ACC', 'Accountancy',             GETDATE(), 1, 0),
('Business Studies','BST','Business Studies',      GETDATE(), 1, 0);