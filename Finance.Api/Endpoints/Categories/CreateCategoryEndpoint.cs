using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;

namespace Finance.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Categories Create")
                .WithSummary("Cria uma nova Categoria")
                .WithDescription("Cria uma nova Categoria")
                .WithOrder(1)
                .Produces<Response<Category?>>();
               
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = "aps180180@gmail.com";
            var result= await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}",result)
                : TypedResults.BadRequest(result);
        }
    }
}
