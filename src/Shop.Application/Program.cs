var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("MySql:ConnectionString")
    ?? throw new InvalidOperationException("MySql:ConnectionString not set");

builder.Services.AddInfrastructure(x => x
    .AddEntityFramework<DataContext>(connectionString)
    .AddGraphQL<DataContext>());

var app = builder.Build();

app.MapGraphQL();

await app.RunAsync();
