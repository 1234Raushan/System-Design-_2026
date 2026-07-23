CREATE TABLE FeeAssignments
(
    Id INT IDENTITY PRIMARY KEY,

    StudentEnrollmentId INT NOT NULL,

    AcademicSessionId INT NOT NULL,

    TotalAmount DECIMAL(10,2) NOT NULL,

    PaidAmount DECIMAL(10,2) NOT NULL DEFAULT 0,

    DueAmount DECIMAL(10,2) NOT NULL,


    CreatedDate DATETIME2 NOT NULL,

    CreatedBy NVARCHAR(100),

    UpdatedDate DATETIME2 NULL,

    UpdatedBy NVARCHAR(100),

    IsActive BIT NOT NULL DEFAULT 1,

    IsDeleted BIT NOT NULL DEFAULT 0,


    FOREIGN KEY(StudentEnrollmentId)
    REFERENCES StudentEnrollments(Id),


    FOREIGN KEY(AcademicSessionId)
    REFERENCES AcademicSessions(Id)
);