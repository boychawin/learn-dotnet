# CRUD

### Install Package

```sh
dotnet add package Microsoft.EntityFrameworkCore 
# dotnet add package Microsoft.EntityFrameworkCore.Migrations
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Migrations

```sh
dotnet ef migrations remove 
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Ref: <https://github.com/kongruksiamza/asp-mvc-tutorial/tree/main>