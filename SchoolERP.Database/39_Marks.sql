CREATE TABLE Marks
(
    Id INT IDENTITY PRIMARY KEY,

    ExamId INT NOT NULL,

    StudentEnrollmentId INT NOT NULL,

    ObtainedMarks DECIMAL(5,2) NOT NULL,

    Remarks NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy NVARCHAR(100),

    UpdatedDate DATETIME2,

    UpdatedBy NVARCHAR(100),

    IsActive BIT NOT NULL DEFAULT 1,

    IsDeleted BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_Marks_Exam
        FOREIGN KEY (ExamId)
        REFERENCES Exams(Id),

    CONSTRAINT FK_Marks_StudentEnrollment
        FOREIGN KEY (StudentEnrollmentId)
        REFERENCES StudentEnrollments(Id)
);

GO

CREATE UNIQUE INDEX IX_Marks
ON Marks
(
    ExamId,
    StudentEnrollmentId
);