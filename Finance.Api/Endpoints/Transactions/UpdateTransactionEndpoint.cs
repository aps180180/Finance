using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Requests.Transactions;
using Finance.Core.Responses;

namespace Finance.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                .WithName("Transaction Update")
                .WithSummary("Atualiza uma Transação")
                .WithDescription("Atualiza uma Transação")
                .WithOrder(2)
                .Produces<Response<Transaction?>>();

        }

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = "aps180180@gmail.com";
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }
}
