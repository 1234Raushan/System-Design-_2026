CREATE TABLE FeePayments
(
    Id INT IDENTITY PRIMARY KEY,


    FeeAssignmentId INT NOT NULL,


    PaidAmount DECIMAL(10,2) NOT NULL,


    PaymentDate DATE NOT NULL,


    PaymentMode NVARCHAR(50) NOT NULL,


    TransactionNo NVARCHAR(100) NULL,


    Remarks NVARCHAR(500) NULL,


    CreatedDate DATETIME2 NOT NULL,

    CreatedBy NVARCHAR(100),

    UpdatedDate DATETIME2 NULL,

    UpdatedBy NVARCHAR(100),

    IsActive BIT DEFAULT 1,

    IsDeleted BIT DEFAULT 0,


    FOREIGN KEY(FeeAssignmentId)
    REFERENCES FeeAssignments(Id)
);