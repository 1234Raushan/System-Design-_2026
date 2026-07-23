CREATE TABLE FeeAssignments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    StudentEnrollmentId INT NOT NULL,

    AcademicSessionId INT NOT NULL,


    TotalAmount DECIMAL(10,2) NOT NULL,

    PaidAmount DECIMAL(10,2) NOT NULL DEFAULT(0),

    DueAmount DECIMAL(10,2) NOT NULL,



    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CreatedBy NVARCHAR(100) NULL,


    UpdatedDate DATETIME2 NULL,

    UpdatedBy NVARCHAR(100) NULL,


    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),



    CONSTRAINT FK_FeeAssignments_StudentEnrollment

    FOREIGN KEY(StudentEnrollmentId)

    REFERENCES StudentEnrollments(Id),



    CONSTRAINT FK_FeeAssignments_AcademicSession

    FOREIGN KEY(AcademicSessionId)

    REFERENCES AcademicSessions(Id)

);
GO
CREATE UNIQUE INDEX IX_FeeAssignments_Student_Session

ON FeeAssignments
(
    StudentEnrollmentId,
    AcademicSessionId
);
GO