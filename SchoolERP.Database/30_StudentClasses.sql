CREATE TABLE StudentClasses
(
    StudentClassId INT IDENTITY PRIMARY KEY,
    StudentId INT,
    ClassId INT,
    AcademicYearId INT,

    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(ClassId) REFERENCES Classes(ClassId),
    FOREIGN KEY(AcademicYearId) REFERENCES AcademicYears(AcademicYearId)
);