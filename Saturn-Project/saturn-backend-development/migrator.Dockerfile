FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS migrationbuild
WORKDIR /app
COPY . .
WORKDIR "/app/src/Saturn.DbMigrator"
RUN dotnet publish Saturn.DbMigrator.csproj -c Release -o /app/mig/publish

FROM base AS final
WORKDIR /app
COPY --from=migrationbuild /app/mig/publish .
CMD ["dotnet", "Saturn.DbMigrator.dll"]