CREATE TABLE BookCopies
(
    CopyId INT IDENTITY PRIMARY KEY,
    BookId INT,
    Barcode NVARCHAR(100),
    Status NVARCHAR(50),

    FOREIGN KEY(BookId) REFERENCES Books(BookId)
);