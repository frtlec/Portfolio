FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Application/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Application/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/
COPY Portfolio.Api/*.csproj Portfolio.Api/
COPY Portfolio.DataMigration/*.csproj Portfolio.DataMigration/
RUN dotnet restore Portfolio.DataMigration/*.csproj
COPY . .
RUN dotnet publish Portfolio.DataMigration/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.DataMigration.dll" ]
