FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj Services/PhotoStock/Portfolio.Services.PhotoStock/ 
RUN dotnet restore Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.Services.PhotoStock.dll" ]