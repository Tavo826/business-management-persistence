# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj para aprovechar cache
COPY ["BusinessPersistence/BusinessPersistence/BusinessPersistence.csproj", "BusinessPersistence/"]
COPY ["BusinessPersistence/Persistence/Persistence.csproj", "Persistence/"]
COPY ["BusinessPersistence/Domain/Domain.csproj", "Domain/"]
COPY ["BusinessPersistence/Application/Application.csproj", "Application/"]

RUN dotnet restore "BusinessPersistence/BusinessPersistence/BusinessPersistence.csproj"

# Copiar el resto del código
COPY . .

# Publicar la app
RUN dotnet publish "BusinessPersistence/BusinessPersistence/BusinessPersistence.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_EnableDiagnostics=0

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BusinessPersistence.dll"]
