CREATE TABLE TeacherSubjects
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TeacherId INT NOT NULL,

    SubjectId INT NOT NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_TeacherSubjects_Teachers
        FOREIGN KEY(TeacherId)
        REFERENCES Teachers(Id),

    CONSTRAINT FK_TeacherSubjects_Subjects
        FOREIGN KEY(SubjectId)
        REFERENCES Subjects(Id)
);