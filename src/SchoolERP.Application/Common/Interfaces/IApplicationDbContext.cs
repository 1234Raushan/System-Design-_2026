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

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
