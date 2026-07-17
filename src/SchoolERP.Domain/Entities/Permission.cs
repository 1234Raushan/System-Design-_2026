using SchoolERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Entities
{


    public sealed class Permission : BaseAuditableEntity
    {
        public string Code { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public string Module { get; private set; } = string.Empty;

        // Navigation Property
        public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

        // Required by EF Core
        private Permission()
        {
        }

        public Permission(
            string code,
            string name,
            string module,
            string? description = null)
        {
            SetCode(code);
            SetName(name);

            Module = module;
            Description = description;
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Permission code is required.");

            Code = code.Trim().ToUpperInvariant();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Permission name is required.");

            Name = name.Trim();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }

        public void ChangeModule(string module)
        {
            if (string.IsNullOrWhiteSpace(module))
                throw new ArgumentException("Module is required.");

            Module = module.Trim();
        }
    }
}