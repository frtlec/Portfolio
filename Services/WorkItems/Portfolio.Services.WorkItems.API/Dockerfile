#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/WorkItems/Portfolio.Services.WorkItems.API/Portfolio.Services.WorkItems.API.csproj", "Services/WorkItems/Portfolio.Services.WorkItems.API/"]
RUN dotnet restore "Services/WorkItems/Portfolio.Services.WorkItems.API/Portfolio.Services.WorkItems.API.csproj"
COPY . .
WORKDIR "/src/Services/WorkItems/Portfolio.Services.WorkItems.API"
RUN dotnet build "Portfolio.Services.WorkItems.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portfolio.Services.WorkItems.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Services.WorkItems.API.dll"]