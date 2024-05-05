
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Infrastructure.Concretes.Common;

namespace Mosbi.Infrastructure.Identity.Users.Commands;

public record UserDeleteCommand(string Id):IRequest<JsonResponse>
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, JsonResponse>
    {
        private readonly UserManager<AppUser> userManager;

        public UserDeleteCommandHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<JsonResponse> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return null;
            }
            else
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
