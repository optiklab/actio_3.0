#FROM microsoft/dotnet:3.0-runtime -> old
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS runtime
#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS runtime -> runs with RMQ error - maybe this is most correct way?
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet Actio.Api.dll