
using Mosbi.Domain.Entities.Membership;

namespace Mosbi.Application.Abstracts.Common.Interfaces;
public interface ITokenService
{
    string BuildToken(AppUser user);
    bool ValidateToken(string token);
}
