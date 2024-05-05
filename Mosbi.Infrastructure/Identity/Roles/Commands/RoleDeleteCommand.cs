
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Infrastructure.Concretes.Common;

namespace Absheron.Infrastructure.Identity.Roles.Commands;

public record RoleDeleteCommand(string Id) : IRequest<JsonResponse>
{
    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, JsonResponse>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleDeleteCommandHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<JsonResponse> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            AppRole user = await roleManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return null;
            }
            else
            {
                IdentityResult result = await roleManager.DeleteAsync(user);
                return new JsonResponse
                {
                    Error = true,
                    Message = "Success"
                };
            }
        }
    }
}
