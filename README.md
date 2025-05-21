# Northwind

## Tech Stack

- .NET 9 (SDK & Runtime)
- ASP.NET Core Web API + MVC
- Entity Framework Core (Database-First, SQL Server)
- Bootstrap 5
- Moq + xUnit (unit tests)

## How to Run

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- SQL Server (with Northwind database restored via script)
- Visual Studio 2022+ or VS Code

### Setup

```bash
git clone https://github.com/mimski/Northwind.git
cd Northwind
```

### Run API
```bash
cd Northwind.Api
dotnet run
```

### Run Web
```bash
cd ../Northwind.Web
dotnet run
```

## Tests

The solution includes unit test projects under the `/tests` directory:

### Run all tests

From the root of the solution:

```bash
dotnet test
```
