using SchoolERP.Domain.Common;
using SchoolERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseAuditableEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
