CREATE TABLE Students
(
    StudentId INT IDENTITY PRIMARY KEY,
    AdmissionNo NVARCHAR(50),
    ParentId INT,
    CurrentClassId INT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    DOB DATE,
    Gender NVARCHAR(20),
    AdmissionDate DATE,

    FOREIGN KEY(ParentId) REFERENCES Parents(ParentId)
);