CREATE TABLE Class_A
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    ClassName NVARCHAR(100) NOT NULL,

    ClassCode NVARCHAR(20) NOT NULL,

    Description NVARCHAR(500) NULL,

    CreatedDate DATETIME2 NOT NULL,

    CreatedBy INT NULL,

    UpdatedDate DATETIME2 NULL,

    UpdatedBy INT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0)
);

CREATE UNIQUE INDEX IX_Classes_ClassName
ON Classes(ClassName);

CREATE UNIQUE INDEX IX_Classes_ClassCode
ON Classes(ClassCode);


| Id | ClassName | ClassCode |
| -: | --------- | --------- |
|  1 | Nursery   | NUR       |
|  2 | LKG       | LKG       |
|  3 | UKG       | UKG       |
|  4 | Class 1   | CLS001    |
|  5 | Class 2   | CLS002    |
|  6 | Class 3   | CLS003    |
|  7 | Class 4   | CLS004    |
|  8 | Class 5   | CLS005    |
|  9 | Class 6   | CLS006    |
| 10 | Class 7   | CLS007    |
| 11 | Class 8   | CLS008    |
| 12 | Class 9   | CLS009    |
| 13 | Class 10  | CLS010    |
| 14 | Class 11  | CLS011    |
| 15 | Class 12  | CLS012    |
