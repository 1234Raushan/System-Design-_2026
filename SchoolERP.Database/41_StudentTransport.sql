CREATE TABLE StudentTransport
(
    StudentTransportId INT IDENTITY PRIMARY KEY,
    StudentId INT,
    BusId INT,
    PickupPoint NVARCHAR(200),

    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(BusId) REFERENCES Buses(BusId)
);