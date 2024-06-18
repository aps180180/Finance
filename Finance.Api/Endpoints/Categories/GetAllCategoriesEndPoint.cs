using Finance.Api.Common.Api;
using Finance.Core;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndPoint :IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Categories Get All")
                .WithSummary("Busca as categorias do usuário")
                .WithDescription("Busca as categorias do usuário")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();

        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler,[FromQuery] int pageNumber=Configuration.DefaultPageNumber,[FromQuery] int pageSize=Configuration.DefaultPageSize )
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = "aps180180@gmail.com",
                PageNumber = pageNumber,
                PageSize = pageSize 
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
