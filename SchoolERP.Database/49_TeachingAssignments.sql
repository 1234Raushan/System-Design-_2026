CREATE TABLE TeachingAssignments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TeacherId INT NOT NULL,
    SubjectId INT NOT NULL,
    ClassId INT NOT NULL,
    SectionId INT NOT NULL,

    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy NVARCHAR(100) NULL,

    UpdatedDate DATETIME2 NULL,
    UpdatedBy NVARCHAR(100) NULL,

    IsActive BIT NOT NULL DEFAULT(1),
    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_TeachingAssignments_Teachers
        FOREIGN KEY (TeacherId)
        REFERENCES Teachers(Id),

    CONSTRAINT FK_TeachingAssignments_Subjects
        FOREIGN KEY (SubjectId)
        REFERENCES Subjects(Id),

    CONSTRAINT FK_TeachingAssignments_Classes
        FOREIGN KEY (ClassId)
        REFERENCES Class_A(Id),

    CONSTRAINT FK_TeachingAssignments_Sections
        FOREIGN KEY (SectionId)
        REFERENCES Sections(Id),

    CONSTRAINT UQ_TeachingAssignments
        UNIQUE
        (
            TeacherId,
            SubjectId,
            ClassId,
            SectionId
        )
);
GO

CREATE INDEX IX_TeachingAssignments_TeacherId
ON TeachingAssignments(TeacherId);

CREATE INDEX IX_TeachingAssignments_ClassSection
ON TeachingAssignments(ClassId, SectionId);

CREATE INDEX IX_TeachingAssignments_Subject
ON TeachingAssignments(SubjectId);
GO