CREATE TABLE StudentFees
(
    StudentFeeId INT IDENTITY PRIMARY KEY,
    StudentId INT,
    FeeTypeId INT,
    Amount DECIMAL(18,2),
    DueDate DATE,
    Status NVARCHAR(30),

    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(FeeTypeId) REFERENCES FeeTypes(FeeTypeId)
);