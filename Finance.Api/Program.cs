using Finance.Api.Data;
using Finance.Api.Handlers;
using Finance.Core.Handlers;
using Finance.Core.Models;
using Finance.Core.Requests.Categories;
using Finance.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connectionString);   
});


builder.Services.AddEndpointsApiExplorer();//
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
}); //

builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();


var app = builder.Build();

app.UseSwagger();//
app.UseSwaggerUI();//


app.MapGet("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
{
    var request = new GetCategoryByIdRequest
    {

        Id = id,
        UserId = "aps180180@gmail.com"
    };

    return await handler.GetByIdAsync(request);
}
).Produces<Response<Category?>>()
.WithName("Categories: Get by Id")
.WithSummary("Busca uma categoria");

app.MapPost("/v1/categories", async ( CreateCategoryRequest request,ICategoryHandler handler) => await handler.CreateAsync(request)
).Produces<Response<Category?>>()
.WithName("Categories: Create")
.WithSummary("Cria uma nova categoria");

app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
{
    request.Id = id;
    return await handler.UpdateAsync(request);
}
).Produces<Response<Category?>>()
.WithName("Categories: Update")
.WithSummary("Atualiza uma categoria");

app.MapDelete("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
{
    var request = new DeleteCategoryRequest
    {
        UserId = "aps180180@gmail.com",
        Id = id 
        
    };
   
   return await handler.DeleteAsync(request);
}
).Produces<Response<Category?>>()
.WithName("Categories: Delete")
.WithSummary("Exclui uma categoria");


app.Run();


