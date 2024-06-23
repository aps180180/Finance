using Finance.Api.Common.Api;
using Finance.Core;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Transactions;
using Finance.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Transaction Get All by Period")
                .WithSummary("Busca as transações do usuário por período")
                .WithDescription("Busca as transações do usuário por período")
                .WithOrder(5)
                .Produces<PagedResponse<List<Transaction>?>>();

        }

        private static async Task<IResult> HandleAsync(
                                                        ClaimsPrincipal user,
                                                        ITransactionHandler handler,
                                                        [FromQuery] DateTime? startDate = null,
                                                        [FromQuery] DateTime? endDate = null,
                                                        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                        [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            var result = await handler.GetByPeriodAsync(request);
            return result.IsSuccess
               ? TypedResults.Ok(result)
               : TypedResults.BadRequest(result);
        }
    }

}
