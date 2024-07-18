
using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Finance.Core.Requests.Reports;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Reports
{
    public class GetExpensesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
           app.MapGet("/expenses",HandleAsync)
                .Produces<Response<List<ExpensesByCategory>?>>(); 
        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IReportHandler handler)
        {
            GetExpensesByCategoryRequest request = new();
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetExpensesByCategoryReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
