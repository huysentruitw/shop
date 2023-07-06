using Shop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(x =>
    x.AddGraphQL());

var app = builder.Build();

app.MapGraphQL();

await app.RunAsync();
