using SchoolERP.Domain.Common;
using SchoolERP.Domain.Interfaces;
using SchoolERP.Persistence.DBContext;
using SchoolERP.Persistence.Repositories;

namespace SchoolERP.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _context;

    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(SchoolDbContext context)
    {
        _context = context;
    }

    public IRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseAuditableEntity
    {
        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(_context);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}