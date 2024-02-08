# Shop example

This sample project demonstrates how to build a shop application using .NET, Blazor and HotChocolate GraphQL.

The architecture will use feature-slicing instead of clean/onion architecture to demonstrate how to build applications focusing on business logic instead of technical concerns.

## Work in progress

- [x] Create the project
- [x] Add minimal infrastructure (EF Core, SQL-Server, GraphQL)
- [ ] Add minimal back-end using feature-slicing
- [ ] Add Blazor front-end
- [ ] Add GitHub Actions CI/CD (Deploy to Azure)
- [ ] Add authentication & authorization
- [ ] Add shopping cart
- [ ] Add payment gateway
- [ ] Add order management
- [ ] ...

## Getting started

### Install .NET

You can install .NET 8.0.1 from the following link: https://dotnet.microsoft.com/download

### Install the tools

The repository contains a `dotnet-tools.json` file that contains the tools required to build the project. You can install them using the following command:

```bash
dotnet tool restore
```

### Start the local database

The project uses SQL-Server as a database. You can start a local instance using docker:

```bash
docker compose up -d
```

The first time, it will take a few minutes to download and start the SQL-server image.

### Create the database

You can create the database using the following command:

```bash
dotnet ef database update --project src/Shop.Application/Shop.Application.csproj
``` 

### Start the application

You can start the back-end using the following command:

```bash
dotnet run --project src/Shop.Application/Shop.Application.csproj
```

### Browse to Banana Cake Pop

To consume the GraphQL API, you can use the Banana Cake Pop tool. You can access it by browsing to the following URL: https://localhost:5001/graphql
