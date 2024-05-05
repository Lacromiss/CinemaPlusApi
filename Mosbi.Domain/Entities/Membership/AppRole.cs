using Microsoft.AspNetCore.Identity;

namespace Mosbi.Domain.Entities.Membership;

public class AppRole : IdentityRole<int>
{
    public byte Rank { get; set; }
}
