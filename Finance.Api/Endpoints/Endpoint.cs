using Finance.Api.Common.Api;
using Finance.Api.Endpoints.Categories;
using Finance.Api.Endpoints.Identity;
using Finance.Api.Endpoints.Transactions;
using Finance.Api.Models;

namespace Finance.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints( this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                .RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();
            
            endpoints.MapGroup("v1/transactions")
               .WithTags("Transactions")
               .RequireAuthorization()
               .MapEndpoint<CreateTransactionEndpoint>()
               .MapEndpoint<UpdateTransactionEndpoint>()
               .MapEndpoint<DeleteTransactionEndpoint>()
               .MapEndpoint<GetTransactionByIdEndpoint>()
               .MapEndpoint<GetTransactionsByPeriodEndpoint>();
            
            endpoints.MapGroup("v1/identity")
               .WithTags("Identity")
               .MapIdentityApi<User>();

            
            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapEndpoint<LogoutEndpoint>()
               .MapEndpoint<GetRolesEndpoint>();
              




        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint  
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
