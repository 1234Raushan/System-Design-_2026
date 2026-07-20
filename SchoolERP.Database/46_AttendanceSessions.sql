CREATE TABLE AttendanceSessions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    AcademicSessionId INT NOT NULL,

    ClassId INT NOT NULL,

    SectionId INT NOT NULL,

    AttendanceDate DATE NOT NULL,


    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,


    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),



    CONSTRAINT FK_AttendanceSession_AcademicSession
    FOREIGN KEY(AcademicSessionId)
    REFERENCES AcademicSessions(Id),



    CONSTRAINT FK_AttendanceSession_Class
    FOREIGN KEY(ClassId)
    REFERENCES Classes(Id),



    CONSTRAINT FK_AttendanceSession_Section
    FOREIGN KEY(SectionId)
    REFERENCES Sections(Id)
);