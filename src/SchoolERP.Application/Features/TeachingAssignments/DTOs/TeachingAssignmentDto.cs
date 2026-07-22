using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.TeachingAssignments.DTOs
{
    public sealed class TeachingAssignmentDto
    {
        public int Id { get; init; }


        public int TeacherId { get; init; }

        public string TeacherName { get; init; } = string.Empty;


        public int SubjectId { get; init; }

        public string SubjectName { get; init; } = string.Empty;


        public int ClassId { get; init; }

        public string ClassName { get; init; } = string.Empty;


        public int SectionId { get; init; }

        public string SectionName { get; init; } = string.Empty;


        public bool IsActive { get; init; }
    }
}
