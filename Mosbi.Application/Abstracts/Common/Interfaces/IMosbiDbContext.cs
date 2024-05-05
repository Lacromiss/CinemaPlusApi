using Microsoft.EntityFrameworkCore;
using Mosbi.Domain.Entities;

namespace Mosbi.Application.Abstracts.Common.Interfaces;

public interface IMosbiDbContext
{
    DbSet<Category> Categories { get; }
  


}
