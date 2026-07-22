                         User
                          |
      ------------------------------------------------
      |             |             |          |        |
   Student      Teacher      Accountant  Librarian Admin
      |             |             |          |
      |             |             |          |
Enrollment   TeachingAssignment  Fees     Books



Users
 |
 |----------------
 |              |
Student       Teacher
 |
 |
StudentEnrollment
 |
 |
StudentAttendance
        ^
        |
AttendanceSession
        ^
        |
TeachingAssignment
        |
 -------------------
 |        |         |
TimeTable Exam   Marks


User
 |
Student
 |
StudentEnrollment
 |
 -------------------------------------------------
 |            |             |          |          |
Attendance   Exam         Fees     Library   Transport
 |
 |
StudentAttendance


                           Users
                              |
              --------------------------------
              |                              |
           Student                         Teacher
              |                              |
              |                              |
   StudentEnrollment                 TeachingAssignment
              |                              |
              |                              |
              |              ---------------------------------
              |              |              |                |
              |          TimeTable   AttendanceSession     Exam
              |                              |              |
              |                              |              |
              |                      StudentAttendance    Marks
              |
              |
 ----------------------------------------------------------
 |              |                |              |           |
Fees        Library          Transport     ReportCard   Documents








                         User
                          |
                          |
                      Teacher
                          |
        --------------------------------
        |              |               |
        |              |               |
 TeacherSubject   ClassTeacher   TeachingAssignment
                   Mapping
                                      |
                                      |
              ----------------------------------------
              |                 |                    |
          Timetable        AttendanceSession       Exam
                                  |
                                  |
                         StudentAttendance
                                  |
                                  |
                          StudentEnrollment