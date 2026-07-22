using SchoolERP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.StudentAttendance.DTOs
{
    public sealed record StudentAttendanceDto
    {
        public int Id { get; init; }

        public int AttendanceSessionId { get; init; }

        public int StudentEnrollmentId { get; init; }

        public int RollNumber { get; init; }

        public string StudentName { get; init; } = string.Empty;

        public DateOnly AttendanceDate { get; init; }

        public AttendanceStatus Status { get; init; }

        public string? Remarks { get; init; }
    }
}
