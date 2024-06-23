using Finance.Api.Common.Api;
using Finance.Api.Models;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
                .WithName("Categories Delete")
                .WithSummary("Exclui uma Categoria")
                .WithDescription("Exclui uma Categoria")
                .WithOrder(3)
                .Produces<Response<Category?>>();

        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
