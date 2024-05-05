
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Infrastructure.Persistance;

namespace Mosbi.Infrastructure.Identity.Users.Queries;

public record UserAllQuery : IRequest<IEnumerable<AppUser>>;
public class UserAllQueryHandler : IRequestHandler<UserAllQuery, IEnumerable<AppUser>>
{
    private readonly MosbiDbContext db;

    public UserAllQueryHandler(MosbiDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<AppUser>> Handle(UserAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<AppUser> users = await db.Users.ToListAsync()
               ?? throw new NullReferenceException();
        return users;
    }
}