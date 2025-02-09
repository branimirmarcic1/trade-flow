FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Services/Position/Position.API/Position.API.csproj", "Services/Position/Position.API/"]
COPY ["Services/Position/Position.Application/Position.Application.csproj", "Services/Position/Position.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks/BuildingBlocks.Messaging/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
COPY ["Services/Position/Position.Domain/Position.Domain.csproj", "Services/Position/Position.Domain/"]
COPY ["Services/Position/Position.Infrastructure/Position.Infrastructure.csproj", "Services/Position/Position.Infrastructure/"]

RUN dotnet restore "./Services/Position/Position.API/Position.API.csproj"

COPY . .
WORKDIR "/src/Services/Position/Position.API"
RUN dotnet build "./Position.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Position.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Position.API.dll"]
