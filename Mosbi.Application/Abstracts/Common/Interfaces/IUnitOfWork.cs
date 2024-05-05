
using Mosbi.Application.Abstracts.Repositories;

namespace Mosbi.Application.Abstracts.Common.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
   

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
