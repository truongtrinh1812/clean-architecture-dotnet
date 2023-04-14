rmdir /S /Q Data/Migrations

dotnet ef migrations add InitialProductDb -c MainDbContext -o Data/Migrations
