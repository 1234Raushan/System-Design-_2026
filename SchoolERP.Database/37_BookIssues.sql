CREATE TABLE BookIssues
(
    IssueId INT IDENTITY PRIMARY KEY,
    StudentId INT,
    CopyId INT,
    IssueDate DATE,
    ReturnDate DATE,
    Fine DECIMAL(18,2),

    FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY(CopyId) REFERENCES BookCopies(CopyId)
);