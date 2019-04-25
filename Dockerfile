FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ddos-sync-http-service/*.csproj ./ddos-sync-http-service/
RUN dotnet restore

# copy everything else and build app
COPY ddos-sync-http-service/. ./ddos-sync-http-service/
WORKDIR /app/ddos-sync-http-service
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/ddos-sync-http-service/out ./
ENTRYPOINT ["dotnet", "ddos-sync-http-service.dll"]
