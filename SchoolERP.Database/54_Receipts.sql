CREATE TABLE Receipts
(
    Id INT IDENTITY(1,1) PRIMARY KEY,


    FeePaymentId INT NOT NULL,


    ReceiptNumber NVARCHAR(100) NOT NULL,


    ReceiptDate DATE NOT NULL,


    Amount DECIMAL(10,2) NOT NULL,



    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CreatedBy NVARCHAR(100) NULL,


    UpdatedDate DATETIME2 NULL,

    UpdatedBy NVARCHAR(100) NULL,


    IsActive BIT NOT NULL DEFAULT(1),

    IsDeleted BIT NOT NULL DEFAULT(0),



    CONSTRAINT FK_Receipts_FeePayment

    FOREIGN KEY(FeePaymentId)

    REFERENCES FeePayments(Id)

);
GO
CREATE UNIQUE INDEX IX_Receipts_ReceiptNumber

ON Receipts(ReceiptNumber);
GO