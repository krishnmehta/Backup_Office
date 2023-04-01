FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

############################## Server build ################################
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS serverbuild
WORKDIR /app
COPY . .
WORKDIR "/app/src/Saturn.HttpApi.Host"
RUN dotnet publish Saturn.HttpApi.Host.csproj -c Release -o /app/publish

RUN mv /app/localhost.pfx /app/publish/localhost.pfx && cd /app/publish && ls

FROM base AS final
WORKDIR /app
COPY --from=serverbuild /app/publish .
CMD ["dotnet", "Saturn.HttpApi.Host.dll"]