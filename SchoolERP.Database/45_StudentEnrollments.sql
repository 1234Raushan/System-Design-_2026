CREATE TABLE StudentEnrollments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    StudentId INT NOT NULL,

    AcademicSessionId INT NOT NULL,

    ClassId INT NOT NULL,

    SectionId INT NOT NULL,

    RollNumber INT NOT NULL,

    AdmissionDate DATE NOT NULL,

    Status INT NOT NULL,

    Remarks NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy int NOT ,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Enrollment_Student
        FOREIGN KEY(StudentId)
        REFERENCES Students(Id),

    CONSTRAINT FK_Enrollment_Session
        FOREIGN KEY(AcademicSessionId)
        REFERENCES AcademicSessions(Id),

    CONSTRAINT FK_Enrollment_Class
        FOREIGN KEY(ClassId)
        REFERENCES Classes(Id),

    CONSTRAINT FK_Enrollment_Section
        FOREIGN KEY(SectionId)
        REFERENCES Sections(Id)
);

ALTER TABLE StudentEnrollments
ADD CONSTRAINT DF_StudentEnrollments_CreatedDate
DEFAULT(GETUTCDATE()) FOR CreatedDate;

CREATE UNIQUE INDEX UX_StudentEnrollments_RollNumber
ON StudentEnrollments
(
    AcademicSessionId,
    ClassId,
    SectionId,
    RollNumber
);