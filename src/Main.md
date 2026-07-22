School ERP
│
├── Authentication
│   ├── Login
│   ├── JWT
│   ├── Refresh Token (Future)
│   ├── Role
│   ├── Permission
│   └── Role Permission
│
├── User Management
│   ├── User
│   ├── Teacher
│   └── Student
│
├── Academic Setup
│   ├── Academic Session
│   ├── Class
│   ├── Section
│   └── Subject
│
├── Student Management
│   ├── Student
│   │
│   └── Student Enrollment
│       ├── Academic Session
│       ├── Class
│       ├── Section
│       └── Roll Number
│
├── Teacher Management
│   ├── Teacher
│   ├── Teacher Subject
│   └── Teacher Class
│
├── Timetable (Next)
│   ├── Teacher
│   ├── Subject
│   ├── Class
│   ├── Section
│   ├── Academic Session
│   └── Period
│
├── Attendance
│   ├── Attendance Session
│   │   ├── Date
│   │   ├── Teacher
│   │   ├── Class
│   │   └── Section
│   │
│   └── Student Attendance
│       ├── StudentEnrollment
│       ├── Attendance Session
│       ├── Status
│       └── Remarks
│
├── Examination (Future)
│   ├── Exam
│   ├── Exam Schedule
│   ├── Marks
│   ├── Grade
│   └── Report Card
│
├── Fee Management (Future)
│   ├── Fee Structure
│   ├── Student Fee
│   ├── Fee Collection
│   └── Receipt
│
├── Library (Future)
│   ├── Book
│   ├── Book Copy
│   ├── Issue Book
│   └── Return Book
│
├── Transport (Future)
│   ├── Route
│   ├── Vehicle
│   ├── Driver
│   └── Student Transport
│
├── Notification (Future)
│   ├── SMS
│   ├── Email
│   └── Push Notification
│
├── Dashboard (Future)
│   ├── Admin Dashboard
│   ├── Teacher Dashboard
│   ├── Student Dashboard
│   └── Parent Dashboard
│
└── Enterprise Features
    ├── Redis Cache
    ├── Serilog
    ├── Global Exception Middleware
    ├── Audit Logs
    ├── Background Jobs
    ├── Docker
    ├── Azure Deployment
    └── Unit Testing