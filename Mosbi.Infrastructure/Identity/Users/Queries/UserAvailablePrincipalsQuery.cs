
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mosbi.Infrastructure.Identity.Dtos.Roles;
using Mosbi.Infrastructure.Identity.Providers;
using Mosbi.Infrastructure.Persistance;

namespace Mosbi.Infrastructure.Identity.Users.Queries;

public class UserAvailablePrincipalsQuery : IRequest<IEnumerable<AvailablePrincipal>>
{
    public int UserId { get; set; }


    public class UserAvailablePrincipalsQueryHandler : IRequestHandler<UserAvailablePrincipalsQuery, IEnumerable<AvailablePrincipal>>
    {
        private readonly MosbiDbContext db;

        public UserAvailablePrincipalsQueryHandler(MosbiDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<AvailablePrincipal>> Handle(UserAvailablePrincipalsQuery request, CancellationToken cancellationToken)
        {
            var principals = AppClaimProvider.principals ?? new string[] { };


            var claims = await db.UserClaims.Where(m => m.UserId == request.UserId && m.ClaimValue.Equals("1"))
                .Select(m => m.ClaimType)
                .ToArrayAsync(cancellationToken);


            var result = (from p in principals
                          join c in claims on p equals c into l_join_principals
                          from lp in l_join_principals.DefaultIfEmpty()
                          select new AvailablePrincipal
                          {
                              PrincipalName = p,
                              Selected = lp != null
                          }).AsEnumerable();

            return result;
        }
    }
}
