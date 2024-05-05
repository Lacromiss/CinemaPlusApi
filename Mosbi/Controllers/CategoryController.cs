
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mosbi.Application.Abstracts.Common.Exceptions;
using Mosbi.Application.Categories.Commands.CreateCategory;
using Mosbi.Application.Categories.Commands.DeleteCategory;
using Mosbi.Application.Categories.Commands.UpdateCategory;
using Mosbi.Application.Categories.Queries;
using Mosbi.Domain.Entities;
using Mosbi.Infrastructure.Concretes.Common;
using Mosbi.WebAPI.Controllers.Base;

namespace Mosbi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new CategorySingleQuery(id)));
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        => Ok(await Mediator.Send(new CategoryAllQuery()));
        [HttpGet("paginate")]
        public async Task<ActionResult<List<Category>>> GetCategories(int page = 1, int size = 10)
        {
            var query = new CategoriesWithPaginationQuery { Page = page, Size = size };
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryCommand request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return Ok(result);
            }
            catch (FileException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new JsonResponse { Error = true, Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Category request)
        {
            try
            {
                var result = await Mediator.Send(new UpdateCategoryCommand(id, request));
                return Ok(result);
            }
            catch (FileException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new FileException(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeleteCategoryCommand(id)));

    }


}

