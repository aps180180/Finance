using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                .WithName("Categories Update")
                .WithSummary("Atualiza uma Categoria")
                .WithDescription("Atualiza uma Categoria")
                .WithOrder(2)
                .Produces<Response<Category?>>();

        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler,UpdateCategoryRequest request,long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok( result)
               : TypedResults.BadRequest(result);
        }
    }
}
