CREATE TABLE Classes
(
    ClassId INT IDENTITY PRIMARY KEY,
    AcademicYearId INT,
    RoomId INT,
    ClassName NVARCHAR(50),
    Section NVARCHAR(20),

    FOREIGN KEY(AcademicYearId) REFERENCES AcademicYears(AcademicYearId),
    FOREIGN KEY(RoomId) REFERENCES Rooms(RoomId)
);