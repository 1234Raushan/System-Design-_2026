CREATE TABLE Parents
(
    ParentId INT IDENTITY PRIMARY KEY,
    FatherName NVARCHAR(100),
    MotherName NVARCHAR(100),
    Mobile NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(300)
);