CREATE TABLE Timetables
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TeachingAssignmentId INT NOT NULL,

    DayOfWeek INT NOT NULL,

    PeriodNumber INT NOT NULL,

    StartTime TIME NOT NULL,

    EndTime TIME NOT NULL,

    RoomNumber NVARCHAR(50) NULL,

    Remarks NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy NVARCHAR(100) NULL,

    UpdatedDate DATETIME2 NULL,
    UpdatedBy NVARCHAR(100) NULL,

    IsActive BIT NOT NULL DEFAULT(1),
    IsDeleted BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Timetables_TeachingAssignments
        FOREIGN KEY (TeachingAssignmentId)
        REFERENCES TeachingAssignments(Id),

    CONSTRAINT CK_Timetables_PeriodNumber
        CHECK (PeriodNumber > 0),

    CONSTRAINT CK_Timetables_Time
        CHECK (StartTime < EndTime),

    CONSTRAINT UQ_Timetables
        UNIQUE
        (
            TeachingAssignmentId,
            DayOfWeek,
            PeriodNumber
        )
);
GO

CREATE INDEX IX_Timetables_TeachingAssignmentId
ON Timetables(TeachingAssignmentId);

CREATE INDEX IX_Timetables_Day_Period
ON Timetables(DayOfWeek, PeriodNumber);
GO