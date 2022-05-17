FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
RUN dotnet dev-certs https --trust
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Application/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Application/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/
COPY Services/WorkItems/Portfolio.Services.WorkItems.API/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.API/
RUN dotnet restore Services/WorkItems/Portfolio.Services.WorkItems.API/*.csproj
COPY . .
RUN dotnet publish Services/WorkItems/Portfolio.Services.WorkItems.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.Services.WorkItems.API.dll" ]