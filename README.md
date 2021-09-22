# Case_Study

I used ASP.NET Core Web API and MVC, Repository Design Pattern, Swagger and MsSQL  

## Getting Started

First of all, you need to clone the project to your local machine

```
gh repo clone ofurkanacuner/Case_Study
```

### Building

A step by step series of building that project

1. Restore the project :hammer:

```
dotnet restore
```

2. Update appsettings.json or appsettings.Development.json (Which you are working stage)

2. Change all connections for your development or production stage

3. If you want to use different Database Provider (Postre Sql, MySQL etc...) You can change on Data layer File: ApplicationContext.cs (Line: 15)

```
    //For Microsoft SQL Server
    optionsBuilder.UseSqlServer(connectionString: @"Server=localhost; Database=CaseStudy; User ID=sa; Password=123;trusted_connection=true;");
```

5. Run EF Core Migrations

```
dotnet ef database update
```

## Running

### Run with Dotnet CLI

Run Multiple Startup Projects :bomb:

```
CaseStudy.Api and CaseStudy.UI
```


## Built With

* [.NET Core 5.0](https://www.microsoft.com/net/) 
* [Entitiy Framework Core](https://docs.microsoft.com/en-us/ef/core/) - .NET ORM Tool
* [MsSQL](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads) - MsSQL
* [Swagger](https://swagger.io/) - API developer tools for testing and documention

## Contributing

* If you want to contribute to codes, create pull request
* If you find any bugs or error, create an issue
