FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY Shared/Portfolio.Shared/*.csproj Shared/Portfolio.Shared/
COPY MailSender/Portfolio.Services.MailSender/*.csproj MailSender/Portfolio.Services.MailSender/ 
RUN dotnet restore  MailSender/Portfolio.Services.MailSender/*.csproj
COPY . .
RUN dotnet publish MailSender/Portfolio.Services.MailSender/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Portfolio.Services.MailSender.dll" ]