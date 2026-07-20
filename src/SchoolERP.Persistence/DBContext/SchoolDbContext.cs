using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Persistence.DBContext
{
    public class SchoolDbContext : DbContext, IApplicationDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Class_A> classes => Set<Class_A>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<TeacherSubject> TeacherSubjects => Set<TeacherSubject>();
        public DbSet<TeacherClass> TeacherClasses => Set<TeacherClass>();
        public DbSet<AcademicSession> AcademicSessions => Set<AcademicSession>();
        public DbSet<StudentEnrollment> StudentEnrollments => Set<StudentEnrollment>();
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}
