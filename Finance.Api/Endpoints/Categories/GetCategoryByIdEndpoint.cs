using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;

namespace Finance.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                .WithName("Categories Get")
                .WithSummary("Busca uma Categoria por Id")
                .WithDescription("Busca uma Categoria por Id")
                .WithOrder(4)
                .Produces<Response<Category?>>();

        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler,  long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = "aps180180@gmail.com",
                Id = id
            };
            
            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
