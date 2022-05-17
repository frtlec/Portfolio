FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY IdentityServer/Portfolio.IdentityServer/*.csproj IdentityServer/Portfolio.IdentityServer/ 
RUN dotnet restore  IdentityServer/Portfolio.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/Portfolio.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.IdentityServer.dll" ]