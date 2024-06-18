using Finance.Api.Common.Api;
using Finance.Api.Endpoints.Categories;

namespace Finance.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints( this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndPoint>();





        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint  
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
