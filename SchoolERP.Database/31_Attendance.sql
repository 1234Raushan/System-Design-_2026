CREATE TABLE Attendance
(
    AttendanceId INT IDENTITY PRIMARY KEY,
    StudentId INT,
    ClassId INT,
    AttendanceDate DATE,
    Status NVARCHAR(20),
    MarkedBy INT,

    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(ClassId) REFERENCES Classes(ClassId),
    FOREIGN KEY(MarkedBy) REFERENCES Users(UserId)
);