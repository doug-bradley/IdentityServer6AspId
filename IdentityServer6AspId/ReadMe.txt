


dotnet ef migrations add ApplicationDbContextModelSnapshot -c ApplicationDbContext -o Migrations/IdentityServer/ApplicationDb
dotnet ef migrations add PersistedGrantDbContextModelSnapshot -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add ConfigurationDbContextModelSnapshot -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb

dotnet ef database update -c ApplicationDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet ef database update -c PersistedGrantDbContext