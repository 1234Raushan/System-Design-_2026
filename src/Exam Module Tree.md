Exam Module Tree
Exam
│
├── Domain
│   └── Exam.cs
│
├── Persistence
│   └── ExamConfiguration.cs
│
├── Application
│   └── Features
│       └── Exams
│           │
│           ├── DTOs
│           │     ├── CreateExamRequest.cs
│           │     ├── UpdateExamRequest.cs
│           │     ├── ExamDto.cs
│           │     └── ExamMappingProfile.cs
│           │
│           ├── Commands
│           │     ├── CreateExam
│           │     ├── UpdateExam
│           │     └── DeleteExam
│           │
│           └── Queries
│                 ├── GetExamById
│                 └── GetExamList
│
└── Api
    └── ExamsController.cs


Database
TeachingAssignments
        │
        │ 1
        │
        ▼
Exams
        │
        │ 1
        │
        ▼
Marks



Relationship
Teacher
    │
TeachingAssignment
    │
Exam
    │
Marks
    │
StudentEnrollment