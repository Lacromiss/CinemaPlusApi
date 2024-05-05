
using MediatR;
using Mosbi.Application.Abstracts.Common.Interfaces;
using Mosbi.Domain.Entities;

namespace Mosbi.Application.Categories.Queries;

public record CategorySingleQuery(int Id) : IRequest<Category>;

internal class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategorySingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
    {
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new InvalidOperationException("Categories is null");

     

        return entity;
    }
}