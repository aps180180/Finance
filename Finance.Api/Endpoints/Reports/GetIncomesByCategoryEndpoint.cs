using Finance.Api.Common.Api;
using Finance.Core.Handlers;
using Finance.Core.Models.Reports;
using Finance.Core.Requests.Reports;
using Finance.Core.Responses;
using System.Security.Claims;

namespace Finance.Api.Endpoints.Reports
{
    public class GetIncomesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/incomes", HandleAsync)
                 .Produces<Response<List<IncomesByCategory>?>>();
        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IReportHandler handler)
        {
            GetIncomesByCategoryRequest request = new();
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetIncomesByCategoryReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
