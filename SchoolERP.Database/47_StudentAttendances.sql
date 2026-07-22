CREATE TABLE StudentAttendances
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    AttendanceSessionId INT NOT NULL,

    StudentEnrollmentId INT NOT NULL,

    Status NVARCHAR(20) NOT NULL,

    Remarks NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT (1),

    IsDeleted BIT NOT NULL DEFAULT (0),

    CONSTRAINT FK_StudentAttendance_AttendanceSession
        FOREIGN KEY (AttendanceSessionId)
        REFERENCES AttendanceSessions(Id),

    CONSTRAINT FK_StudentAttendance_StudentEnrollment
        FOREIGN KEY (StudentEnrollmentId)
        REFERENCES StudentEnrollments(Id)
);

GO

-- One student can have only one attendance record
-- in one attendance session.

CREATE UNIQUE INDEX IX_StudentAttendance_Unique
ON StudentAttendances
(
    AttendanceSessionId,
    StudentEnrollmentId
);