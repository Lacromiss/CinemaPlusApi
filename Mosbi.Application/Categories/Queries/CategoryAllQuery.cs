﻿
using MediatR;
using Mosbi.Application.Abstracts.Common.Interfaces;
using Mosbi.Domain.Entities;

namespace Mosbi.Application.Categories.Queries;

public record CategoryAllQuery:IRequest<IEnumerable<Category>>;
public class CategoryAllQueryHandler : IRequestHandler<CategoryAllQuery, IEnumerable<Category>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> Handle(CategoryAllQuery request, CancellationToken cancellationToken)
    {
      
        IEnumerable<Category> Categories = await _unitOfWork.CategoryRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Categories;
    }
}
