# JWT AUTH API

```sh 
set ASPNETCORE_ENVIRONMENT=Development

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package BCrypt.Net
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package Microsoft.EntityFrameworkCore.Design

```

### Migrations

```sh
dotnet ef migrations remove 
dotnet ef migrations add InitialCreate
dotnet ef database update
```


ref: <https://github.com/persteenolsen/dotnet-8-jwt-refresh-auth-api/tree/main>