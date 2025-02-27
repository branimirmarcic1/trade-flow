FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Services/ProofofConcept/ProofofConcept/ProofofConcept.csproj", "Services/ProofofConcept/ProofofConcept/"]
RUN dotnet restore "./Services/ProofofConcept/ProofofConcept/ProofofConcept.csproj"

COPY . .
WORKDIR "/src/Services/ProofofConcept/ProofofConcept"
RUN dotnet build "./ProofofConcept.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProofofConcept.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Postavi ispravan URL za ASP.NET Core hostanje
ENV ASPNETCORE_URLS=http://+:8083

ENTRYPOINT ["dotnet", "ProofofConcept.dll"]
