CREATE TABLE StudentAttendances
(
    Id INT IDENTITY(1,1) PRIMARY KEY,


    AttendanceSessionId INT NOT NULL,


    StudentId INT NOT NULL,


    Status NVARCHAR(20) NOT NULL,


    Remarks NVARCHAR(500) NULL,


    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,


    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),



    CONSTRAINT FK_StudentAttendance_Session
    FOREIGN KEY(AttendanceSessionId)
    REFERENCES AttendanceSessions(Id),



    CONSTRAINT FK_StudentAttendance_Student
    FOREIGN KEY(StudentId)
    REFERENCES Students(Id)

);