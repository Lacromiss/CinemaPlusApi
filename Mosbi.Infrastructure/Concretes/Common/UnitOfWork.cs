
using Mosbi.Application.Abstracts.Common.Interfaces;
using Mosbi.Application.Abstracts.Repositories;
using Mosbi.Infrastructure.Concretes.Repositories;
using Mosbi.Infrastructure.Persistance;

namespace Mosbi.Infrastructure.Concretes.Common;

public class UnitOfWork : IUnitOfWork
{
    readonly MosbiDbContext _dbContext;

    public UnitOfWork(MosbiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   private ICategoryRepository? _categoryRepository;
    

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_dbContext);

   

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
