FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
RUN dotnet dev-certs https --trust
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Application/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Application/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Domain.Core/
COPY Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/*.csproj Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure/
COPY Portfolio.Api/*.csproj Portfolio.Api/
RUN dotnet restore Portfolio.Api/*.csproj
COPY . .
RUN dotnet publish Portfolio.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:10.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.Api.dll" ]
