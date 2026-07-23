Features
│
└── Fees
    │
    ├── FeeAssignments
    │
    ├── FeePayments
    │
    └── Receipts

Domain Entities

Domain
│
└── Entities
    │
    ├── FeeAssignment.cs
    │
    ├── FeePayment.cs
    │
    └── Receipt.cs

Database Relationship

StudentEnrollment
        |
        | 1
        |
        | many
        |
FeeAssignments
        |
        | 1
        |
        | many
        |
FeePayments
        |
        |
        |
Receipt


Student
   |
StudentEnrollment
   |
   |
 -------------------------
 |                       |
Attendance              Fees
 |                       |
StudentAttendance       FeeAssignment
                         |
                         |
                    FeePayment
                         |
                         |
                      Receipt

 Persistence
│
└── EntityConfigurations
    │
    ├── FeeAssignmentConfiguration.cs
    ├── FeePaymentConfiguration.cs
    └── ReceiptConfiguration.cs                     


    SchoolERPDatabase

StudentEnrollment
        |
        |
        ▼

FeeAssignments
        |
        |
        ▼

FeePayments
        |
        |
        ▼

Receipts