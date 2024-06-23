using Finance.Api.Common.Api;
using Finance.Api.Models;
using Finance.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Finance.Api.Endpoints.Identity
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/logout", HandleAsync)
                .RequireAuthorization();
               

        }

        private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}
