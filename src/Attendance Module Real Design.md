********Business Flow ********

Academic Year
      |
      |
Class
      |
Section
      |
Teaching Assignment
      |
      |
Teacher takes Attendance
      |
      |
Attendance Header
      |
      |
Attendance Details
      |
      |
Students

Complete Module Tree Structure

SchoolERP
│
├── Domain
│   │
│   └── Entities
│       │
│       ├── Attendance.cs
│       │
│       └── AttendanceDetail.cs
│
│
├── Persistence
│   │
│   └── Configurations
│       │
│       ├── AttendanceConfiguration.cs
│       │
│       └── AttendanceDetailConfiguration.cs
│
│
├── Application
│   │
│   └── Features
│       │
│       └── Attendances
│           │
│           ├── DTOs
│           │   │
│           │   ├── CreateAttendanceRequest.cs
│           │   │
│           │   ├── UpdateAttendanceRequest.cs
│           │   │
│           │   ├── AttendanceDto.cs
│           │   │
│           │   ├── AttendanceDetailDto.cs
│           │   │
│           │   └── AttendanceMappingProfile.cs
│           │
│           │
│           ├── Commands
│           │   │
│           │   ├── CreateAttendance
│           │   │   │
│           │   │   ├── CreateAttendanceCommand.cs
│           │   │   ├── CreateAttendanceValidator.cs
│           │   │   └── CreateAttendanceHandler.cs
│           │   │
│           │   │
│           │   ├── UpdateAttendance
│           │   │   │
│           │   │   ├── UpdateAttendanceCommand.cs
│           │   │   ├── UpdateAttendanceValidator.cs
│           │   │   └── UpdateAttendanceHandler.cs
│           │   │
│           │   │
│           │   └── DeleteAttendance
│           │       │
│           │       ├── DeleteAttendanceCommand.cs
│           │       ├── DeleteAttendanceValidator.cs
│           │       └── DeleteAttendanceHandler.cs
│           │
│           │
│           └── Queries
│               │
│               ├── GetAttendanceById
│               │   │
│               │   ├── GetAttendanceByIdQuery.cs
│               │   └── GetAttendanceByIdHandler.cs
│               │
│               │
│               ├── GetAttendanceList
│               │   │
│               │   ├── GetAttendanceListQuery.cs
│               │   └── GetAttendanceListHandler.cs
│               │
│               │
│               └── GetStudentAttendanceReport
│                   │
│                   ├── Query
│                   └── Handler
│
│
└── Api
    │
    └── Controllers
        │
        └── AttendancesController.cs


********Database Tree Structure********
Database
│
├── Attendances
│
└── AttendanceDetails

Attendances
     |
     | 1
     |
     | many
     |
AttendanceDetails

********Attendance Table Design********
Attendances

Id
TeachingAssignmentId
AttendanceDate

CreatedDate
CreatedBy

UpdatedDate
UpdatedBy

IsActive
IsDeleted

********AttendanceDetails Table********
AttendanceDetails

Id

AttendanceId

StudentId

Status

Remarks

CreatedDate
CreatedBy

UpdatedDate
UpdatedBy

IsActive
IsDeleted

********Final Relationship********
Teacher
   |
   |
TeachingAssignment
   |
   |
Attendance
   |
   |
AttendanceDetail
   |
   |
Student