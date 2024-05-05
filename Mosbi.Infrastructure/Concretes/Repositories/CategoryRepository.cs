

using Mosbi.Application.Abstracts.Repositories;
using Mosbi.Domain.Entities;
using Mosbi.Infrastructure.Concretes.Repositories.Base;
using Mosbi.Infrastructure.Persistance;

namespace Mosbi.Infrastructure.Concretes.Repositories;

public class CategoryRepository:Repository<Category>,ICategoryRepository
{
    public CategoryRepository(MosbiDbContext context) : base(context)
    {
    }
}