using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; protected set; }
        public int? CreatedBy { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }
        public int? UpdatedBy { get; protected set; }
        public bool IsActive { get; protected set; } = true;
        public bool IsDeleted { get; protected set; }
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
        public void SoftDelete() => IsDeleted = true;
    }
}
