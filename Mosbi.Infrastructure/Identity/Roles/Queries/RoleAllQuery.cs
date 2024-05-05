using MediatR;
using Microsoft.EntityFrameworkCore;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Infrastructure.Persistance;

namespace Mosbi.Infrastructure.Identity.Roles.Queries;

public record RoleAllQuery:IRequest<IEnumerable<AppRole>>;
public class RoleAllQueryHandler : IRequestHandler<RoleAllQuery, IEnumerable<AppRole>>
{
    private readonly MosbiDbContext _db;

    public RoleAllQueryHandler(MosbiDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<AppRole>> Handle(RoleAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<AppRole> appRole = await _db.Roles.ToListAsync(cancellationToken)
          ?? throw new NotImplementedException();
        return appRole;
    }
}
