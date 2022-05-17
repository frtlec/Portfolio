FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY  Gateway/Portfolio.Gateway/*.csproj  Gateway/Portfolio.Gateway/
RUN dotnet restore Gateway/Portfolio.Gateway/*.csproj
COPY . .
RUN dotnet publish Gateway/Portfolio.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.Gateway.dll" ]