using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Finance.Core.Requests.Reports;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Reports
{
    public class GetIncomesAndExpensesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/incomes-expenses", HandleAsync)
                 .Produces<Response<List<IncomesAndExpenses>?>>();
        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IReportHandler handler)
        {
            GetIncomesAndExpensesRequest request = new();
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetIncomesAndExpensesReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
