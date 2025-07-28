# Tekus Backend

Este proyecto utiliza Entity Framework Core para el acceso a datos. A continuación se detallan los pasos para ejecutar y mantener la base de datos.

## Requisitos previos

- .NET 9.0 o superior
- SQL Server
- Entity Framework Core CLI tools

### Instalación de herramientas de EF Core

```bash
dotnet tool install --global dotnet-ef
```
# Crear appsettings.json dentro del tekus.api

``` JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "TekusDb": "Server=XXXXXX;Database=Tekus;User Id=XXXXX;Password=XXXXX;Encrypt=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "ThisIsASuperSecretKeyWith32Chars!",
    "Issuer": "tekus.api",
    "Audience": "tekus.api"
  }
}
```

# Crear Base de datos:
```bash
dotnet ef database update -p tekus.infraestructure -s tekus.api

```