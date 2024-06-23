using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Requests.Transactions;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                .WithName("Transaction Get")
                .WithSummary("Busca uma Transação por Id")
                .WithDescription("Busca uma Transação por Id")
                .WithOrder(4)
                .Produces<Response<Transaction?>>();

        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
