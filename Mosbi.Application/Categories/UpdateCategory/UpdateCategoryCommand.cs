
using MediatR;
using Mosbi.Application.Abstracts.Common.Interfaces;
using Mosbi.Domain.Entities;

namespace Mosbi.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id,Category Category) : IRequest<Category>;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Name = request.Category.Name;


        await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}