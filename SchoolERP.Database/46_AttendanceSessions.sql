CREATE TABLE AttendanceSessions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TeachingAssignmentId INT NOT NULL,

    AttendanceDate DATE NOT NULL,

    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy NVARCHAR(100) NULL,

    UpdatedDate DATETIME2 NULL,
    UpdatedBy NVARCHAR(100) NULL,

    IsActive BIT NOT NULL DEFAULT(1),
    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_AttendanceSessions_TeachingAssignments
        FOREIGN KEY (TeachingAssignmentId)
        REFERENCES TeachingAssignments(Id),

    -- One attendance session per teaching assignment per day
    CONSTRAINT UQ_AttendanceSessions
        UNIQUE
        (
            TeachingAssignmentId,
            AttendanceDate
        )
);
GO

CREATE INDEX IX_AttendanceSessions_TeachingAssignmentId
ON AttendanceSessions(TeachingAssignmentId);

CREATE INDEX IX_AttendanceSessions_AttendanceDate
ON AttendanceSessions(AttendanceDate);
GO


TeachingAssignment
        │
        │ 1
        │
        │
        ▼
AttendanceSession
        │
        │ 1
        │
        ▼
StudentAttendance

