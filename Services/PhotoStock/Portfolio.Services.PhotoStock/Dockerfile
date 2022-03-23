#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/PhotoStock/Portfolio.Services.PhotoStock/Portfolio.Services.PhotoStock.csproj", "Services/PhotoStock/Portfolio.Services.PhotoStock/"]
RUN dotnet restore "Services/PhotoStock/Portfolio.Services.PhotoStock/Portfolio.Services.PhotoStock.csproj"
COPY . .
WORKDIR "/src/Services/PhotoStock/Portfolio.Services.PhotoStock"
RUN dotnet build "Portfolio.Services.PhotoStock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portfolio.Services.PhotoStock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Services.PhotoStock.dll"]