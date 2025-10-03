FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
RUN dotnet dev-certs https --trust
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj Services/PhotoStock/Portfolio.Services.PhotoStock/ 
RUN dotnet restore Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/Portfolio.Services.PhotoStock/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .

# Buster eski repolarý archive.debian.org'a yönlendir
RUN sed -i 's|deb.debian.org/debian|archive.debian.org/debian|g' /etc/apt/sources.list \
 && sed -i 's|security.debian.org/debian-security|archive.debian.org/debian-security|g' /etc/apt/sources.list \
 && apt-get update \
 && apt-get install -y libgdiplus \
 && rm -rf /var/lib/apt/lists/*

ENTRYPOINT [ "dotnet","Portfolio.Services.PhotoStock.dll" ]