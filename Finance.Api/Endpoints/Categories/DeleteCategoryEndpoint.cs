using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;

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

        private static async Task<IResult> HandleAsync(ICategoryHandler handler,  long id)
        {
            var request = new DeleteCategoryRequest
            { UserId = "aps180180@gmail.com",
              Id = id
            };
                       
            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
