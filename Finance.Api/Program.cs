using Finance.Api.Data;
using Finance.Api.Endpoints;
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

builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();//
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();//


var app = builder.Build();

app.UseSwagger();//
app.UseSwaggerUI();//
app.MapGet("/",() => new {message = "OK"});
app.MapEndpoints();// metodo de extensão



app.Run();


