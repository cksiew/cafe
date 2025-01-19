#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CMS.API/CMS.API.csproj", "CMS.API/"]
COPY ["CMS.Application/CMS.Application.csproj", "CMS.Application/"]
COPY ["CMS.Domain/CMS.Domain.csproj", "CMS.Domain/"]
COPY ["CMS.Infrastructure/CMS.Infrastructure.csproj", "CMS.Infrastructure/"]
RUN dotnet restore "./CMS.API/CMS.API.csproj"
COPY . .
WORKDIR "/src/CMS.API"
RUN dotnet build "./CMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMS.API.dll"]