
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mosbi.Domain.Entities.Membership;

namespace Mosbi.Infrastructure.Identity.Roles.Queries;
public record RoleSingleQuery(int Id) : IRequest<AppRole>;

internal class RoleSingleQueryHandler : IRequestHandler<RoleSingleQuery, AppRole>
{
    private readonly RoleManager<AppRole> roleManager;

    public RoleSingleQueryHandler(RoleManager<AppRole> roleManager)
    {
        this.roleManager = roleManager;
    }
    public async Task<AppRole> Handle(RoleSingleQuery request, CancellationToken cancellationToken)
    {
        if (request.Id < 1)
            return null;


        var model = await roleManager.Roles.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return model;
    }
}
