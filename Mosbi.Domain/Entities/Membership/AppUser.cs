﻿using Microsoft.AspNetCore.Identity;

namespace Mosbi.Domain.Entities.Membership;

public class AppUser : IdentityUser<int>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}
