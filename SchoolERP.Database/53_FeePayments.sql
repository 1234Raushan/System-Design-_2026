CREATE TABLE FeePayments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,


    FeeAssignmentId INT NOT NULL,


    PaidAmount DECIMAL(10,2) NOT NULL,


    PaymentDate DATE NOT NULL,


    PaymentMode NVARCHAR(50) NOT NULL,


    TransactionNo NVARCHAR(100) NULL,


    Remarks NVARCHAR(500) NULL,



    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CreatedBy NVARCHAR(100) NULL,


    UpdatedDate DATETIME2 NULL,

    UpdatedBy NVARCHAR(100) NULL,


    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),



    CONSTRAINT FK_FeePayments_FeeAssignment

    FOREIGN KEY(FeeAssignmentId)

    REFERENCES FeeAssignments(Id)

);
GO