CREATE TABLE TeacherSubjects
(
    TeacherSubjectId INT IDENTITY PRIMARY KEY,
    TeacherId INT,
    SubjectId INT,

    FOREIGN KEY(TeacherId) REFERENCES Teachers(TeacherId),
    FOREIGN KEY(SubjectId) REFERENCES Subjects(SubjectId)
);