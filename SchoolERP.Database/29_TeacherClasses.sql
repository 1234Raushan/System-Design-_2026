CREATE TABLE TeacherClasses
(
    TeacherClassId INT IDENTITY PRIMARY KEY,
    TeacherId INT,
    ClassId INT,
    SubjectId INT,

    FOREIGN KEY(TeacherId) REFERENCES Teachers(TeacherId),
    FOREIGN KEY(ClassId) REFERENCES Classes(ClassId),
    FOREIGN KEY(SubjectId) REFERENCES Subjects(SubjectId)
);