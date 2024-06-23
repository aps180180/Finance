using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Transactions;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
                .WithName("Transaction Delete")
                .WithSummary("Exclui uma Transação")
                .WithDescription("Exclui uma Transação")
                .WithOrder(3)
                .Produces<Response<Transaction?>>();

        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
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
