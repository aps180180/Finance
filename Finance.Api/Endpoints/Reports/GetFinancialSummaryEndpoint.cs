using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Finance.Core.Requests.Reports;
using Finance.Core.Responses;
using System.Security.Claims;
namespace Finance.Api.Endpoints.Reports
{
    public class GetFinancialSummaryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/summary",HandleAsync)
                .Produces<Response<FinancialSummary?>>();
        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,IReportHandler handler)
        {
            GetFinancialSummaryRequest request = new();
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetFinancialSummaryReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
