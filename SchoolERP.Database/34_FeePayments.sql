CREATE TABLE FeePayments
(
    PaymentId INT IDENTITY PRIMARY KEY,
    StudentFeeId INT,
    PaidAmount DECIMAL(18,2),
    PaymentDate DATE,
    PaymentMode NVARCHAR(50),
    TransactionNo NVARCHAR(100),

    FOREIGN KEY(StudentFeeId) REFERENCES StudentFees(StudentFeeId)
);