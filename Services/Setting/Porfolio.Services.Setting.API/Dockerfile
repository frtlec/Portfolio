FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
USER root
RUN dotnet dev-certs https -ep ~/.aspnet/https/aspnetapp.pfx -p zk147olu
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/Setting/Porfolio.Services.Setting.API/*.csproj Services/Setting/Porfolio.Services.Setting.API/ 
RUN dotnet restore  Services/Setting/Porfolio.Services.Setting.API/*.csproj
COPY . .
RUN dotnet publish Services/Setting/Porfolio.Services.Setting.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Porfolio.Services.Setting.API.dll" ]