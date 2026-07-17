CREATE TABLE Teachers
(
    TeacherId INT IDENTITY PRIMARY KEY,
    DepartmentId INT,
    EmployeeCode NVARCHAR(50),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Mobile NVARCHAR(20),
    Email NVARCHAR(100),

    FOREIGN KEY(DepartmentId) REFERENCES Departments(DepartmentId)
);