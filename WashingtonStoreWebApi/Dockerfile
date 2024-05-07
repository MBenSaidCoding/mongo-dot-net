# Step 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY WashingtonStoreWebApi.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore

# Step 2 - Publish
RUN dotnet publish -c Release --no-build -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WashingtonStoreWebApi.dll"]