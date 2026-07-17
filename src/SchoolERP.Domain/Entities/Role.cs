using SchoolERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Entities
{
    public sealed class Role : BaseAuditableEntity
    {
        public string RoleName { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        // one role multiple users
        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
        // Required by EF Core
        private Role()
        {
        }

        public Role(string name, string? description)
        {
            SetName(name);
            Description = description;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name cannot be empty.");

            RoleName = name.Trim();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }

        public void SetUpdatedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        public void SetUpdatedBy(int? updatedBy)
        {
            UpdatedBy = updatedBy;
        }

        public void SetCreatedBy(int? createdBy)
        {
            CreatedBy = createdBy;
        }
    }
}