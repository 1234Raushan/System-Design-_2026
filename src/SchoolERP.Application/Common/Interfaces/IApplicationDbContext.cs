using Microsoft.EntityFrameworkCore;
using SchoolERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<Student> Students { get; }
        DbSet<Class_A> classes { get; }
        DbSet<Section> Sections { get; }
        DbSet<Subject> Subjects { get; }
        DbSet<Teacher> Teachers { get; }
        DbSet<TeacherSubject> TeacherSubjects { get; }
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
