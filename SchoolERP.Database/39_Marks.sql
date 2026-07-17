CREATE TABLE Marks
(
    MarkId INT IDENTITY PRIMARY KEY,
    ExamId INT,
    StudentId INT,
    SubjectId INT,
    Marks DECIMAL(5,2),
    Grade NVARCHAR(10),

    FOREIGN KEY(ExamId) REFERENCES Exams(ExamId),
    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(SubjectId) REFERENCES Subjects(SubjectId)
);