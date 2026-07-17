using SchoolERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Entities
{
    public sealed class RolePermission
    {
        
        public int RoleId { get; private set; }

        public int PermissionId { get; private set; }

        // Navigation Properties

        public Role Role { get; private set; } = null!;

        public Permission Permission { get; private set; } = null!;

        private RolePermission()
        {
        }

        public RolePermission(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
