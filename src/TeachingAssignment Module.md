************************TeachingAssignment Module*****************
TeachingAssignment
│
├── Commands
│   │
│   ├── CreateTeachingAssignment
│   │      ├── CreateTeachingAssignmentCommand.cs
│   │      ├── CreateTeachingAssignmentHandler.cs
│   │      └── CreateTeachingAssignmentValidator.cs
│   │
│   ├── UpdateTeachingAssignment
│   │      ├── UpdateTeachingAssignmentCommand.cs
│   │      ├── UpdateTeachingAssignmentHandler.cs
│   │      └── UpdateTeachingAssignmentValidator.cs
│   │
│   └── DeleteTeachingAssignment
│          ├── DeleteTeachingAssignmentCommand.cs
│          ├── DeleteTeachingAssignmentHandler.cs
│          └── DeleteTeachingAssignmentValidator.cs
│
├── Queries
│   │
│   ├── GetTeachingAssignmentById
│   │      ├── GetTeachingAssignmentByIdQuery.cs
│   │      └── GetTeachingAssignmentByIdHandler.cs
│   │
│   └── GetTeachingAssignmentList
│          ├── GetTeachingAssignmentListQuery.cs
│          └── GetTeachingAssignmentListHandler.cs
│
├── DTOs
│   │
│   ├── CreateTeachingAssignmentRequest.cs
│   ├── UpdateTeachingAssignmentRequest.cs
│   ├── TeachingAssignmentDto.cs
│   └── TeachingAssignmentMappingProfile.cs
│
└── TeachingAssignmentsController.cs

************************Domain*****************
Domain
│
└── Entities
      │
      └── TeachingAssignment.cs

************************Persistence*****************
Persistence
│
├── Configurations
│      └── TeachingAssignmentConfiguration.cs
│
└── SchoolERPDbContext.cs

************************Business Relationship*****************


Teacher
    │
    ▼
Subject
    │
    ▼
Class
    │
    ▼
Section
    │
    ▼
TeachingAssignment
    │
    ├──────── Timetable
    │
    ├──────── Lesson Plan (Future)
    │
    ├──────── Homework (Future)
    │
    ├──────── Assignment (Future)
    │
    ├──────── Class Test (Future)
    │
    └──────── Marks Entry (Future)



    **************************Updated********************************

    Academic Management
│
├── Teacher
│
├── Subject
│
├── Class
│
├── Section
│
├── Teaching Assignment
│       |
│       |
│       ├── Teacher
│       ├── Subject
│       ├── Class
│       └── Section
│
├── Timetable
│       |
│       └── Teaching Assignment
│
├── Attendance
│       |
│       └── Teaching Assignment
│
├── Homework
│       |
│       └── Teaching Assignment
│
├── Exam
│       |
│       └── Teaching Assignment
│
└── Marks
        |
        └── Teaching Assignment