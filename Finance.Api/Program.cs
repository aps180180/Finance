using Finance.Api;
using Finance.Api.Common.Api;
using Finance.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();// extension method
builder.AddSecurity();// extension method
builder.AddDataContexts();// extension method
builder.AddCrossOrigin();// extension method
builder.AddDocumentation();// extension method
builder.AddServices();// extension method


var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();// metodo de extens�o

app.UseCors(ApiConfiguration.CorsPoliceName);
app.UseSecurity(); // metodo de extens�o

app.MapEndpoints();// metodo de extens�o



app.Run();


